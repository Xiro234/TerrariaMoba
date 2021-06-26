using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles.Jorm;
using TerrariaMoba.StatusEffects;

namespace TerrariaMoba.Abilities.Jorm {
    public class Hammerfall : Ability {
        public Hammerfall() : base("Hammerfall", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public const float BIGHAMMER_SPEED = 7f;
        public const float BIGHAMMER_DAMAGE = 700f;
        public const float BIGHAMMER_HEIGHT = -400f;
        public const int STUN_DURATION = 150;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                int dir = User.direction;
                Vector2 velocity = dir < 0 ? new Vector2(-BIGHAMMER_SPEED, BIGHAMMER_SPEED) : new Vector2(BIGHAMMER_SPEED);
                Vector2 spawnLoc = new Vector2(User.Top.X, User.Top.Y + BIGHAMMER_HEIGHT);
                
                Projectile.NewProjectileDirect(spawnLoc, velocity, 
                    TerrariaMoba.Instance.ProjectileType("HammerfallProj"), (int)BIGHAMMER_DAMAGE, 0, User.whoAmI, dir);

                Main.PlaySound(SoundID.Item1, User.Center);
            }
        }
        
        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.modProjectile;
            HammerfallProj trap = modProjectile as HammerfallProj;
            if (trap != null) {
                StatusEffectManager.AddEffect(target, new FunStun(STUN_DURATION, true));
            }
        }
    }
}