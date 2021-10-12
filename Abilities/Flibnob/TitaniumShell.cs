using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class TitaniumShell : Ability {
        public TitaniumShell() : base("Titanium Shell", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Flibnob/FlibnobAbilityTwo").Value; }

        public const int SHELL_BASE_ARMOR = 10;
        public const float SHELL_BASE_MS_REDUCTION = 0.33f;
        public const int SHELL_BASE_DURATION = 240;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                StatusEffectManager.AddEffect(User, new TitaniumShellEffect(SHELL_BASE_DURATION, SHELL_BASE_ARMOR, SHELL_BASE_MS_REDUCTION));
            }
        }
    }
}