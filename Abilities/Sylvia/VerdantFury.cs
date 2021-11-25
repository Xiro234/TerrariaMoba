using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class VerdantFury : Ability {
        public VerdantFury(Player player) : base(player, "Verdant Fury", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaAbilityThree").Value; }
        
        public const float ATKSPD_MODIFIER = 1.3f;
        public const float ATKVEL_MODIFIER = 1.3f;
        public const int BUFF_DURATION = 240;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                StatusEffectManager.AddEffect(User, new VerdantFuryEffect(BUFF_DURATION, ATKSPD_MODIFIER, ATKVEL_MODIFIER, false));
            }
        }
    }
}