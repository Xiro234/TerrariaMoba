using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using System;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class UIAbilityIcon : UIImage {
        private Ability MyAbility{ get; set; }

        public UIAbilityIcon(Texture2D texture) : base(texture) { }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            SetImage(MyAbility.Icon);
            
            if (IsMouseHovering) {
                Main.hoverItemName = MyAbility.Name;
            }

            //Cooldown Effect
            if (MyAbility.CooldownTimer > 0) {
                Rectangle hitbox = GetDimensions().ToRectangle();

                //pixel adjustments so it covers the entirety of the icon
                int left = hitbox.Left - 1;
                int right = hitbox.Right - 1;
                int top = hitbox.Top;
                int bottom = hitbox.Bottom;
                
                float rads = (float)(Math.PI * 2) - ((float)(Math.PI * 2) * ((float) MyAbility.CooldownTimer / (float) MyAbility.BaseCooldown));
                
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

        public void SetAbility(Ability ability) {
            MyAbility = ability;
        }

        /*public override void Click(UIMouseEvent evt) {
            if (Main.keyState.IsKeyDown(Keys.LeftAlt)) {
                var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
                if (ability.CooldownTimer > 0) {
                    Main.NewText(Main.LocalPlayer.name + " is ready to use " + ability.Name + " in " + Math.Ceiling(ability.CooldownTimer / 60f)  + " seconds!");
                }
                else {
                    Main.NewText(Main.LocalPlayer.name + " is ready to use " + ability.Name + "!");
                }
            }
        }*/
    }
}