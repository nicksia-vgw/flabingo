using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Bingo.Views {
    public class BingoCardView  : MonoBehaviour {
        public int Index;

        protected void Start() {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponentInChildren<Text>().text = String.Empty;
                //transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ClickButton(index));
            }
        }
        
        protected void OnEnable() {    
            StateManager.SubscribeUntilDisable(this, state => {
                if (state.Bingo.Cards[Index] != null) {
                    UpdateCard(state.Bingo.Cards[Index].Numbers);
                }
            });
            
        }
        
        private void UpdateCard(List<int> numbers) {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).GetComponentInChildren<Text>().text = numbers[i].ToString();
                //transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ClickButton(index));
            } 
        }
        
//        private void ClickButton(int index) {
//            
//            var button = BingoGrid.transform.GetChild(index).GetComponent<Button>();
//            var clickedNumber = int.Parse(BingoGrid.transform.GetChild(index).GetComponentInChildren<Text>().text);
//            if (_hacksEnabled || _knownNumbers.Contains(clickedNumber)) {
//                button.onClick.RemoveAllListeners();
//                button.interactable = false;
//                _clickedNumbers.Add(index);
//                if (WinningLines.Any(l => l.Intersect(_clickedNumbers).Count() == l.Count)) {
//                    BingoButton.interactable = true;
//                }
//            }
//        }
        
    }
}