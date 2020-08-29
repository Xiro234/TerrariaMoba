using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class ResourceBar : UIElement {
        private Color gradientA;
        private Color gradientB;
        private int previousLife;
        private int resource; //0 for life, 1 for mana

        public ResourceBar(Color gradA, Color gradB, int res) {
            gradientA = gradA;
            gradientB = gradB;
            resource = res;
        }
        
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            float quotient;
            var player = Main.LocalPlayer;
            if (player != null) {
                if (resource == 0) {
                    quotient = (float) player.statLife / player.statLifeMax2;
                }
                else if (resource == 1) {
                    quotient = (float) player.statMana / player.statManaMax;
                }
                else {
                    quotient = (float) player.GetModPlayer<MobaPlayer>().MyCharacter.experience /
                               player.GetModPlayer<MobaPlayer>().MyCharacter.xpPerLevel;
                }

                quotient = Utils.Clamp(quotient, 0f, 1f);
                Rectangle hitbox = GetDimensions().ToRectangle();

                int left = hitbox.Left;
                int right = hitbox.Right;
                int top = hitbox.Top;
                int bottom = hitbox.Bottom;
                if (resource != 2) {
                    int steps = (int) ((right - left) * quotient);
                    int i = 0;
                    for (i = 0; i < steps; i++) {
                        float percent = (float) i / (right - left);
                        spriteBatch.Draw(Main.magicPixel, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height),
                            Color.Lerp(gradientA, gradientB, percent));
                    }

                    if (resource == 0) {
                        if (previousLife > player.statLife) {
                            int prevLifeSteps =
                                (int) ((right - left) *
                                       ((float) (previousLife - player.statLife) / player.statLifeMax2) +
                                       i);
                            for (int j = i; j < prevLifeSteps; j++) {
                                spriteBatch.Draw(Main.magicPixel, new Rectangle(left + j, hitbox.Y, 1, hitbox.Height),
                                    Color.Red);
                            }

                            previousLife -= 2;
                        }
                        else if (previousLife <= player.statLife) {
                            previousLife = player.statLife;
                        }
                    }
                }
                else {
                    int steps = (int) ((bottom - top) * quotient);
                    for (int i = 0; i < steps; i++) {
                        float percent = (float) i / (bottom - top);
                        spriteBatch.Draw(Main.magicPixel, new Rectangle(hitbox.X, bottom - i, hitbox.Width, 1),
                            Color.Lerp(gradientA, gradientB, percent));
                    }
                }
            }
        }
    }
}