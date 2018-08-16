using System.Collections.Generic;
using System.Linq;
using Unidux;

namespace Plugins.FasterDux {
    public class Reducer<TState> where TState : StateBase {
        private readonly List<IActionMapping<TState>> _actionMap;

        public Reducer(List<IActionMapping<TState>> actionMap) {
            _actionMap = actionMap;
        }

        public TState Reduce(TState state, IAction action) {
            return _actionMap
                .Where(m => m.ActionType() == action.GetType())
                .Aggregate(state, (s, m) => m.Reducer()(s, action));
        }
    }
}