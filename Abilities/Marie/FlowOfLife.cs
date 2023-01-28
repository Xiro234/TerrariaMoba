using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Marie {
    public class FlowOfLife : Ability {
        public FlowOfLife(Player player) : base(player, "Flow of Life", 240, 100, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieAbilityTwo").Value; }

        public const int FLOW_HEAL = 300;
        public const int FLOW_CASTTIME = 120;
        public const float FLOW_RANGE = 200f;

        public int Timer = 0;

        public override void OnCast() {
            Timer = FLOW_CASTTIME;
            IsActive = true;
        }

        public override void WhileActive() {
            Timer--;
            for (int d = 0; d < 6; d++) {
                Dust.NewDust(User.position, User.width, User.height, DustID.Water_GlowingMushroom, 0f, 0f, 150, default(Color), 1.5f);
            }
            if (Timer == 0) {
                TimeOut();
            }
        }

        //TODO - Might need syncing to ensure HP bars are updated but might be tripping
        public override void TimeOut() {
            IsActive = false;
            float closestDist  = float.MaxValue;
            int closestPlayerID = -1;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length();
                if (plr.active && plr.team == User.team && dist <= FLOW_RANGE && i != User.whoAmI) {
                    closestDist = dist < closestDist ? dist : closestDist;
                    closestPlayerID = i;
                }
            }
            
            if (closestPlayerID != -1) {
                Player plr = Main.player[closestPlayerID];
                plr.statLife += FLOW_HEAL;
                CombatText.NewText(plr.Hitbox, Color.CornflowerBlue, FLOW_HEAL, true);
                SoundEngine.PlaySound(SoundID.Item4, plr.Center);
                for (int d = 0; d < 30; d++) {
                    Dust.NewDust(plr.position, plr.width, plr.height, DustID.Water_GlowingMushroom, 0f, 0f, 150, default(Color), 1.5f);
                }
                CooldownTimer = BaseCooldown;
            } else {
                Main.NewText("Could not find a player in range to heal!");
                User.GetModPlayer<MobaPlayer>().CurrentResource += BaseResourceCost / 2;
                CooldownTimer = BaseCooldown / 2;
            }
        }
    }
}