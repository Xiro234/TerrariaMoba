using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Jorm;

namespace TerrariaMoba.Abilities.Jorm {
    public class ShieldOfHephaesta : Ability {
        public ShieldOfHephaesta(Player player) : base(player, "Seal of Hephaesta", 240, 40, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Jorm/JormAbilityThree").Value; }

        public const float SHIELD_RANGE = 40f;
        public const float RANGE_TO_ABSORB = 40f;
        public const float BARRIER_ABSORB_MAG = 0.25f;
        public const int BARRIER_DURATION = 180;

        public const int INTERNAL_CAST_TIME = 60;
        public int timer;
        
        public override void OnCast() {
            timer = INTERNAL_CAST_TIME;
            IsActive = true;
            PaladinsResolve pr = User.GetModPlayer<MobaPlayer>().Hero.Trait as PaladinsResolve;
            if (pr != null) {
                pr.AddStack();
            }
        }

        public override void WhileActive() {
            timer--;
            for (int d = 0; d < 2; d++) {
                Dust.NewDust(User.position, User.width, User.height, DustID.Sandnado, 0, 0, 200);
            }
            if (timer == 0) {
                TimeOut();
            }
        }

        //TODO - Possibly convert to nearest ally to mouse.
        public override void TimeOut() {
            IsActive = false;
            float closestDist  = float.MaxValue;
            int closestPlayerID = -1;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length() / 16f;
                if (plr.active && plr.team == User.team && dist <= SHIELD_RANGE && i != User.whoAmI) {
                    closestDist = dist < closestDist ? dist : closestDist;
                    closestPlayerID = i;
                }
            }

            if (closestPlayerID != -1) {
                Player plr = Main.player[closestPlayerID];
                StatusEffectManager.AddEffect(plr, new HolyBarrier(RANGE_TO_ABSORB, BARRIER_ABSORB_MAG, BARRIER_DURATION, true, User.whoAmI));
                SoundEngine.PlaySound(SoundID.Item4, plr.Center);
                for (int d = 0; d < 8; d++) {
                    Dust.NewDust(plr.position, plr.width, plr.height, DustID.Sandnado, 0f, 0f, 200, default(Color), 1.5f);
                }
            } else {
                if (Main.netMode != NetmodeID.Server) {
                    Main.NewText("Could not find a player in range to give Holy Barrier!");
                }
                User.GetModPlayer<MobaPlayer>().CurrentResource += BaseResourceCost / 2;
                CooldownTimer = BaseCooldown / 2;
            }
        }
    }
}