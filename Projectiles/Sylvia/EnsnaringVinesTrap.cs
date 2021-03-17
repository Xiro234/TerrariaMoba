using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace TerrariaMoba.Projectiles.Sylvia {
    public class EnsnaringVinesTrap : ModProjectile {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("EnsnaringVines");
        }
        
        public int TrapDuration { get; set; }

        public override void SetDefaults() {
            projectile.friendly = true;
            projectile.width = 34;
            projectile.height = 26;
            projectile.tileCollide = false;
        }
        
        public override void AI() {
            if ((int)projectile.ai[0] == 0) {
                Main.PlaySound(SoundID.Grass, projectile.position);
                for (int i = 0; i < 20; i++) {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                        0, 150, Color.LightGreen, 0.7f);
                }
            }
            projectile.ai[0] += 1f;
            
            /*
            var player = Main.player[projectile.owner].GetModPlayer<MobaPlayer>();
            //Venus Flytrap
            if (player.MyCharacter.talentArray[2, 2]) {
                if (projectile.ai[0] >= 540) {
                    projectile.Kill();
                }
            }
            else {
            */
            
            if ((int)projectile.ai[0] == TrapDuration) {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft) {
            Main.PlaySound(SoundID.Grass, projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }
     }
}