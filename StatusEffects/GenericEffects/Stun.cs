using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.GenericEffects {
    public abstract class Stun : StatusEffect, ISetControls, IShoot {
        public Stun() { }
        public Stun(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
        
        public void SetControls() {
            User.controlRight = false;
            User.controlLeft = false;
            User.controlJump = false;
            User.controlUp = false;
            User.controlDown = false;
        }

        public override void WhileActive() {
            User.GetModPlayer<MobaPlayer>().DisableAbilities = true;
            base.WhileActive();
        }

        public bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, 
            int damage, float knockback) {
            return false;
        }

        public class StunDrawLayer : PlayerDrawLayer {
            public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
                return StatusEffectManager.PlayerHasEffectType<Stun>(drawInfo.drawPlayer);
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