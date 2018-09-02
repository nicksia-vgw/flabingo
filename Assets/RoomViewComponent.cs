using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomViewComponent : MonoBehaviour {
    public GameObject PlayCard;
    private Coroutine _coroutine;

    public void TogglePlayCard(Boolean isActive) {
        if (PlayCard.activeSelf != isActive && _coroutine == null) {
            _coroutine = StartCoroutine(ShowPlayCardAsync(PlayCard, isActive));
        }      
    }

    private IEnumerator ShowPlayCardAsync(GameObject playCard, Boolean active) {
        if (!active) {
            yield return new WaitForSeconds(0.2f);
        }
        
        float flipAngle = 0f;
        while (flipAngle < Mathf.PI / 2) {
            flipAngle += 0.08f;
            SetScaleX(Mathf.Cos(flipAngle));
            yield return null;
        }
        
        playCard.SetActive(active);
        
        while (flipAngle > 0) {
            flipAngle -= 0.08f;
            SetScaleX(Mathf.Cos(flipAngle));
            yield return null;
        }
        
        transform.localScale = Vector3.one;
        _coroutine = null;
    }

    private void SetScaleX(float x) {
        var scale = transform.localScale;
        scale.x = x;
        transform.localScale = scale;
    }
}
