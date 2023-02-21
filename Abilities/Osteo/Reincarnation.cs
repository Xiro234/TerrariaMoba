using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Osteo {
    public class Reincarnation : Ability, IOnRespawn, IKill {
        public Reincarnation(Player player) : base(player, "Reincarnation", 360, 0, AbilityType.Passive) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoUltimateTwo").Value; }

        public const int RESPAWN_DELAY = 150;
        public const float RESPAWN_HEALTH_MODIFIER = 0.5f;
        private int Timer;
        private Vector2 DeathPosition;

        public override void WhileActive() {
            if (User.dead && Timer == 0) {
                User.SpawnX = (int)(DeathPosition.X / 16.0f);
                User.SpawnY = (int)(DeathPosition.Y / 16.0f);
                User.Spawn(PlayerSpawnContext.ReviveFromDeath);
            } else if (User.dead) {
                Timer--;
            }
        }
        public void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            DeathPosition = User.position;
            Timer = RESPAWN_DELAY;
        }
        public bool OnRespawn() {
            if (CooldownTimer == 0) {
                User.GetModPlayer<MobaPlayer>().HealMe((int)(User.statLifeMax2 * RESPAWN_HEALTH_MODIFIER) - User.statLife, false);
                CooldownTimer = BaseCooldown;
                return false;
            } else {
                return true;
            }
        }
    }
}