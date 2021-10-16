using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class SealOfHephaesta : Ability {
        public SealOfHephaesta() : base("Seal of Hephaesta", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        public const float SEAL_HEAL_RANGE = 200f;
        public const int SEAL_HEAL_AMOUNT = 200;
        public const int INTERNAL_CAST_TIME = 60;
        public int timer;
        
        public override void OnCast() {
            timer = INTERNAL_CAST_TIME;
            IsActive = true;
        }

        public override void WhileActive() {
            timer--;
            for (int d = 0; d < 2; d++) {
                Dust.NewDust(User.position, User.width, User.height, 269, 0, 0, 200);
            }
            if (timer == 0) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            IsActive = false;
            float closestDist  = float.MaxValue;
            int closestPlayerID = -1;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length();
                if (plr.active && plr.team == User.team && dist <= SEAL_HEAL_RANGE && i != User.whoAmI) {
                    closestDist = dist < closestDist ? dist : closestDist;
                    closestPlayerID = i;
                }
            }

            if (closestPlayerID != -1) {
                Player plr = Main.player[closestPlayerID];
                plr.statLife += SEAL_HEAL_AMOUNT;
                CombatText.NewText(Main.player[closestPlayerID].Hitbox, Color.Goldenrod, SEAL_HEAL_AMOUNT, true);
                SoundEngine.PlaySound(SoundID.Item4, plr.Center);
                for (int d = 0; d < 8; d++) {
                    Dust.NewDust(plr.position, plr.width, plr.height, 269, 0f, 0f, 200, default(Color), 1.5f);
                }
            } else {
                Main.NewText("Could not find a player in range to heal!");
            }
        }
        
        //TODO - refactor to give allies jorms armor/mr and jorm takes 25% of all premitigated dmg / is an effect
    }
}