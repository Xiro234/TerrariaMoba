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
        public const int SPORE_DAMAGE = 90;
        public const int SPORE_DELAY = 90;
        public const int SPORE_LIFEITME = 60;

        public override void OnCast() {
            if (Main.myPlayer == User.whoAmI) {
                float closestDist = float.MaxValue;
                int closestPlayerID = -1;
                for (int i = 0; i < Main.maxPlayers; i++) {
                    Player plr = Main.player[i];
                    float dist = (Main.MouseWorld - plr.Center).Length();
                    if (plr.active && plr.team == User.team && dist < closestDist) {
                        closestDist = dist;
                        closestPlayerID = i;
                    }
                }

                if (closestPlayerID != -1) {
                    Player plr = Main.player[closestPlayerID];
                    StatusEffectManager.AddEffect(plr, new FungalArmorEffect(ATTACK_DAMAGE_REDUCTION, SPORE_DAMAGE, SPORE_DELAY, SPORE_LIFEITME, ARMOR_DURATION, false, User.whoAmI));
                    SoundEngine.PlaySound(SoundID.Item4, plr.Center);
                } else {
                    if (Main.netMode != NetmodeID.Server) {
                        Main.NewText("FungalArmor.cs: This should never happen.");
                    }
                }
            }
        }
    }
}