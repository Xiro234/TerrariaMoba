using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.UI;
using TerrariaMoba.Enums;

namespace TerrariaMoba.UI {
    public class MobaBar : UIState {
        private UIText ability1Cooldown;
        private UIText ability2Cooldown;
        private UIText ultimateCooldown;
        private UIText traitCooldown;
        private UIPanel bar;
        private UIAbilityIcon ability1Panel;
        private UIAbilityIcon ability2Panel;
        private UIAbilityIcon ultimatePanel;
        private UIAbilityIcon traitPanel;
        
        public override void OnInitialize() {
            bar = new UIPanel();
            bar.Width.Set(500, 0);
            bar.Height.Set(75, 0);
            bar.VAlign = 0.95f;
            bar.HAlign = 0.5f;
            Append(bar);

            ability1Panel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            ability1Panel.VAlign = 0.5f;
            ability1Panel.HAlign = 0.03f;
            
            ability1Cooldown = new UIText("");
            ability1Cooldown.VAlign = 0.5f;
            ability1Cooldown.HAlign = 0.5f;
            ability1Panel.Append(ability1Cooldown);
            
            ability2Panel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            ability2Panel.VAlign = 0.5f;
            ability2Panel.HAlign = 0.18f;
            
            ability2Cooldown = new UIText("");
            ability2Cooldown.VAlign = 0.5f;
            ability2Cooldown.HAlign = 0.5f;
            ability2Panel.Append(ability2Cooldown);
            
            ultimatePanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            ultimatePanel.VAlign = 0.5f;
            ultimatePanel.HAlign = 0.33f;
            
            ultimateCooldown = new UIText("");
            ultimateCooldown.VAlign = 0.5f;
            ultimateCooldown.HAlign = 0.5f;
            ultimatePanel.Append(ultimateCooldown);
            
            traitPanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            traitPanel.VAlign = 0.5f;
            traitPanel.HAlign = 0.48f;
            
            traitCooldown = new UIText("");
            traitCooldown.VAlign = 0.5f;
            traitCooldown.HAlign = 0.5f;
            traitPanel.Append(traitCooldown);
            
            bar.Append(ability1Panel);
            bar.Append(ability2Panel);
            bar.Append(ultimatePanel);
            bar.Append(traitPanel);
        }

        public void SetIcons() {
            var player = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            
            ability1Panel.SetIcon(player.MyCharacter.AbilityOneIcon, player.MyCharacter.AbilityOneName);
            ability2Panel.SetIcon(player.MyCharacter.AbilityTwoIcon, player.MyCharacter.AbilityTwoName);
        }

        public override void Update(GameTime gameTime) {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            if (mobaPlayer.CharacterPicked) {
                if(mobaPlayer.MyCharacter.AbilityOneCooldownTimer > 0) {
                    ability1Cooldown.SetText((mobaPlayer.MyCharacter.AbilityOneCooldownTimer / 60).ToString());
                    ability1Panel.isOnCooldown = true;
                    ability1Panel.cooldown = mobaPlayer.MyCharacter.AbilityOneCooldown;
                    ability1Panel.cooldownTimer = mobaPlayer.MyCharacter.AbilityOneCooldownTimer;
                }
                else {
                    ability1Cooldown.SetText("");
                    ability1Panel.isOnCooldown = false;
                }
                
                if(mobaPlayer.MyCharacter.AbilityTwoCooldownTimer > 0) {
                    ability2Cooldown.SetText((mobaPlayer.MyCharacter.AbilityTwoCooldownTimer / 60).ToString());
                    ability2Panel.isOnCooldown = true;
                    ability2Panel.cooldown = mobaPlayer.MyCharacter.AbilityTwoCooldown;
                    ability2Panel.cooldownTimer = mobaPlayer.MyCharacter.AbilityTwoCooldownTimer;
                }
                else {
                    ability2Cooldown.SetText("");
                    ability2Panel.isOnCooldown = false;
                }
            }
        }

        public void UnLoad() {
            ability1Cooldown = null;
            ability2Cooldown = null;
            ability1Panel = null;
            ability2Panel = null;
            bar = null;
        }
    }
}