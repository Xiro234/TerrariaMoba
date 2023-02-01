using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class SearingBond : Ability {
        public SearingBond(Player player) : base(player, "Searing Bond", 0, 0, AbilityType.Active) { }

        public override Texture2D Icon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobTrait").Value;
        }

        public const int BASE_ARMOR_GAIN = 5;
        public const int BURN_BASE_DURATION = 120;
        public const float BUFF_RANGE = 20f;
        private int finalStacks;
        private int timer;

        public override void OnCast() {
            if (IsActive) {
                IsActive = false;
            } else {
                IsActive = true;
                timer = 0;
            }
        }

        public override void WhileActive() {
            int total = 0;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length() / 16.0f;
                if (plr.active && plr.team != User.team && dist <= BUFF_RANGE && i != User.whoAmI) { // && plr.whatever.hasEffect("fire")
                    total++;
                }
            }
            finalStacks = total;

            if (timer == 0) {
                StatusEffectManager.AddEffect(User, new SearingBondEffect(User.whoAmI, finalStacks, BASE_ARMOR_GAIN, BURN_BASE_DURATION, 60, false));
                timer = 60;
            } else {
                timer--;
            }
        }
    }
}