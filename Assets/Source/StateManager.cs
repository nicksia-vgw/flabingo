using System;
using Unidux;
using UniRx;
using UnityEngine;

namespace Source {
    public sealed class StateManager : SingletonMonoBehaviour<StateManager>, IStoreAccessor {
        private Store<State> _store;
        public TextAsset InitialStateJson;

        public static State State => Store.State;

        public static Subject<State> Subject => Store.Subject;

        private static State InitialState => Instance.InitialStateJson != null
            ? JsonUtility.FromJson<State>(Instance.InitialStateJson.text)
            : new State();

        public static Store<State> Store {
            get {
                return Instance._store = Instance._store ?? new Store<State>(InitialState, new StateReducer.Reducer());
            }
        }

        public IStoreObject StoreObject => Store;

        public static object Dispatch<TAction>(TAction action) {
            return Store.Dispatch(action);
        }

        public static void Subscribe(Component subscriber, Action<State> action) {
            Subject
                .StartWith(State)
                .Subscribe(action)
                .AddTo(subscriber);
        }
        
        public static void SubscribeUntilDisable(Component subscriber, Action<State> action) {
            Subject
                .TakeUntilDisable(subscriber)
                .StartWith(State)
                .Subscribe(action)
                .AddTo(subscriber);
        }

        private void Update() {
            Store.Update();
        }
    }
}