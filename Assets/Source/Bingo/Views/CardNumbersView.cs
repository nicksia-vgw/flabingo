using System;
using System.Collections.Generic;
using Source.Bingo.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Bingo.Views {
    public class CardNumbersView  : MonoBehaviour {
        public int Index;

        protected void Start() {
            Index = transform.parent.GetSiblingIndex();
            for (int i = 0; i < transform.childCount; i++) {
                int index = i;
                transform.GetChild(i).GetComponentInChildren<Text>().text = String.Empty;
                transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ClickButton(index));
            }
        }
        
        protected void OnEnable() { 
            StateManager.Subscribe(this, state => {
                if (state.Bingo.Cards[Index] != null) {
                    UpdateCard(state.Bingo.Cards[Index].Numbers, state.Bingo.Cards[Index].DaubedIndexes);
                }
            });
        }
        
        private void UpdateCard(List<int> numbers, List<int> daubedIndexes) {
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                child.GetComponentInChildren<Text>().text = i < numbers.Count ? numbers[i].ToString() : String.Empty;
                if (child.GetComponent<Button>().IsInteractable() && daubedIndexes.Contains(i)) {
                    AudioController.Play("daub");
                }
                child.GetComponent<Button>().interactable = !daubedIndexes.Contains(i);
            } 
        }
        
        private void ClickButton(int index) {
            StateManager.Dispatch(new CardDaubNumberAction {CardIndex = Index, NumberIndex = index}); 
        }
        
    }
}