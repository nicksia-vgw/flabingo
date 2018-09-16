using System.Collections;
using System.Collections.Generic;
using Source;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ChatHeadDummyViewComponent : MonoBehaviour {

	protected void OnEnable() { 
		StateManager.Subscribe(this, state => {
			if (state.Bingo.CalledNumbers.Count > 3 && Random.value > 0.8f) {
				
				StartCoroutine(ShowEmoteAsync());
			}
		});
	}

	private IEnumerator ShowEmoteAsync() {
		yield return new WaitForSeconds(Random.Range(1f, 3.5f));
		var index = (new[] {"03", "04", "11", "12", "16", "17"})[Random.Range(0, 5)];
		GetComponent<ChatHeadView>().ShowEmote($"emojis_{index}");
	}
}
