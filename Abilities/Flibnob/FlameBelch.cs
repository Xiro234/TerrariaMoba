/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Players;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Flibnob {
    [Serializable]
    public class FlameBelch : Ability {
        public FlameBelch(Player myPlayer) : base(myPlayer) {
            Name = "Flame Belch";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobAbilityOne");
        }
        
        public override void OnCast() {
            Timer = (3 * 60) + 1;
            IsActive = true;
            User.AddBuff(BuffType<Buffs.Channeling>(), Timer);
        }

        public override void WhileActive() {
            Timer--;
            if (Timer % 60 == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    Vector2 position = User.Center;
                    Vector2 playerToMouse = Main.MouseWorld - position;
                    double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                    float dirX = (float)(playerToMouse.X * (6.0 / mag));
                    float dirY = (float)(playerToMouse.Y * (6.0 / mag));
                    Vector2 vel = new Vector2(dirX, dirY);
                
                    Main.PlaySound(SoundID.DD2_OgreAttack, User.Center);
                    Projectile.NewProjectile(position, vel,
                        TerrariaMoba.Instance.ProjectileType("FlameBelchSpawner"), 
                        (int)User.GetModPlayer<MobaPlayer>().FlibnobStats.A1FireballDmg.Value, 0, User.whoAmI);
                }
            }
            if (Timer == 0) {
                TimeOut();
            }
        }

        public override void TimeOut() {
            Timer = 0;
            IsActive = false;
            cooldownTimer = 10 * 60;
        }
    }
}*/