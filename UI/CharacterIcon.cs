using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariaMoba.Characters;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class CharacterIcon : UIImage {
        public Character character;

        public CharacterIcon(Character newCharacter) : base(newCharacter.CharacterIcon) {
            character = newCharacter;
        }
        
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();

            if (IsMouseHovering) {
                Main.hoverItemName = character.FullName;
            }

            if (modPlayer.selectedCharacter == character.identity && character.identity != CharacterIdentity.Base) {
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
            modPlayer.selectedCharacter = character.identity;
            Main.PlaySound(10);
        }

        public override void MouseOver(UIMouseEvent evt) {
            base.MouseOver(evt);
            Main.PlaySound(12);
        }
    }
}