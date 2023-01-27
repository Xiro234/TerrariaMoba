using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class SearingBond : Ability {
        public SearingBond(Player player) : base(player, "Searing Bond", 0, 0, AbilityType.Passive) {
        }

        public override Texture2D Icon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobTrait").Value;
        }

        public const int BASE_ARMOR_GAIN = 5;
        public const int BURN_BASE_DURATION = 120;
        public const float BUFF_RANGE = 50f;
        public int finalStacks;

        public override void WhileActive() {

            int total = 0;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length();
                if (plr.active && plr.team != User.team && dist <= BUFF_RANGE && i != User.whoAmI) { // && plr.whatever.hasEffect("fire")
                    total++;
                }
            }
            finalStacks = total;

            StatusEffectManager.AddEffect(User, new SearingBondEffect(User.whoAmI, total, BASE_ARMOR_GAIN, BURN_BASE_DURATION, 60, false));
        }
    }
}