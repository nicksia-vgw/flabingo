using System.Collections.Generic;
using Plugins.FasterDux;
using Source.Bingo.Actions;

namespace Source.Bingo {
    public class BingoReducer {
        public static readonly List<IActionMapping<BingoState>> Mappings = new List<IActionMapping<BingoState>> {
            new ActionMapping<BingoState, UpdateCalledNumbersAction>(UpdateCalledNumbers),
        };

        private static BingoState UpdateCalledNumbers(BingoState state, UpdateCalledNumbersAction action) {
            state.CalledNumbers = action.CalledNumbers;
            return state;
        }
    }
}