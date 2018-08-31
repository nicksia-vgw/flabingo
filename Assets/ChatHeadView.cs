using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHeadView : MonoBehaviour {
    private Coroutine _coroutine;

    public void ShowEmote(string emoteName) {
        if (_coroutine != null) {
            StopCoroutine(_coroutine);
            GetEmoteObject().SetActive(false);
        }
        _coroutine = StartCoroutine(ShowEmoteAsync(emoteName));
    }

    private IEnumerator ShowEmoteAsync(string emoteName) {
        Image image = GetEmoteImage();
        image.sprite = Resources.Load<Sprite>($"emotes/{emoteName}");
        GetEmoteObject().SetActive(true);
        yield return new WaitForSeconds(1);
        GetEmoteObject().SetActive(false);
    }

    private Image GetEmoteImage() {
        return transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    private GameObject GetEmoteObject() {
        return transform.GetChild(0).gameObject;
    }
}
