using Terraria;
using static Terraria.ModLoader.ModContent;
using System;
using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Sylvia {
    public class VerdantFury : Ability {
        public VerdantFury(Player myPlayer) : base(myPlayer) {
            Name = "Verdant Fury";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaAbilityTwo");
        }

        public override void Cast() {
            player.AddBuff(BuffType<Buffs.VerdantFuryBuff>(), 60 * 4);
            Cooldown = 60 * 10;
        }
    }
}