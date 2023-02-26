using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles.Osteo;
using TerrariaMoba.Projectiles;
using Terraria;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.StatusEffects.Osteo {
    public class MucormycosisEffect : StatusEffect, IKill {
        public override string DisplayName { get => "Fungal Armor"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        private int MucorSporeDamage;
        private int MucorSporeDuration;

        public MucormycosisEffect() { }
        public MucormycosisEffect(int msdmg, int msdur, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            MucorSporeDamage = msdmg;
            MucorSporeDuration = msdur;
        }

        public void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 velocity = new Vector2(hitDirection * -1, 0);
                Projectile proj = Projectile.NewProjectileDirect(new EntitySource_StatusEffect(User, this),
                        User.position, velocity, ModContent.ProjectileType<MucormycosisSpore>(), 1, 0, User.whoAmI);
                TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: MucorSporeDamage);

                MucormycosisSpore spore = proj.ModProjectile as MucormycosisSpore;
                if (spore != null) {
                    spore.SporeLifetime = MucorSporeDuration;
                }
            }
        }
    }
}
