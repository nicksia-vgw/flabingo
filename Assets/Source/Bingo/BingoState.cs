using System;
using System.Collections.Generic;
using Source.Bingo.Entities;
using Unidux;

namespace Source.Bingo {
    [Serializable]
    public class BingoState : StateBase {
        public List<int> CalledNumbers;
        public List<BingoCard> Cards = new List<BingoCard>(new BingoCard[4]);
    }
}