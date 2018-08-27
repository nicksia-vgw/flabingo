using System;
using Source.Bingo;
using Unidux;
using UnityEngine;

namespace Source {
    [Serializable]
    public class State : StateBase {
        public BingoState Bingo;
        public PlayerState Player;

        public State() {
            Bingo = new BingoState();
            Player = new PlayerState();
        }
    }
}