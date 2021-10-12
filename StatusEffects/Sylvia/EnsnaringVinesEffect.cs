using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class EnsnaringVinesEffect : Root {
        public override string DisplayName { get => "Ensnaring Vines"; }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public EnsnaringVinesEffect() { }

        public EnsnaringVinesEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }

        public override void GetListOfPlayerDrawLayers(List<PlayerDrawLayer> playerLayers) {
            var playerLayer = new PlayerDrawLayer("TerrariaMoba", DisplayName, PlayerDrawLayer.MiscEffectsFront, delegate(PlayerDrawSet drawInfo) {
                Texture2D texture = ModContent.Request<Texture2D>("Textures/Sylvia/EnsnaringVines").Value;
                Player drawPlayer = drawInfo.drawPlayer;

                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + (drawPlayer.height - 2f) - Main.screenPosition.Y);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            });
            
            playerLayers.Add(playerLayer);
            base.GetListOfPlayerDrawLayers(playerLayers);
        }
    }
}