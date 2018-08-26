using System.Collections.Generic;
using Plugins.FasterDux;
using Source.Bingo.Actions;
using Source.Bingo.Entities;

namespace Source.Bingo {
    public class BingoReducer {
        private const int CARD_MIDDLE_INDEX = 12;
        public static readonly List<IActionMapping<BingoState>> Mappings = new List<IActionMapping<BingoState>> {
            new ActionMapping<BingoState, CalledNumbersUpdateAction>(UpdateCalledNumbers),
            new ActionMapping<BingoState, CardStartAction>(StartCard),
            new ActionMapping<BingoState, CardDaubNumberAction>(CardDaubNumber),
            new ActionMapping<BingoState, CardBingoAction>(CardBingo),
            new ActionMapping<BingoState, ResetGameAction>(ResetGame),
            

        };

        private static BingoState ResetGame(BingoState state, ResetGameAction action) {
            state.Cards[0] = new BingoCard();
            state.Cards[0].DaubedIndexes.Add(CARD_MIDDLE_INDEX);

            return state;
        }

        private static BingoState CardBingo(BingoState state, CardBingoAction action) {
            throw new System.NotImplementedException();
        }

        private static BingoState CardDaubNumber(BingoState state, CardDaubNumberAction action) {
            BingoCard card = state.Cards[action.CardIndex];
            
            int attemptedNumber = card.Numbers[action.NumberIndex];
            if (true || state.CalledNumbers.Contains(attemptedNumber)) {
                card.DaubedIndexes.Add(action.NumberIndex);
            }
  
            return state;
        }

        private static BingoState StartCard(BingoState state, CardStartAction action) {
            state.Cards[0] = new BingoCard {Numbers = action.StartingNumbers};
            state.Cards[0].DaubedIndexes.Add(CARD_MIDDLE_INDEX);
            return state;
        }

        private static BingoState UpdateCalledNumbers(BingoState state, CalledNumbersUpdateAction action) {
            state.CalledNumbers = action.CalledNumbers;
            return state;
        }
    }
}