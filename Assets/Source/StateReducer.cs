using Plugins.FasterDux;
using Source.Bingo;
using Unidux;

namespace Source {
    public class StateReducer {
        public class Reducer : ReducerBase<State, IAction>
        {
            private readonly Reducer<BingoState> _bingoReducer = new Reducer<BingoState>(BingoReducer.Mappings);
            
            public override State Reduce(State state, IAction action) {
                return new State {
                    Bingo = _bingoReducer.Reduce(state.Bingo, action)
                };
            }
        }
    }
}