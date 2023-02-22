using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Osteo;

namespace TerrariaMoba.Abilities.Osteo {
    public class FungalArmor : Ability {
        public FungalArmor(Player player) : base(player, "Fungal Armor", 180, 25, AbilityType.Active) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityOne").Value; }

        public const float ATTACK_DAMAGE_REDUCTION = 0.40f;
        public const int ARMOR_DURATION = 360;
        public const int SPORE_DAMAGE = 69;
        public const int SPORE_DELAY = 90;

        public override void OnCast() {
            float closestDist = float.MaxValue;
            int closestPlayerID = -1;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (Main.MouseWorld - plr.Center).Length();
                if (plr.active && plr.team == User.team && dist < closestDist) {
                    if (Main.netMode != NetmodeID.Server) {
                        Main.NewText(Main.player[i].name + " has the distance: " + dist);
                    }
                    closestDist = dist;
                    closestPlayerID = i;
                }
            }

            if (Main.netMode != NetmodeID.Server) {
                Main.NewText("closestPlayerID: " + Main.player[closestPlayerID].name);
            }

            if (closestPlayerID != -1) {
                Player plr = Main.player[closestPlayerID];
                StatusEffectManager.AddEffect(plr, new FungalArmorEffect(ATTACK_DAMAGE_REDUCTION, SPORE_DAMAGE, SPORE_DELAY, ARMOR_DURATION, false, User.whoAmI));
                SoundEngine.PlaySound(SoundID.Item4, plr.Center);
            } else {
                if (Main.netMode != NetmodeID.Server) {
                    Main.NewText("FungalArmor.cs: This should never happen.");
                }
            }
        }
    }
}