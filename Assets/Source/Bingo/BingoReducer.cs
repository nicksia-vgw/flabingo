using System.Collections.Generic;
using Plugins.FasterDux;
using Source.Bingo.Actions;
using Source.Bingo.Entities;

namespace Source.Bingo {
    public class BingoReducer {
        public static readonly List<IActionMapping<BingoState>> Mappings = new List<IActionMapping<BingoState>> {
            new ActionMapping<BingoState, UpdateCalledNumbersAction>(UpdateCalledNumbers),
            new ActionMapping<BingoState, UpdateBingoCardNumbersAction>(UpdateBingoCardNumbers)
        };

        private static BingoState UpdateBingoCardNumbers(BingoState state, UpdateBingoCardNumbersAction action) {
            state.Cards[0] = new BingoCard {Numbers = action.StartingNumbers};
            return state;
        }

        private static BingoState UpdateCalledNumbers(BingoState state, UpdateCalledNumbersAction action) {
            state.CalledNumbers = action.CalledNumbers;
            return state;
        }
    }
}