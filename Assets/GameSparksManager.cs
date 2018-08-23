using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using GameSparks.Core;
using GameSparks.RT;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameSparksManager : MonoBehaviour {
	public GameObject BingoGrid;
	public GameObject NumbersGrid;
	public GameObject ReferenceGrid;
	
	public GameObject GameCanvas;
	
	public Button BingoButton;

	public List<List<int>> WinningLines = new List<List<int>> {
		// Horizontal
		new List<int> {0,   1,  2,  3,  4},
		new List<int> {5,   6,  7,  8,  9},
		new List<int> {10, 11, 12, 13, 14},
		new List<int> {15, 16, 17, 18, 19},
		new List<int> {20, 21, 22, 23, 24},
		
		// Vertical
		new List<int> {0, 5, 10, 15, 20},
		new List<int> {1, 6, 11, 16, 21},
		new List<int> {2, 7, 12, 17, 22},
		new List<int> {3, 8, 13, 18, 23},
		new List<int> {4, 9, 14, 19, 24},
		
		// Diagonal
		new List<int> {0, 6, 12, 18, 24},
		new List<int> {4, 8, 12, 16, 20},
	};
	
	private static GameSparksManager instance = null;
	private string _challenge;
	private List<int> _knownNumbers = new List<int>();
	private List<int> _clickedNumbers = new List<int> {12};

	private const int STARTING_NUMBERS_CODE = 100;
	private const int NUMBER_PULLED_CODE = 101;
	private const int PLAYER_ID_CODE = 102;
	private const int END_GAME_CODE = 110;

	private const int BINGO_CODE = 201;

	private bool _hacksEnabled;

	void Awake() {
		if (instance == null) // check to see if the instance has a reference
		{
			instance = this; // if not, give it a reference to this class...
			DontDestroyOnLoad(this.gameObject); // and make this object persistent as we load new scenes
		} else // if we already have a reference then remove the extra manager from the scene
		{
			Destroy(this.gameObject);
		}
	}

	void Start() {
		StartCoroutine(AuthAsync());
		MatchUpdatedMessage.Listener = message => {
			Debug.Log("Updated Match!");
			message.AddedPlayers.ForEach(p => Debug.Log(p));
		};
		
		MatchFoundMessage.Listener = message => {
			Debug.Log("Found Match: " + message.MatchId);
			message.Participants.ToList().ForEach(p => Debug.Log(p.Id));
			var rt = GetComponent<GameSparksRTUnity>();
			rt.Configure(message,
				peerId => { },
				peerId => { },
				ready => { },
				OnPacketReceived);
			rt.Connect();
			GameCanvas.gameObject.SetActive(true);
		};
	}

	private void OnPacketReceived(RTPacket packet) {
		switch (packet.OpCode) {
			case STARTING_NUMBERS_CODE:
				Debug.Log($"Got starting numbers: {packet.Data.GetString(1)}");
				LoadBingoGrid(packet.Data.GetString(1).Split(',').Select(int.Parse).ToList());
				LoadReferenceGrid();
				break;
			case NUMBER_PULLED_CODE:
				Debug.Log($"Got numbers: {packet.Data.GetString(1)}");
				LoadNumbers(packet.Data.GetString(1).Split(',').Select(int.Parse).ToList());
				break;
			case PLAYER_ID_CODE: 
				Debug.Log($"I am player: {packet.Data.GetString(1)}");
				break;
			case END_GAME_CODE: 
				Debug.Log("Game finished!");
				break;
		}
		//Debug.Log(packet.OpCode + "-" + packet.Data.ToString());
	}
	
	private void LoadReferenceGrid() {
		for (int i = 0; i < ReferenceGrid.transform.childCount; i++) {
			ReferenceGrid.transform.GetChild(i).GetComponentInChildren<Text>().text = (i + 1).ToString();
		}
	}

	private void LoadBingoGrid(List<int> numbers) {
		for (int i = 0; i < numbers.Count; i++) {
			var index = i;
			BingoGrid.transform.GetChild(i).GetComponentInChildren<Text>().text = numbers[i].ToString();
			BingoGrid.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ClickButton(index));
		}
	}

	private void ClickButton(int index) {
		var button = BingoGrid.transform.GetChild(index).GetComponent<Button>();
		var clickedNumber = int.Parse(BingoGrid.transform.GetChild(index).GetComponentInChildren<Text>().text);
		if (_hacksEnabled || _knownNumbers.Contains(clickedNumber)) {
			button.onClick.RemoveAllListeners();
			button.interactable = false;
			_clickedNumbers.Add(index);
			if (WinningLines.Any(l => l.Intersect(_clickedNumbers).Count() == l.Count)) {
				BingoButton.interactable = true;
			}
		}
	}

	public void EnableHacks() {
		_hacksEnabled = true;
	}

	public void Bingo() {
		Debug.Log("Pressed Bingo!");
		BingoButton.GetComponentInChildren<Text>().text = "You win good for you.";
		GetComponent<GameSparksRTUnity>().Disconnect();
		GameCanvas.gameObject.SetActive(false);
	}

	public void StartMatchmaking() {
		StartCoroutine(StartMatchmakingAsync());
	}

	private void LoadNumbers(List<int> numbers) {
		_knownNumbers = numbers;
		numbers.Reverse();
		for (int i = 0; i < NumbersGrid.transform.childCount && i < numbers.Count; i++) {
			NumbersGrid.transform.GetChild(i).GetComponentInChildren<Text>().text = numbers[i].ToString();
			ReferenceGrid.transform.GetChild(numbers[i] - 1).GetComponentInChildren<Button>().interactable = false;
		}
	}

	private IEnumerator AuthAsync() {
		yield return new WaitForSeconds(1f);
		new DeviceAuthenticationRequest().SetDisplayName("Nick Sia").Send(response => {
			if (!response.HasErrors) {
				Debug.Log($"Authenticated as {response.UserId}");

			} else {
				Debug.Log("Failed to authenticate!");
			}
		});
	}
	
	

	private IEnumerator StartMatchmakingAsync() {
		
		new MatchmakingRequest()
			.SetAction(null)
			.SetMatchShortCode("matchClassicBingo")
			.SetSkill(0)
			.Send(response => {
				Debug.Log("Matchmaking Requested");
			});
		yield return null;
		
	}
}

//.SetDisplayName("Nick Sia")
//.SetPassword("password")
//.SetUserName("Nick Sia")