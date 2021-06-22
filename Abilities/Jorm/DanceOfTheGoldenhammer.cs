using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles.Jorm;

namespace TerrariaMoba.Abilities.Jorm {
    public class DanceOfTheGoldenhammer : Ability {
        public DanceOfTheGoldenhammer() : base("Dance of the Goldenhammer", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - 4 hammers spin around him, damage and daze on hit (they break on collide).
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile proj = Projectile.NewProjectileDirect(User.Center, Vector2.Zero, 
                    TerrariaMoba.Instance.ProjectileType("SpinningHammer"), 0, 0, User.whoAmI);
                Main.PlaySound(SoundID.Item1, User.Center);

                SpinningHammer hammer = proj.modProjectile as SpinningHammer;
            }
        }
    }
}