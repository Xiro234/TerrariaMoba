using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.StatusEffects.Osteo;
using TerrariaMoba.StatusEffects;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;

namespace TerrariaMoba.Abilities.Osteo {
    public class EyeOfFright : Ability, ISetControls {
        public EyeOfFright(Player player) : base(player, "Eye of Fright", 60, 0, AbilityType.Active) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityThree").Value; }

        public const int FEAR_DURATION = 120;
        public const float TARGET_VELOCITY_TO_OSTEO = 0.75f;
        public const float MAX_BLOCK_RANGE = 40f;
        private int Timer;

        public override void OnCast() {
            if (Main.myPlayer == User.whoAmI) {
                IsActive = true;
                SoundEngine.PlaySound(SoundID.AbigailSummon, User.Center);
                float closestDist = float.MaxValue;
                int closestPlayerID = -1;
                for (int i = 0; i < Main.maxPlayers; i++) {
                    Player plr = Main.player[i];
                    if (plr.active && plr.team != User.team) {
                        Vector2 dir = plr.Center - User.Center;

                        bool isFacingEnemy = Math.Sign(dir.X) == User.direction;

                        float dist = (User.Center - plr.Center).Length() / 16.0f;
                        if (dist < closestDist && dist < MAX_BLOCK_RANGE && isFacingEnemy) {
                            closestDist = dist;
                            closestPlayerID = i;
                        }
                    }
                }

                if (closestPlayerID != -1) {
                    Timer = FEAR_DURATION;
                    Player plr = Main.player[closestPlayerID];
                    StatusEffectManager.AddEffect(plr, new EyeOfFrightEffect(TARGET_VELOCITY_TO_OSTEO, FEAR_DURATION, false, User.whoAmI));
                    SoundEngine.PlaySound(SoundID.NPCDeath65, plr.Center);
                } else {
                    if (Main.netMode != NetmodeID.Server) {
                        Main.NewText("Eye of Fright: Couldn't find an enemy in range to target!");
                        IsActive = false;
                    }
                }
            }
        }

        public override void WhileActive() {
            if (Timer == 0) {
                TimeOut();
            } else {
                Timer--;
            }
        }

        public override void TimeOut() {
            IsActive = false;
        }

        public void SetControls() { 
            if (IsActive) {
                User.controlRight = false;
                User.controlLeft = false;
                User.controlJump = false;
                User.controlUp = false;
                User.controlDown = false;
                User.controlUseItem = false;
                User.controlHook = false;
                User.controlInv = false;
                User.controlMap = false;
            }
        }
    }
}