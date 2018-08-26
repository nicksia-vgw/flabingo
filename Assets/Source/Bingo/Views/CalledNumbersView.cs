using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Bingo.Views {
    public class CalledNumbersView : MonoBehaviour {
        private void OnEnable() {
            StateManager.SubscribeUntilDisable(this, state => {
                LoadNumbers(state.Bingo.CalledNumbers);
            });
        }
        
        private void LoadNumbers(List<int> numbers) {
            if (numbers == null) {
                return;
            }
            
            for (int i = 0; i < transform.childCount && i < numbers.Count; i++) {
                transform.GetChild(i).GetComponentInChildren<Text>().text = numbers[i].ToString();
            }
        }
    }
}
