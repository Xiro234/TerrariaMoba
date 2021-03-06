/*using System;
using Terraria;
using TerrariaMoba.Enums;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Marie {
    [Serializable]
    public class Floodboost : Ability {
        public int FloodboostTimer = 420;
        public Floodboost(Player myPlayer) : base(myPlayer) {
            AbilityType = Enums.AbilityType.Passive;
            Name = "Floodboost";
            IsActive = true;
            Timer = FloodboostTimer;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieTrait");
        }

        public override void WhileActive() {
            Timer--;
            if (Timer == 120) {
                User.AddBuff(BuffType<Buffs.Floodboost>(), 3 * 60);
            }
            if (Timer == 0) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            Timer = FloodboostTimer;
        }
    }
}*/