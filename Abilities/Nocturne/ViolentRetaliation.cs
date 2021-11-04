using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Nocturne;

namespace TerrariaMoba.Abilities.Nocturne {
    public class ViolentRetaliation : Ability {
        public ViolentRetaliation() : base("Violent Retaliation", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public const int GUARD_DURATION = 60;
        
        //TODO - Slow any attacker that hits nocturne with the effect up after effect duration (probably a talent)
        //TODO - Grant bonus damage for every instance of damage taken during effect after effect duration

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                StatusEffectManager.AddEffect(User, new TitaniumGuardEffect(GUARD_DURATION, false));
            }
        }
    }
}