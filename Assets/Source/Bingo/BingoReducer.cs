using System.Collections.Generic;
using Plugins.FasterDux;
using Source.Bingo.Actions;
using Source.Bingo.Entities;

namespace Source.Bingo {
    public class BingoReducer {
        public static readonly List<IActionMapping<BingoState>> Mappings = new List<IActionMapping<BingoState>> {
            new ActionMapping<BingoState, CalledNumbersUpdateAction>(UpdateCalledNumbers),
            new ActionMapping<BingoState, CardUpdateNumbersAction>(UpdateBingoCardNumbers),
            new ActionMapping<BingoState, CardDaubNumberAction>(CardDaubNumber)
        };

        private static BingoState CardDaubNumber(BingoState state, CardDaubNumberAction action) {
            BingoCard card = state.Cards[action.CardIndex];
            
            int attemptedNumber = card.Numbers[action.NumberIndex];
            if (state.CalledNumbers.Contains(attemptedNumber)) {
                card.DaubedIndexes.Add(action.NumberIndex);
            }
  
//            if (_hacksEnabled || _knownNumbers.Contains(clickedNumber)) {
//                if (WinningLines.Any(l => l.Intersect(_clickedNumbers).Count() == l.Count)) {
//                    BingoButton.interactable = true;
//                }
//            }
        return state;
        }

        private static BingoState UpdateBingoCardNumbers(BingoState state, CardUpdateNumbersAction action) {
            state.Cards[0] = new BingoCard {Numbers = action.StartingNumbers};
            return state;
        }

        private static BingoState UpdateCalledNumbers(BingoState state, CalledNumbersUpdateAction action) {
            state.CalledNumbers = action.CalledNumbers;
            return state;
        }
    }
}