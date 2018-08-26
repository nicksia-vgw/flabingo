using System;
using Unidux;

namespace Source.Bingo.Actions {
    [Serializable]
    public class CardDaubNumberAction : IAction {
        public int CardIndex;
        public int NumberIndex;
    }
}