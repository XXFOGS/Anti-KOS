using System.Collections.Generic;
using Rocket.API;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace AntiKos.Commands
{
    class ClearKillsCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "clearkills";

        public string Help => "Clears individual kills of a person";

        public string Syntax => "<player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions
        {
            get { return new List<string>() { "antikos.clearkills" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = UnturnedPlayer.FromName(command[0]);

            if (player != null)
            {
                AntiKosPlugin.clearKills(player.CSteamID);
                caller.sendMessage($"[<color=green> AntiKos </color>] You have cleared kills of {player.DisplayName}");
            } else
            {
                caller.sendMessage($"[<color=green> AntiKos </color>] Player not found");
            }
        }
    }
}
