using System;
using Terraria;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Flibnob {
    public class BattleHardened : Ability {
        private int oldDefBoost;
        public BattleHardened(Player myPlayer) : base(myPlayer) {
            Type = AbilityType.Passive;
            Name = "Battle Hardened";
            IsActive = true;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobTrait");
            oldDefBoost = 0;
        }

        public override void Using() {
            float change = (float) (player.statLifeMax2 - player.statLife) / player.statLifeMax2 * 100f;
            int defBoost = (int) Math.Floor(change / 2f);
            if (defBoost != oldDefBoost) {
                player.GetModPlayer<MobaPlayer>().armor += defBoost - oldDefBoost;
                oldDefBoost = defBoost;
            }
        }
    }
}