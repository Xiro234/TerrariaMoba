using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
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
        private UIImage bar;
        private UIAbilityIcon ability1Panel;
        private UIAbilityIcon ability2Panel;
        private UIAbilityIcon ultimatePanel;
        private UIAbilityIcon traitPanel;
        private ResourceBar lifeBar;
        private ResourceBar manaBar;
        private UIText lifeText;
        private UIText manaText;
        private static readonly MethodInfo DrawBuffIcon = typeof(Main).GetMethod("DrawBuffIcon", BindingFlags.NonPublic | BindingFlags.Static);
        
        public override void OnInitialize() {
            bar = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/MobaBarBackground"));
            bar.VAlign = 0.95f;
            bar.HAlign = 0.5f;
            Append(bar);

            ability1Panel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            ability1Panel.Top.Set(72, 0);
            ability1Panel.Left.Set(268, 0);
            
            ability1Cooldown = new UIText("");
            ability1Cooldown.VAlign = 0.5f;
            ability1Cooldown.HAlign = 0.5f;
            ability1Panel.Append(ability1Cooldown);

            ability2Panel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            ability2Panel.Top.Set(72, 0);
            ability2Panel.Left.Set(338, 0);
            
            ability2Cooldown = new UIText("");
            ability2Cooldown.VAlign = 0.5f;
            ability2Cooldown.HAlign = 0.5f;
            ability2Panel.Append(ability2Cooldown);
            
            ultimatePanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            ultimatePanel.Top.Set(72, 0);
            ultimatePanel.Left.Set(408, 0);
            
            ultimateCooldown = new UIText("");
            ultimateCooldown.VAlign = 0.5f;
            ultimateCooldown.HAlign = 0.5f;
            ultimatePanel.Append(ultimateCooldown);
            
            traitPanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Blank"), "");
            traitPanel.Top.Set(72, 0);
            traitPanel.Left.Set(478, 0);
            
            traitCooldown = new UIText("");
            traitCooldown.VAlign = 0.5f;
            traitCooldown.HAlign = 0.5f;
            traitPanel.Append(traitCooldown);
            
            lifeBar = new ResourceBar(Color.DarkGreen, Color.Green, 0);
            lifeBar.Left.Set(54, 0);
            lifeBar.Top.Set(5, 0);
            lifeBar.Width.Set(432, 0);
            lifeBar.Height.Set(14, 0);

            lifeText = new UIText("");
            lifeText.VAlign = 0.5f;
            lifeText.HAlign = 0.5f;
            lifeBar.Append(lifeText);
            
            manaBar = new ResourceBar(Color.DarkBlue, Color.DarkCyan, 1);
            manaBar.Left.Set(54, 0);
            manaBar.Top.Set(22, 0);
            manaBar.Width.Set(432, 0);
            manaBar.Height.Set(14, 0);

            manaText = new UIText("");
            manaText.VAlign = 0.5f;
            manaText.HAlign = 0.5f;
            manaBar.Append(manaText);

            bar.Append(lifeBar);
            bar.Append(manaBar);
            bar.Append(ability1Panel);
            bar.Append(ability2Panel);
            bar.Append(ultimatePanel);
            bar.Append(traitPanel);
        }

        public void SetIcons() {
            var player = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            
            ability1Panel.SetIcon(player.MyCharacter.AbilityOneIcon, player.MyCharacter.AbilityOneName);
            ability2Panel.SetIcon(player.MyCharacter.AbilityTwoIcon, player.MyCharacter.AbilityTwoName);
            ultimatePanel.SetIcon(player.MyCharacter.UltimateIcon, player.MyCharacter.UltimateName);
            traitPanel.SetIcon(player.MyCharacter.TraitIcon, player.MyCharacter.TraitName);
        }

        public override void Update(GameTime gameTime) {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            if (mobaPlayer.CharacterPicked) {
                DrawIcon(ref ability1Cooldown, ref ability1Panel, mobaPlayer.MyCharacter.AbilityOneCooldown, mobaPlayer.MyCharacter.AbilityOneCooldownTimer);
                DrawIcon(ref ability2Cooldown, ref ability2Panel, mobaPlayer.MyCharacter.AbilityTwoCooldown, mobaPlayer.MyCharacter.AbilityTwoCooldownTimer);
                DrawIcon(ref ultimateCooldown, ref ultimatePanel, mobaPlayer.MyCharacter.UltimateCooldown, mobaPlayer.MyCharacter.UltimateCooldownTimer);
                DrawIcon(ref traitCooldown, ref traitPanel, mobaPlayer.MyCharacter.TraitCooldown, mobaPlayer.MyCharacter.TraitCooldownTimer);
            }

            lifeText.SetText(Main.LocalPlayer.statLife + "/" + Main.LocalPlayer.statLifeMax);
            manaText.SetText(Main.LocalPlayer.statMana + "/" + Main.LocalPlayer.statManaMax);
        }

        public void DrawIcon(ref UIText text,ref UIAbilityIcon icon, int cooldown, int timer) {
            if(timer > 0) {
                if (timer >= 40) {
                    text.SetText(Math.Ceiling(timer / 60f)
                        .ToString());
                }
                else {
                    text.SetText((Math.Ceiling((timer / 60f) * 10) / 10f)
                        .ToString());
                }
                
                icon.isOnCooldown = true;
                icon.cooldown = cooldown;
                icon.cooldownTimer = timer;
            }
            else {
                text.SetText("");
                icon.isOnCooldown = false;
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