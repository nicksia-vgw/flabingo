using System;
using System.Collections.Generic;
using Unidux;

namespace Source.Bingo {
    [Serializable]
    public class BingoState : StateBase {
        public List<int> CalledNumbers;
    }
}