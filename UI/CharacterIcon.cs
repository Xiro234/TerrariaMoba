using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using System;
using TerrariaMoba.Characters;

namespace TerrariaMoba.UI {
    public class CharacterIcon : UIImage {
        public string hoverText;
        public CharacterEnum character;

        public CharacterIcon(Texture2D texture, CharacterEnum newCharacter) : base(texture) {
            character = newCharacter;

            switch (character) {
                case(CharacterEnum.Sylvia):
                    hoverText = "Sylvia Verda";
                    break;
                default:
                    hoverText = "";
                    break;
            }
        }
        
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            if (IsMouseHovering) {
                Main.hoverItemName = hoverText;
            }
        }

        public override void Click(UIMouseEvent evt) {
            var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            switch (character) {
                case(CharacterEnum.Sylvia):
                    modPlayer.MyCharacter = new Sylvia();
                    break;
                default:
                    Main.NewText("Error! No CharacterEnum!");
                    break;
            }
        }
    }
}