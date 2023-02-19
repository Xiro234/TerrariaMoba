using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class TitaniumShell : Ability {
        public TitaniumShell(Player player) : base(player, "Titanium Shell", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobAbilityTwo").Value; }

        public const int SHELL_ARMOR = 10;
        public const int SHELL_MR = 10;
        public const float SHELL_MS_MODIFIER = -0.90f;
        public const float SHELL_HEALING = 0.50f;
        public const int SHELL_DURATION = 180;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                StatusEffectManager.AddEffect(User, new TitaniumShellEffect(SHELL_ARMOR, SHELL_MR, SHELL_MS_MODIFIER, SHELL_HEALING, SHELL_DURATION, false, User.whoAmI));
            }
        }
    }
}