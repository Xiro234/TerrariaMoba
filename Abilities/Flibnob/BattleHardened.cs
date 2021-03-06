/*using System;
using Terraria;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Flibnob {
    [Serializable]
    public class BattleHardened : Ability {
        private int oldDefBoost;
        public BattleHardened(Player myPlayer) : base(myPlayer) {
            AbilityType = Enums.AbilityType.Passive;
            Name = "Battle Hardened";
            IsActive = true;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobTrait");
            oldDefBoost = 0;
        }

        public override void WhileActive() {
            float change = (float) (User.statLifeMax2 - User.statLife) / User.statLifeMax2 * 100f;
            int defBoost = (int) Math.Floor(change / 2f);
            if (defBoost != oldDefBoost) {
                User.GetModPlayer<MobaPlayer>().armor += defBoost - oldDefBoost;
                oldDefBoost = defBoost;
            }
        }
    } }*/