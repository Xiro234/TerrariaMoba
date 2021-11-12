using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Nocturne;

namespace TerrariaMoba.Abilities.Nocturne {
    public class ViolentRetaliation : Ability {
        public ViolentRetaliation(Player player) : base(player, "Violent Retaliation", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public const int RETAL_DURATION = 60;
        public const float BUFF_MAGNITUDE = 0.04f;
        public const int BUFF_DURATION = 150;

        private int hitCount;
        private int Timer;
        
        //TODO - Slow any attacker that hits nocturne with the effect up after effect duration (probably a talent)
        //TODO - Grant bonus damage for every instance of damage taken during effect after effect duration

        public override void OnCast() {
            Timer = RETAL_DURATION;
            IsActive = true;
        }

        public override void WhileActive() {
            /*
             * if nocturne is hit
             * all damage reduced to 0
             * hitcount++
             *
             * also make him glow or overlay something on him
             */
            Timer--;
        }

        public override void TimeOut() {
            StatusEffectManager.AddEffect(User, new RetaliationDMGEffect(BUFF_MAGNITUDE, hitCount, BUFF_DURATION, true));
        }
    }
}