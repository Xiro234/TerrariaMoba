using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using Terraria.GameContent;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

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
                int left = hitbox.Left;
                int right = hitbox.Right;
                int top = hitbox.Top;
                int bottom = hitbox.Bottom + 1;
                
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
                        
                        spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(right - i, top + j, 1, 1), color); //Top + j makes it go clockwise
                    }
                }

                var text = "";
                if (MyAbility.CooldownTimer >= 40) {
                    text = Math.Ceiling(MyAbility.CooldownTimer / 60f).ToString();
                }
                else {
                    text = ((Math.Ceiling((MyAbility.CooldownTimer / 60f) * 10) / 10f).ToString());
                }

                spriteBatch.DrawString(FontAssets.MouseText.Value, text, hitbox.Center() - FontAssets.MouseText.Value.MeasureString(text) / 2f, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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