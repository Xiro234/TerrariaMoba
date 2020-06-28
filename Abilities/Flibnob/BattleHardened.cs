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
            float change = -1 * (player.statLife - player.statLifeMax2) / (float)player.statLifeMax2 * 100;
            int defBoost = (int) (1 * Math.Floor(change / 2f));
            player.GetModPlayer<MobaPlayer>().customStats.armor = defBoost;
        }
    }
}