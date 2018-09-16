using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	private static AudioSource _audioSource;
	protected void Awake() {
		_audioSource = GetComponent<AudioSource>();
	}

	protected void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Play("click");
		}
	}

	public static void Play(string clip) {
		AudioClip audioClip = Resources.Load<AudioClip>($"sfx/{clip}");
		_audioSource.PlayOneShot(audioClip);
	}
	
	public static void PlayVoice(string clip) {
		AudioClip audioClip = Resources.Load<AudioClip>($"voice/{clip}");
		_audioSource.PlayOneShot(audioClip);
	}
}
