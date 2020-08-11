using Terraria;
using TerrariaMoba.Enums;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Marie {
    public class Floodboost : Ability {
        public int FloodboostTimer = 420;
        public Floodboost(Player myPlayer) : base(myPlayer) {
            Type = AbilityType.Passive;
            Name = "Floodboost";
            IsActive = true;
            Timer = FloodboostTimer;
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieTrait");
        }

        public override void Using() {
            Timer--;
            if (Timer == 120) {
                player.AddBuff(BuffType<Buffs.Floodboost>(), 3 * 60);
            }
            if (Timer == 0) {
                End();
            }
        }

        public override void End() {
            Timer = FloodboostTimer;
        }
    }
}