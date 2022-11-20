using System.Collections.Generic;
using Rocket.API;
using SDG.Unturned;
using Rocket.Unturned.Player;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace AntiKos.Commands
{
    class TimeTillClearCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "timetillclear";

        public string Help => "Displays how much time is left until clear of users kills";

        public string Syntax => "<player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions
        {
            get { return new List<string>() { "antikos.timetillclear" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = UnturnedPlayer.FromName(command[0]);

            if (player != null)
            {
                caller.sendMessage($"[<color=green> AntiKos </color>] {AntiKosPlugin.timeTillClear(player.CSteamID)} is left until clear of {player.DisplayName}'s kills");
            }
            else
            {
                caller.sendMessage($"[<color=green> AntiKos </color>] Player not found");
            }
        }
    }
}
