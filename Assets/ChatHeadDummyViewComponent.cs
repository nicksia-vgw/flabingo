using System;
using System.Collections;
using System.Collections.Generic;
using Source;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChatHeadDummyViewComponent : MonoBehaviour {
	public int BingosWon;
	private int _lastNumbersCount;

	protected void OnEnable() { 
		StateManager.Subscribe(this, state => {
			if (state.Bingo.CalledNumbers.Count == _lastNumbersCount) {
				return;
			} 
			
			if (state.Bingo.CalledNumbers.Count > 3 && Random.value > 0.85f) {
				StartCoroutine(ShowEmoteAsync());
			}
			
			if ((state.Bingo.CalledNumbers.Count > (15 * (BingosWon + 1))) && Random.value > 0.85f && BingosWon <= 2) {
				BingosWon++;
				StartCoroutine(ShowBingoAsync());
			}

			_lastNumbersCount = state.Bingo.CalledNumbers.Count;
		});
	}

	private IEnumerator ShowBingoAsync() {
		yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
		GetComponent<ChatHeadView>().ShowBingo();
	}

	private IEnumerator ShowEmoteAsync() {
		yield return new WaitForSeconds(Random.Range(1f, 3.5f));
		var index = (new[] {"03", "04", "11", "12", "16", "17"})[Random.Range(0, 5)];
		GetComponent<ChatHeadView>().ShowEmote($"emojis_{index}");
	}
}
