using System;
using System.Collections.Generic;
using Source.Bingo.Entities;
using Unidux;

namespace Source.Bingo {
    [Serializable]
    public class PlayerState : StateBase {
        public Dictionary<string, long> Currencies = new Dictionary<string, long> {
            {"gold", 0},
            {"bingo", 0},
            {"bingoPremium", 0},
        };
    }
}