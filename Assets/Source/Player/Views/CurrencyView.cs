using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Player.Views {
    public class CurrencyView : MonoBehaviour {
        public string CurrencyShortCode;
        
        private void OnEnable() {
            StateManager.SubscribeUntilDisable(this, state => {
                long rawValue = state.Player.Currencies[CurrencyShortCode];
                GetComponent<Text>().text = $"{rawValue:n0}";
            });
        }
    }
}
