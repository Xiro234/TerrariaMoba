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
        public EnsnaringVinesEffect(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
    }
    
    public class EnsnaringVinesLayer : PlayerDrawLayer {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
            return StatusEffectManager.PlayerHasEffectType<EnsnaringVinesEffect>(drawInfo.drawPlayer);
        }
        
        public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ProjectileOverArm,
            PlayerDrawLayers.FrozenOrWebbedDebuff);

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            Texture2D texture = ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/EnsnaringVines").Value;
            Player drawPlayer = drawInfo.drawPlayer;
                
            int drawX = (int)(drawInfo.Position.X + drawPlayer.width / 2f - Main.screenPosition.X);
            int drawY = (int)(drawInfo.Position.Y + (drawPlayer.height - 2f) - Main.screenPosition.Y);
            DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), 
                Lighting.GetColor((int)((drawInfo.Position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.Position.Y + drawPlayer.height) / 16f)), 0f, 
                new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
            drawInfo.DrawDataCache.Add(data);
        }
    }
    

}