using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Nocturne;

namespace TerrariaMoba.Abilities.Nocturne {
    public class TitaniumGuard : Ability {
        public TitaniumGuard() : base("Titanium Guard", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public const int GUARD_DURATION = 300;
        
        //TODO - Remove effect on hit (if possible)
        //TODO - Grant MS on hit

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                StatusEffectManager.AddEffect(User, new TitaniumGuardEffect(GUARD_DURATION, false));
            }
        }
    }
}