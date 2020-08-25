using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Packets;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.UI;

namespace TerrariaMoba.Abilities {
    public class Ability {
        public string Name = "";
        public int Cooldown = 0;
        public int Timer = 0;
        public bool IsActive = false;
        public bool NeedsSyncing = false;
        public int ResourceCost = 0;
        public AbilityType Type = AbilityType.Active;
        public Texture2D Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        public Player player;

        public Ability() {
            Name = "";
            Cooldown = 0;
            IsActive = false;
            ResourceCost = 0;
            Type = AbilityType.Active;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }

        public Ability(Player myPlayer) {
            player = myPlayer;
        }

        public virtual void Cast() { }
        public virtual void Using() { CheckSync(); }
        public virtual void End() { }
        public virtual void CheckSync() { }
        public virtual void ReadAbility(MemoryStream stream) { }

        public virtual byte[] WriteAbility() {
            return null;
        }

        public virtual void DrawSelf(SpriteBatch spriteBatch, UIAbilityIcon abilityIcon) { }
    }
}