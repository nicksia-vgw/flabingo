using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Bingo.Views {
    public class CalledNumbersReferenceView : MonoBehaviour {
        protected void Start() {
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                child.GetComponentInChildren<Text>().text = (i + 1).ToString();
            }
        }
        
        protected void OnEnable() {
            StateManager.SubscribeUntilDisable(this, state => {
                LoadNumbers(state.Bingo.CalledNumbers);
            });
        }
        
        private void LoadNumbers(List<int> numbers) {
            if (numbers == null) {
                return;
            }
            
            for (int i = 0; i < transform.childCount && i < numbers.Count; i++) {
                transform.GetChild(numbers[i] - 1).GetComponentInChildren<Button>().interactable = false;
            }
        }
    }
}
