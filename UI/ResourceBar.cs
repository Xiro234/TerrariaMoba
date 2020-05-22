using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;

namespace TerrariaMoba.UI {
    public class ResourceBar : UIElement {
        private Color gradientA;
        private Color gradientB;
        //private int previousLife; Do a sastifying damage indicator at some point
        private int resource; //0 for life, 1 for mana

        public ResourceBar(Color gradA, Color gradB, int manaOrLife) {
            gradientA = gradA;
            gradientB = gradB;
            resource = manaOrLife;
        }
        
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            
            float quotient;
            var player = Main.LocalPlayer;
            if (resource == 0) {
                quotient = (float) player.statLife / player.statLifeMax;
            }
            else {
                quotient = (float) player.statMana / player.statManaMax;
            }

            quotient = Utils.Clamp(quotient, 0f, 1f);
            Rectangle hitbox = GetDimensions().ToRectangle();

            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int) ((right - left) * quotient);

            for (int i = 0; i < steps; i++) {
                float percent = (float) i / (right - left);
                spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }
    }
}