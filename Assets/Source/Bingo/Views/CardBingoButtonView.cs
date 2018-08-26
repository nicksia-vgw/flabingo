using System;
using System.Collections.Generic;
using System.Linq;
using Source.Bingo.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Bingo.Views {
    public class CardBingoButtonView  : MonoBehaviour {
        public int Index;

        protected void Start() {
            Index = transform.parent.GetSiblingIndex();
            GetComponent<Button>().onClick.AddListener(() => GameSparksManager.Instance.Bingo());
        }
        
        protected void OnEnable() {    
            StateManager.SubscribeUntilDisable(this, state => {
                if (state.Bingo.Cards[Index] != null) {
                    UpdateBingoButton(state.Bingo.Cards[Index].DaubedIndexes);
                }
            });
        }
        
        private void UpdateBingoButton(List<int> daubedIndexes) {
            GetComponent<Button>().interactable = BingoConstants.WinningLines.Any(l => l.Intersect(daubedIndexes).Count() == l.Count);
        }
    }
}