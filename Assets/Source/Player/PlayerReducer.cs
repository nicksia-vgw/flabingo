using System.Collections.Generic;
using GameSparks.Api.Responses;
using Plugins.FasterDux;
using Source.Bingo;
using Source.Bingo.Actions;
using Source.Bingo.Entities;
using Source.Player.Actions;

namespace Source.Player {
    public class PlayerReducer {
        public static readonly List<IActionMapping<PlayerState>> Mappings = new List<IActionMapping<PlayerState>> {
            new ActionMapping<PlayerState, AccountDetailsResponseAction>(OnAccountDetailsResponse),
        };

        private static PlayerState OnAccountDetailsResponse(PlayerState state, AccountDetailsResponseAction action) {
            state.Currencies["gold"] = action.AccountDetailsResponse.Currencies.GetNumber("gold") ?? 0;
            state.Currencies["bingo"] = action.AccountDetailsResponse.Currencies.GetNumber("bingo") ?? 0;
            state.Currencies["bingoPremium"] = action.AccountDetailsResponse.Currencies.GetNumber("bingoPremium") ?? 0;
            return state;
        }
    }
}