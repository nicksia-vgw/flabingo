using System.Collections.Generic;
using System.Runtime.Hosting;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Bingo.Views {
    public class CalledNumbersTopView : MonoBehaviour {
        private void OnEnable() {
            StateManager.SubscribeUntilDisable(this, state => {
                LoadNumbers(state.Bingo.CalledNumbers);
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
                    child.gameObject.SetActive(true);
                    child.GetComponentsInChildren<Text>()[1].text = "BINGO"[numbers[i] / 15].ToString();
                    child.GetComponentsInChildren<Text>()[0].text = numbers[i].ToString();
                    child.GetComponent<Image>().sprite = Resources.Load<Sprite>($"bingoball_{numbers[i] / 15}");
                } else {
                    child.gameObject.SetActive(false);
                }
            }
        }
        
        
    }
}
