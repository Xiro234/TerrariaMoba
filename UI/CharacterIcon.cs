using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class CharacterIcon : UIImage {
        public string hoverText;
        public CharacterEnum character;

        public CharacterIcon(Texture2D texture, CharacterEnum newCharacter) : base(texture) {
            character = newCharacter;

            switch (character) {
                case(CharacterEnum.Sylvia):
                    hoverText = "Sylvia Verda, Markswoman of the Jungle";
                    break;
                case(CharacterEnum.Marie):
                    hoverText = "Marie Tidewrath, High Priestess of Lacusia";
                    break;
                case(CharacterEnum.Flibnob):
                    hoverText = "Flibnob, the Ogre Lord";
                    break;
                case(CharacterEnum.Osteo):
                    hoverText = "Osteo Prime, Last Necromancer of the Mudpits";
                    break;
                case(CharacterEnum.Nocturne):
                    hoverText = "Nocturne Umbra, The Glory of Ecliptera";
                    break;
                case(CharacterEnum.Chastradamus):
                    hoverText = "Chastradamus, the Plague Doctor";
                    break;
                case(CharacterEnum.OldMan):
                    hoverText = "Old Man, The Venerable Trawler";
                    break;
                case(CharacterEnum.Jorm):
                    hoverText = "Jorm Goldenhammer, Hero of the Ogre War";
                    break;
                default:
                    hoverText = "";
                    break;
            }
        }
        
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();

            if (IsMouseHovering) {
                Main.hoverItemName = hoverText;
            }

            if (modPlayer.CharacterSelected == character && character != CharacterEnum.Null) {
                Rectangle hitbox = GetDimensions().ToRectangle();

                int left = hitbox.Left;
                int right = hitbox.Right;
                int top = hitbox.Top;
                int bottom = hitbox.Bottom;
                spriteBatch.Draw(Main.magicPixel, new Rectangle(left - 2, top - 2, right - left + 4, 2), Color.Yellow); //Top rect
                spriteBatch.Draw(Main.magicPixel, new Rectangle(right, top - 2, 2, bottom - top + 4), Color.Yellow); //Right rect
                spriteBatch.Draw(Main.magicPixel, new Rectangle(left - 2, bottom, right - left + 4, 2), Color.Yellow); //Bottom Rect
                spriteBatch.Draw(Main.magicPixel, new Rectangle(left - 2, top - 2, 2, bottom - top + 4), Color.Yellow); //Left Rect
            }
        }

        public override void Click(UIMouseEvent evt) {
            var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            modPlayer.CharacterSelected = character;
            Main.PlaySound(10);
        }

        public override void MouseOver(UIMouseEvent evt) {
            base.MouseOver(evt);
            Main.PlaySound(12);
        }
    }
}