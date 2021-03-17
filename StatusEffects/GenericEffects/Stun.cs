using System.Collections.Generic;
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
        public Stun(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }
        
        public void SetControls() {
            User.controlRight = false;
            User.controlLeft = false;
            User.controlJump = false;
            User.controlUp = false;
            User.controlDown = false;
        }

        public bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            return false;
        }

        public override void GetListOfPlayerLayers(List<PlayerLayer> playerLayers) {
            var playerLayer = new PlayerLayer("TerrariaMoba", DisplayName, PlayerLayer.MiscEffectsFront, delegate(PlayerDrawInfo drawInfo) {
                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = ModLoader.GetMod("TerrariaMoba");
                MobaPlayer mobaPlayer = drawPlayer.GetModPlayer<MobaPlayer>();
                
                Texture2D texture = mod.GetTexture("Textures/StunnedSprite");
                Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width/2) - 10,
                    drawPlayer.Top.Y - Main.screenPosition.Y - 44);
                DrawData data = new DrawData(texture, texturePos, Color.White);
                Main.playerDrawData.Add(data);
            });

            playerLayers.Add(playerLayer);
        }
    }
}