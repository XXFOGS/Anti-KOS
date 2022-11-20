using System.Collections.Generic;
using Rocket.API;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace AntiKos.Commands
{
    class CheckKillsCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "checkkills";

        public string Help => "Tells you how many kills the plugin has recorded";

        public string Syntax => "<player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions
        {
            get { return new List<string>() { "antikos.checkkills" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = UnturnedPlayer.FromName(command[0]);

            if (player != null)
            {
                caller.sendMessage($"[<color=green> AntiKos </color>] Plugin has recorded {AntiKosPlugin.viewKills(player.CSteamID)} kills made by {player.DisplayName}");
            }
            else
            {
                caller.sendMessage($"[<color=green> AntiKos </color>] Player not found");
            }
        }
    }
}
