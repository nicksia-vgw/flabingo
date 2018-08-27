using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePulseEffect : MonoBehaviour {
	public Text Text;
	// Use this for initialization
	void Start () {
		Text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		Text.color = new Color(0, 0, 0, (Mathf.Sin(Time.timeSinceLevelLoad * 4f) / 5) + 0.8f);
	}
}
