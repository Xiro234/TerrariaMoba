using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.GenericEffects {
    public abstract class Slow : StatusEffect, IResetEffects {
        private float modifier;
        
        public Slow() { }

        public Slow(float magnitude, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
            modifier = magnitude;
        }
        
        public void ResetEffects() {
            User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.MovementSpeed *= 1-modifier;
            User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.JumpSpeed *= 1-modifier;
        }
        
        public class SlowDrawLayer : PlayerDrawLayer {
            public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
                return StatusEffectManager.PlayerHasEffectType<Daze>(drawInfo.drawPlayer);
            }
        
            public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ProjectileOverArm,
                PlayerDrawLayers.FrozenOrWebbedDebuff);

            protected override void Draw(ref PlayerDrawSet drawInfo) {
                Player drawPlayer = drawInfo.drawPlayer;

                Texture2D texture = Mod.Assets.Request<Texture2D>("Textures/StunnedSprite").Value;
                Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width/2) - 10,
                    drawPlayer.Top.Y - Main.screenPosition.Y - 44);
                DrawData data = new DrawData(texture, texturePos, Color.White);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}