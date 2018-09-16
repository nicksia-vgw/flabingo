using System.Collections;
using System.Collections.Generic;
using System.Runtime.Hosting;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Bingo.Views {
    public class CalledNumbersTopView : MonoBehaviour {
        private int _previousNumbersCount;

        private void OnEnable() {
            StateManager.SubscribeUntilDisable(this, state => {
                if (state.Bingo.CalledNumbers.Count > _previousNumbersCount) {
                    LoadNumbers(state.Bingo.CalledNumbers);
                }

                _previousNumbersCount = state.Bingo.CalledNumbers.Count;
            });
            
        }
        
        private void LoadNumbers(List<int> numbers) {
            if (numbers == null) {
                return;
            }

            numbers.Reverse();
            for (int i = 0; i < transform.childCount; i++) {
                var child = transform.GetChild(i);
                if (i < numbers.Count) {
                    
                    StartCoroutine(ShowNumberAsync(child, numbers[i], i == transform.childCount - 1));
                } else {
                    child.gameObject.SetActive(false);
                }

            }
        }

        private IEnumerator ShowNumberAsync(Transform child, int number, bool isLast) {
            var uiChild = child.GetChild(0);
            var rectTransform = uiChild.GetComponent<RectTransform>();

            if (isLast) {
                child.gameObject.SetActive(false);
                yield return new WaitForSeconds(160f/360f);
            }
            else {
                var anchoredPosition = rectTransform.anchoredPosition;
                while (anchoredPosition.x < 160) {
                    anchoredPosition.x += 360 * Time.deltaTime;
                    rectTransform.anchoredPosition = anchoredPosition;
                    yield return null;
                }

                anchoredPosition.x = 0;
                rectTransform.anchoredPosition = anchoredPosition;
            }

            child.gameObject.SetActive(true);
            uiChild.GetComponentsInChildren<Text>()[1].text = "BINGO"[(number - 1) / 15].ToString();
            uiChild.GetComponentsInChildren<Text>()[0].text = number.ToString();
            uiChild.GetComponent<Image>().sprite = Resources.Load<Sprite>($"bingoball_{(number-1) / 15}");
        }
    }
}
