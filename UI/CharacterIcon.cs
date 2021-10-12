using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariaMoba.Characters;
using TerrariaMoba.Players;
using Terraria.Audio;

namespace TerrariaMoba.UI {
    public class CharacterIcon : UIImage {
        private Character Hero { get; set; }

        public CharacterIcon(Character newHero) : base(newHero.CharacterIcon) {
            Hero = newHero;
        }
        
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            
            if (Hero != null) {
                if (IsMouseHovering) {
                    Main.hoverItemName = Hero.Name;
                }

                if (modPlayer.selectedCharacter == Hero.GetType()) {
                    Rectangle hitbox = GetDimensions().ToRectangle();

                    int left = hitbox.Left;
                    int right = hitbox.Right;
                    int top = hitbox.Top;
                    int bottom = hitbox.Bottom;
                    spriteBatch.Draw(TextureAssets.MagicPixel, new Rectangle(left - 2, top - 2, right - left + 4, 2),
                        Color.Yellow); //Top rect
                    spriteBatch.Draw(TextureAssets.MagicPixel, new Rectangle(right, top - 2, 2, bottom - top + 4),
                        Color.Yellow); //Right rect
                    spriteBatch.Draw(TextureAssets.MagicPixel, new Rectangle(left - 2, bottom, right - left + 4, 2),
                        Color.Yellow); //Bottom Rect
                    spriteBatch.Draw(TextureAssets.MagicPixel, new Rectangle(left - 2, top - 2, 2, bottom - top + 4),
                        Color.Yellow); //Left Rect
                }
            }
        }

        public override void Click(UIMouseEvent evt) {
            var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            if (Hero != null) {
                modPlayer.selectedCharacter = Hero.GetType();
            }

            SoundEngine.PlaySound(10);
        }

        public override void MouseOver(UIMouseEvent evt) {
            base.MouseOver(evt);
            SoundEngine.PlaySound(12);
        }
    }
}