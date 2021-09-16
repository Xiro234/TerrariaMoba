using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI;
using TerrariaMoba.Characters;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.UI {
    public class ResourceBar : UIElement {
        private Resource resource { get; set; }
        private Texture2D bar;

        public ResourceBar(Resource res) {
            resource = res;
            bar = UIUtils.GetBarFromResource(resource);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            
            float quotient;
            var player = Main.LocalPlayer;
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            if (player != null) {
                string text = "";

                if (resource == Resource.Life) {
                    quotient = (float) player.statLife / player.statLifeMax2;
                    text = player.statLife + "/" + player.statLifeMax2;
                }
                else if (resource == Resource.Experience) {
                    quotient = (float) mobaPlayer.Hero.Experience / Character.XP_PER_LEVEL;
                    text = mobaPlayer.Hero.Experience + "/" + Character.XP_PER_LEVEL;
                }
                else {
                    float maxResource = mobaPlayer.FlatStats.MaxResource + mobaPlayer.Hero.BaseStatistics.MaxResource;
                    float currentResource = mobaPlayer.CurrentResource;
                    quotient = currentResource / maxResource;
                    text = currentResource + "/" + maxResource;
                }

                quotient = Utils.Clamp(quotient, 0f, 1f);
                Rectangle rectangle = GetDimensions().ToRectangle();
                
                float scale = 0.6f;
                spriteBatch.Draw(bar, rectangle.Center(), new Rectangle(0, 0, (int)(bar.Width * quotient), bar.Height), Color.White);
                Vector2 stringPos = rectangle.Center() + new Vector2(bar.Width / 2f, bar.Height / 2f);
                stringPos -= (new Vector2(Main.fontMouseText.MeasureString(text).X / 2f,Main.fontMouseText.MeasureString(text).Y / 3f)) * scale;
                spriteBatch.DrawString(Main.fontMouseText, text, stringPos, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
    }
}