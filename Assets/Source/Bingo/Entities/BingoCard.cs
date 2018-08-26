using System;
using System.Collections.Generic;

namespace Source.Bingo.Entities {
    [Serializable]
    public class BingoCard {
        public List<int> Numbers = new List<int>();
        public List<int> DaubedIndexes = new List<int>();
    }
}