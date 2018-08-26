using System.Collections.Generic;
using Unidux;

namespace Source.Bingo.Actions {
    public class CardStartAction : IAction {
        public int CardIndex;
        public List<int> StartingNumbers;
    }
}