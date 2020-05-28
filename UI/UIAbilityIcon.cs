using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Microsoft.Xna.Framework.Input;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class UIAbilityIcon : UIImage {
        public string hoverText;
        public bool isOnCooldown = false;
        public int cooldown;
        public int cooldownTimer;

        public UIAbilityIcon(Texture2D texture, string abilityName) : base(texture) {
            hoverText = abilityName;
        }

        public void SetIcon(Texture2D texture, string abilityName) {
            SetImage(texture);
            hoverText = abilityName;
        }
        
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            if (IsMouseHovering) {
                Main.hoverItemName = hoverText;
            }

            //Cooldown Effect
            if (isOnCooldown) {
                Rectangle hitbox = GetDimensions().ToRectangle();

                //pixel adjustments so it covers the entirety of the icon
                int left = hitbox.Left - 1;
                int right = hitbox.Right - 1;
                int top = hitbox.Top + 1;
                int bottom = hitbox.Bottom + 1;
                
                float rads = (float)(Math.PI * 2) - ((float)(Math.PI * 2) * ((float) cooldownTimer / (float) cooldown));
                
                for (int i = 0; i < right - left; i++) {
                    for (int j = 0; j < bottom - top; j++) {
                        //https://stackoverflow.com/questions/3441782/how-to-calculate-the-angle-of-a-vector-from-the-vertical
                        float theta = ((float) Math.Atan2((left + i) - hitbox.Center.X, (top + j) - hitbox.Center.Y) + (float)Math.PI);
                        Color color = Color.MidnightBlue;
                        
                        if (theta > rads) {
                            color.A = 255;
                        }
                        else {
                            color.A = 180;
                        }
                        
                        spriteBatch.Draw(Main.magicPixel, new Rectangle(right - i, top + j, 1, 1), color); //Top + j makes it go clockwise
                    }
                }
            }
        }

        public override void Click(UIMouseEvent evt) {
            if (Main.keyState.IsKeyDown(Keys.LeftAlt)) {
                var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
                if (isOnCooldown) {
                    Main.NewText(Main.LocalPlayer.name + " is ready to use " + hoverText + " in " + Math.Ceiling(cooldownTimer / 60f)  + " seconds!");
                }
                else {
                    Main.NewText(Main.LocalPlayer.name + " is ready to use " + hoverText + "!");
                }
            }
        }
    }
}