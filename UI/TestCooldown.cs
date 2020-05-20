using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class TestCooldown : UIState {
        private UIText text;
        public override void OnInitialize() {
            UIPanel panel = new UIPanel();
            
            panel.Width.Set(300, 0);
            panel.Height.Set(300, 0);
            Append(panel);

            text = new UIText("");
            panel.Append(text);
        }

        public override void Update(GameTime gameTime) {
            if (Main.LocalPlayer.GetModPlayer<MobaPlayer>().CharacterPicked) {
                try {
                    text.SetText((Main.LocalPlayer.GetModPlayer<MobaPlayer>().MyCharacter.AbilityOneCooldownTimer / 60).ToString());
                }
                catch (NullReferenceException) {
                    Main.NewText("NULL!");
                }
            }
        }
    }
}