using System;
using Unidux;

namespace Plugins.FasterDux {
	public interface IActionMapping<TState> {
		Type ActionType();
		Func<TState, IAction, TState> Reducer();
	}

	public class ActionMapping<TState, TActionType> : IActionMapping<TState> where TActionType : class {
		private readonly Func<TState, TActionType, TState> _reducer;

		public ActionMapping(Func<TState, TActionType, TState> reducer) {
			_reducer = reducer;
		}

		public Type ActionType() => typeof(TActionType);
		public Func<TState, IAction, TState> Reducer() => (state, action) => _reducer(state, action as TActionType);
	}
}