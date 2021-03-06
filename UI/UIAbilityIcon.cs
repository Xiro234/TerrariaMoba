using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System;
using Microsoft.Xna.Framework.Input;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class UIAbilityIcon : UIImage {
        public Ability ability;
        public int abilityMaxCooldown = 0;

        public UIAbilityIcon(Texture2D texture) : base(texture) {
            //ability = new Ability();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
            SetImage(ability.Icon);
            
            if (IsMouseHovering) {
                Main.hoverItemName = ability.Name;
            }

            //Cooldown Effect
            if (ability.CooldownTimer > 0) {
                Rectangle hitbox = GetDimensions().ToRectangle();
                if (abilityMaxCooldown == 0) {
                    abilityMaxCooldown = ability.CooldownTimer;
                }

                //pixel adjustments so it covers the entirety of the icon
                int left = hitbox.Left - 1;
                int right = hitbox.Right - 1;
                int top = hitbox.Top + 1;
                int bottom = hitbox.Bottom + 1;
                
                float rads = (float)(Math.PI * 2) - ((float)(Math.PI * 2) * ((float) ability.CooldownTimer / (float) abilityMaxCooldown));
                
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
            else {
                abilityMaxCooldown = 0;
            }

            ability.AdditionalDrawing(spriteBatch, this);
        }

        public override void Click(UIMouseEvent evt) {
            if (Main.keyState.IsKeyDown(Keys.LeftAlt)) {
                var modPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
                if (ability.CooldownTimer > 0) {
                    Main.NewText(Main.LocalPlayer.name + " is ready to use " + ability.Name + " in " + Math.Ceiling(ability.CooldownTimer / 60f)  + " seconds!");
                }
                else {
                    Main.NewText(Main.LocalPlayer.name + " is ready to use " + ability.Name + "!");
                }
            }
        }
    }
}