using System.Collections.Generic;
using Rocket.API;
using SDG.Unturned;
using UP = Rocket.Unturned.Player.UnturnedPlayer;

namespace AntiKos.Commands
{
    class ClearAllKillsCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "clearallkills";

        public string Help => "Clears everybodies kills";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions
        {
            get { return new List<string>() { "antikos.clearallkills" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            AntiKosPlugin.clearAllKills();
            caller.sendMessage($"[<color=green> AntiKos </color>] Cleared everybodies kills");
        }
    }
}
