using Rocket.API;
using Rocket.Core.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Rocket.Unturned.Items;
using SDG.Unturned;

namespace AntiKos
{
    public class Configuration : IRocketPluginConfiguration
    {
        public int KillCounterExpireTimeSeconds;
        public int KillsToGetKicked;
        public string KickMessage;
        public bool EnableKillBeforeBan;
        public bool BanEnabled;
        public string BanMessage;
        public int KillsToGetBanned;
        public uint BanDurationSeconds;

        public void LoadDefaults()
        {
            KillCounterExpireTimeSeconds = 1800;
            KillsToGetKicked = 2;
            KickMessage = "[AntiKos] You have killed too many people. Further killing will result in ban";
            EnableKillBeforeBan = true;
            BanEnabled = true;
            BanMessage = "[AntiKos] You have killed too many people";
            KillsToGetBanned = 4;
            BanDurationSeconds = 43200;
        }
    }
}
