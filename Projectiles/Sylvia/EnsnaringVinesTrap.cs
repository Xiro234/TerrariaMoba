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
            Projectile.friendly = true;
            Projectile.width = 34;
            Projectile.height = 26;
            Projectile.tileCollide = false;
        }
        
        public override void AI() {
            if ((int)Projectile.ai[0] == 0) {
                SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
                for (int i = 0; i < 20; i++) {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, 0,
                        0, 150, Color.LightGreen, 0.7f);
                }
            }
            Projectile.ai[0] += 1f;
            
            /*
            var Player = Main.player[Projectile.owner].GetModPlayer<MobaPlayer>();
            //Venus Flytrap
            if (Player.MyCharacter.talentArray[2, 2]) {
                if (Projectile.ai[0] >= 540) {
                    Projectile.Kill();
                }
            }
            else {
            */
            
            if ((int)Projectile.ai[0] == TrapDuration) {
                Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
            for (int i = 0; i < 20; i++) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, 0,
                    0, 150, Color.LightGreen, 0.7f);
            }
        }
     }
}