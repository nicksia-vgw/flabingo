using Plugins.FasterDux;
using Source.Bingo;
using Source.Player;
using Unidux;

namespace Source {
    public class StateReducer {
        public class Reducer : ReducerBase<State, IAction>
        {
            private readonly Reducer<BingoState> _bingoReducer = new Reducer<BingoState>(BingoReducer.Mappings);
            private readonly Reducer<PlayerState> _playerReducer = new Reducer<PlayerState>(PlayerReducer.Mappings);

            
            public override State Reduce(State state, IAction action) {
                return new State {
                    Bingo = _bingoReducer.Reduce(state.Bingo, action),
                    Player = _playerReducer.Reduce(state.Player, action)
                };
            }
        }
    }
}