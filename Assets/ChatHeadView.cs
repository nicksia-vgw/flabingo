using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatHeadView : MonoBehaviour {
    public Text BingoText;
    public GameObject FlabingoObj;
    
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
        yield return new WaitForSeconds(2f);
        GetEmoteObject().SetActive(false);
    }

    private Image GetEmoteImage() {
        return transform.GetChild(1).GetChild(0).GetComponent<Image>();
    }

    private GameObject GetEmoteObject() {
        return transform.GetChild(1).gameObject;
    }

    public void ShowBingo() {
        StartCoroutine(ShowBingoAsync());
    }
    
    private IEnumerator ShowBingoAsync() {
        try {
            BingoText.text = (int.Parse(BingoText.text) - 1).ToString();
        }
        catch (Exception e) {
			
        }
		
        FlabingoObj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        FlabingoObj.SetActive(false);
    }
}
