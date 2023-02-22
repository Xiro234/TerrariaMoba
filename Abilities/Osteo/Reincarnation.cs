using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Abilities.Osteo {
    public class Reincarnation : Ability, IPreKill, ISetControls {
        public Reincarnation(Player player) : base(player, "Reincarnation", 720, 0, AbilityType.Active) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoUltimateTwo").Value; }

        public const int RESPAWN_DELAY = 150;
        public const float RESPAWN_HEALTH_MODIFIER = 0.5f;
        private int Timer;

        public override void WhileActive() {
            if (Timer == 0) {
                IsActive = false;
                SoundEngine.PlaySound(SoundID.Zombie95, User.Center);
                User.GetModPlayer<MobaPlayer>().HealMe(1000, true);
                User.immune = false;
                CooldownTimer = BaseCooldown;
            } else {
                Timer--;
                User.immune = true;
            }
        }

        public bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if (CooldownTimer == 0) {
                User.statLife = 1;
                SoundEngine.PlaySound(SoundID.Zombie105, User.Center);
                User.immune = true;
                Timer = RESPAWN_DELAY;
                IsActive = true;
                playSound = false;
                genGore = false;
                return false;
            } else {
                return true;
            }
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