using System;
using System.Timers;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using Rocket.API;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Items;
using Rocket.Unturned.Player;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Plugins;
using Rocket.Core.Logging;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using SDG.Unturned;
using Steamworks;
using SDG;
using UnityEngine;
using UnityEngine.Events;
using UP = Rocket.Unturned.Player.UnturnedPlayer;
using Rocket.API.Serialisation;
using Rocket.Unturned.Chat;
using SDG.Provider;
using Logger = Rocket.Core.Logging.Logger;

namespace AntiKos
{
    public class AntiKosPlugin : RocketPlugin<Configuration>
    {
        public static AntiKosPlugin Instance;
        public string Creator = "XXFOGS";
        public string PluginName = "AntiKos";
        public string Version = "1.0.0";

        public static IDictionary<CSteamID, Koser> KosDictionary = new Dictionary<CSteamID, Koser>();

        protected override void Load()
        {
            Instance = this;

            Logger.Log($"{PluginName} by {Creator} has been loaded! Version: {Version}");
            UnturnedPlayerEvents.OnPlayerDeath += onPlayerDeath;
            
        }

        protected override void Unload()
        {
            Logger.Log($"{PluginName} has been unloaded");
            UnturnedPlayerEvents.OnPlayerDeath -= onPlayerDeath;
        }

        void FixedUpdate()
        {
            foreach (var koser in KosDictionary.ToList())
            {
                if ((DateTimeOffset.Now.ToUnixTimeSeconds() - koser.Value.LastKillTime) > Configuration.Instance.KillCounterExpireTimeSeconds)
                {
                    KosDictionary.Remove(koser.Key);
                }
            }
        }

        private void onPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            if (UP.FromCSteamID(murderer) == null) return;
                    
            if (!UP.FromCSteamID(murderer).HasPermission("antikos.bypass"))
            {
                if (KosDictionary.ContainsKey(murderer))
                {
                    KosDictionary[murderer].Kills += 1;
                    KosDictionary[murderer].LastKillTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                }
                else
                {
                    KosDictionary.Add(murderer, new Koser(murderer, 1, DateTimeOffset.Now.ToUnixTimeSeconds()));
                }

                if (KosDictionary.ContainsKey(murderer))
                {
                    if (KosDictionary[murderer].Kills >= Configuration.Instance.KillsToGetKicked || KosDictionary[murderer].Kills >= Configuration.Instance.KillsToGetBanned)
                    {
                        if (KosDictionary[murderer].Kills >= Configuration.Instance.KillsToGetBanned && Configuration.Instance.BanEnabled)
                        {
                            if (Configuration.Instance.EnableKillBeforeBan) UP.FromCSteamID(murderer).Damage(byte.MaxValue, new Vector3(), EDeathCause.KILL, ELimb.SPINE, CSteamID.Nil);
                            KosDictionary.Remove(murderer);
                            UP.FromCSteamID(murderer).Ban(Configuration.Instance.BanMessage, Configuration.Instance.BanDurationSeconds);
                        }
                        else if (KosDictionary[murderer].Kills == Configuration.Instance.KillsToGetKicked)
                        {
                            UP.FromCSteamID(murderer).Kick(Configuration.Instance.KickMessage);
                        }
                    }
                }
            }
        }

        public static void clearKills(CSteamID player)
        {
            KosDictionary.Remove(player);
        }

        public static void clearAllKills()
        {
            KosDictionary.Clear();
        }

        public static int viewKills(CSteamID player)
        {
            return (KosDictionary.ContainsKey(player) ? KosDictionary[player].Kills : 0);
        }

        public static long timeTillClear(CSteamID player)
        {
            return (KosDictionary.ContainsKey(player) ? (Instance.Configuration.Instance.KillCounterExpireTimeSeconds - (DateTimeOffset.Now.ToUnixTimeSeconds() - KosDictionary[player].LastKillTime)) : 0);
        }
    }

    public class Koser
    {
        public CSteamID SteamID { get; set; }
        public int Kills { get; set; }
        public long LastKillTime { get; set; }

        public Koser(CSteamID steamid, int kills, long lastkilltime)
        {
            SteamID = steamid;
            Kills = kills;
            LastKillTime = lastkilltime;
        }
    }
}
