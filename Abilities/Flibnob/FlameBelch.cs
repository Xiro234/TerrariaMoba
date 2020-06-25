using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Flibnob
{
    public class FlameBelch : Ability
    {
        public FlameBelch(Player myPlayer) : base(myPlayer) {
            Name = "Flame Belch";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobAbilityOne");
        }
        
        public override void Cast()
        {
            Timer = (3 * 60) + 1;
            IsActive = true;
            player.AddBuff(BuffType<Buffs.Channeling>(), Timer);
        }

        public override void Using() {
            Timer--;
            if (Timer % 60 == 0) {
                Vector2 position = Main.LocalPlayer.Center;
                Vector2 playerToMouse = Main.MouseWorld - position;
                double mag = Math.Sqrt(playerToMouse.X * playerToMouse.X + playerToMouse.Y * playerToMouse.Y);
                float dirX = (float)(playerToMouse.X * (6.0 / mag));
                float dirY = (float)(playerToMouse.Y * (6.0 / mag));
                Vector2 vel = new Vector2(dirX, dirY);
                
                Main.PlaySound(SoundID.DD2_OgreAttack, player.Center);
                Projectile proj = Main.projectile[Projectile.NewProjectile(position, vel, 
                    TerrariaMoba.Instance.ProjectileType("FlameBelchSpawner"), 50, 0, Main.LocalPlayer.whoAmI, 0f)];
            }
            if (Timer == 0) {
                End();
            }
        }

        public override void End()
        {
            Timer = 0;
            IsActive = false;
            Cooldown = 10 * 60;
        }
    }
}