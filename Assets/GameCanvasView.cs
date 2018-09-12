using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasView : MonoBehaviour {
	public GameObject Flabingo;

	private void OnEnable() {
		Flabingo.SetActive(false);
	}
}
