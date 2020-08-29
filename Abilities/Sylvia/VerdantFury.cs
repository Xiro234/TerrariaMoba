﻿using Terraria;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Sylvia {
    public class VerdantFury : Ability {
        public VerdantFury(Player myPlayer) : base(myPlayer) {
            Name = "Verdant Fury";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaAbilityTwo");
        }

        public override void Cast() {
            player.AddBuff(BuffType<Buffs.VerdantFuryBuff>(), 60 * 4);
            Cooldown = 10 * 60;
        }
    }
}