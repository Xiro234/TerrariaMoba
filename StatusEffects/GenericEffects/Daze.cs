using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.GenericEffects {
    public abstract class Daze : StatusEffect, IResetEffects {
        public Daze() { }

        public Daze(float magnitude, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
            modifier = magnitude;
        }

        private float modifier;
        
        public void ResetEffects() {
            //User.GetModPlayer<MobaPlayer>().Stats.AttackSpeed *= 1-(modifier/100f);
            //User.GetModPlayer<MobaPlayer>().Stats.MoveSpeed *= 1-(modifier/100f);
            //User.GetModPlayer<MobaPlayer>().Stats.JumpSpeed *= 1-(modifier/100f);
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