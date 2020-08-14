using System;
using Terraria;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Flibnob {
    public class BattleHardened : Ability {
        public BattleHardened(Player myPlayer) : base(myPlayer) {
            Type = AbilityType.Passive;
            Name = "Battle Hardened";
            IsActive = true;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobTrait");
        }

        public override void Using() {
            float change = -100f * ((player.statLife - player.statLifeMax2) / (float) player.statLifeMax2);
            int defBoost = (int) Math.Floor(change / 2f);
            player.GetModPlayer<MobaPlayer>().armor = defBoost;
        }
    }
}