using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Packets;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.UI;

namespace TerrariaMoba.Abilities {
    [Serializable]
    public class Ability {
        public string Name = "";
        public int ChannelTime = 0;
        public int Cooldown = 0;
        public int Timer = 0;
        public bool IsActive = false;
        public bool CanUse = false;
        public int ResourceCost = 0;
        public AbilityType Type = AbilityType.Active;
        public Texture2D Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        public Player player;

        public Ability() {
            Name = "";
            ChannelTime = 0;
            Cooldown = 0;
            IsActive = false;
            CanUse = false;
            ResourceCost = 0;
            Type = AbilityType.Active;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }

        public Ability(Player myPlayer) {
            player = myPlayer;
        }

        public virtual void Start() {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            if (Cooldown == 0 && mobaPlayer.customStats.currentResource >= ResourceCost) {
                mobaPlayer.customStats.currentResource -= ResourceCost;
                OnCast();
                if (Main.netMode == NetmodeID.MultiplayerClient) {
                    Sync();
                }
            }
        }
        
        public virtual void OnCast() { }
        public virtual void InUse() { }
        public virtual void OnEnd() { }

        public virtual void Sync(int fromWho = -1, int toWho = -1) {
            Packets.SyncAbilitiesPacket.Write(this);
        }
    }
}