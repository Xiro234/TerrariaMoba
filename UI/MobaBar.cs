﻿using System;
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
        private UIText lifeText;
        private UIText manaText;
        private UIText defenseText;
        private UIText moveSpeedText;
        private UIText deathTimer;
        private UIText levelText;
        private UIImage bar;
        private UIImage characterIcon;
        private UIImage moveSpeedIcon;
        private UIImage defenseIcon;
        private UIAbilityIcon ability1Panel;
        private UIAbilityIcon ability2Panel;
        private UIAbilityIcon ultimatePanel;
        private UIAbilityIcon traitPanel;
        private ResourceBar lifeBar;
        private ResourceBar manaBar;
        private ResourceBar experienceBar;

        public override void OnInitialize() {
            bar = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/MobaBarBackground"));
            bar.VAlign = 0.95f;
            bar.HAlign = 0.5f;
            Append(bar);

            ability1Panel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"), "");
            ability1Panel.Top.Set(50, 0);
            ability1Panel.Left.Set(268, 0);
            
            ability1Cooldown = new UIText("");
            ability1Cooldown.VAlign = 0.5f;
            ability1Cooldown.HAlign = 0.5f;
            ability1Panel.Append(ability1Cooldown);

            ability2Panel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"), "");
            ability2Panel.Top.Set(50, 0);
            ability2Panel.Left.Set(338, 0);
            
            ability2Cooldown = new UIText("");
            ability2Cooldown.VAlign = 0.5f;
            ability2Cooldown.HAlign = 0.5f;
            ability2Panel.Append(ability2Cooldown);
            
            ultimatePanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"), "");
            ultimatePanel.Top.Set(50, 0);
            ultimatePanel.Left.Set(408, 0);
            
            ultimateCooldown = new UIText("");
            ultimateCooldown.VAlign = 0.5f;
            ultimateCooldown.HAlign = 0.5f;
            ultimatePanel.Append(ultimateCooldown);
            
            traitPanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"), "");
            traitPanel.Top.Set(50, 0);
            traitPanel.Left.Set(478, 0);
            
            traitCooldown = new UIText("");
            traitCooldown.VAlign = 0.5f;
            traitCooldown.HAlign = 0.5f;
            traitPanel.Append(traitCooldown);
            
            lifeBar = new ResourceBar(Color.DarkGreen, Color.Lime, 0);
            lifeBar.Left.Set(54, 0);
            lifeBar.Top.Set(5, 0);
            lifeBar.Width.Set(432, 0);
            lifeBar.Height.Set(14, 0);

            lifeText = new UIText("");
            lifeText.VAlign = 0.5f;
            lifeText.HAlign = 0.5f;
            lifeText.TextColor = new Color(255f, 255f, 255f, 0f);
            lifeBar.Append(lifeText);
            
            manaBar = new ResourceBar(Color.DarkBlue, Color.DarkCyan, 1);
            manaBar.Left.Set(54, 0);
            manaBar.Top.Set(22, 0);
            manaBar.Width.Set(432, 0);
            manaBar.Height.Set(14, 0);

            manaText = new UIText("");
            manaText.VAlign = 0.5f;
            manaText.HAlign = 0.5f;
            manaText.TextColor = new Color(255, 255, 255, 0);
            manaBar.Append(manaText);
            
            experienceBar = new ResourceBar(Color.DarkGray, Color.LightGray, 2);
            experienceBar.Left.Set(164, 0);
            experienceBar.Top.Set(50, 0);
            experienceBar.Width.Set(6, 0);
            experienceBar.Height.Set(52, 0);

            levelText = new UIText("");
            levelText.Left.Set(224, 0);
            levelText.Top.Set(92, 0);
            levelText.Width.Set(6, 0);
            levelText.Height.Set(6, 0);

            characterIcon = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/Lock"));
            characterIcon.Left.Set(180, 0);
            characterIcon.Top.Set(50, 0);

            deathTimer = new UIText("");
            deathTimer.VAlign = 0.5f;
            deathTimer.HAlign = 0.5f;
            characterIcon.Append(deathTimer);
            
            moveSpeedIcon = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/moveSpeedImage"));
            moveSpeedIcon.Left.Set(16, 0);
            moveSpeedIcon.Top.Set(54, 0);
            
            moveSpeedText = new UIText("");
            moveSpeedText.Left.Set(32, 0);
            moveSpeedText.Top.Set(58, 0);

            defenseIcon = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/defenseImage"));
            defenseIcon.Left.Set(16, 0);
            defenseIcon.Top.Set(72, 0);
            
            defenseText = new UIText("");
            defenseText.Left.Set(32, 0);
            defenseText.Top.Set(72, 0);
            
            bar.Append(lifeBar);
            bar.Append(manaBar);
            bar.Append(experienceBar);
            bar.Append(defenseText);
            bar.Append(moveSpeedText);
            bar.Append(ability1Panel);
            bar.Append(ability2Panel);
            bar.Append(ultimatePanel);
            bar.Append(traitPanel);
            bar.Append(characterIcon);
            bar.Append(levelText);
            bar.Append(moveSpeedIcon);
            bar.Append(defenseIcon);
        }

        public void SetIcons() {
            var player = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            try {
                ability1Panel.SetIcon(player.MyCharacter.AbilityOneIcon, player.MyCharacter.AbilityOneName);
                ability2Panel.SetIcon(player.MyCharacter.AbilityTwoIcon, player.MyCharacter.AbilityTwoName);
                ultimatePanel.SetIcon(player.MyCharacter.UltimateIcon, player.MyCharacter.UltimateName);
                traitPanel.SetIcon(player.MyCharacter.TraitIcon, player.MyCharacter.TraitName);
                characterIcon.SetImage(player.MyCharacter.CharacterIcon);
            }
            catch (NullReferenceException exception) {
                Main.NewText(exception.Message);
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen)) {
                Main.LocalPlayer.mouseInterface = true;
            }
            
            var player = Main.LocalPlayer;
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            if (mobaPlayer.CharacterPicked) {
                DrawIcon(ref ability1Cooldown, ref ability1Panel, mobaPlayer.MyCharacter.AbilityOneCooldown, mobaPlayer.MyCharacter.AbilityOneCooldownTimer);
                DrawIcon(ref ability2Cooldown, ref ability2Panel, mobaPlayer.MyCharacter.AbilityTwoCooldown, mobaPlayer.MyCharacter.AbilityTwoCooldownTimer);
                DrawIcon(ref ultimateCooldown, ref ultimatePanel, mobaPlayer.MyCharacter.UltimateCooldown, mobaPlayer.MyCharacter.UltimateCooldownTimer);
                DrawIcon(ref traitCooldown, ref traitPanel, mobaPlayer.MyCharacter.TraitCooldown, mobaPlayer.MyCharacter.TraitCooldownTimer);
                levelText.SetText(mobaPlayer.MyCharacter.level.ToString(), 0.75f, false);
            }
            
            if (player.lifeRegen >= 0) {
                lifeText.SetText(player.statLife + "/" + player.statLifeMax2 + " (+" + player.lifeRegen + ")", 0.75f, false);
            }
            else {
                lifeText.SetText(player.statLife + "/" + player.statLifeMax2 + " (" + player.lifeRegen + ")", 0.75f, false);
            }

            if (player.manaRegen >= 0) {
                manaText.SetText(player.statMana + "/" + player.statManaMax + " (+" + player.manaRegen + ")", 0.75f, false);
            }
            else {
                manaText.SetText(player.statMana + "/" + player.statManaMax + " (" + player.manaRegen + ")", 0.75f, false);
            }

            defenseText.SetText(player.statDefense.ToString(), 0.6f, false);
            moveSpeedText.SetText(Math.Round(player.velocity.Length()).ToString(), 0.6f, false);

            if (player.dead) {
                deathTimer.SetText(Math.Ceiling(player.respawnTimer / 60f).ToString());
            }
            else {
                deathTimer.SetText("");
            }
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
            ultimateCooldown = null;
            traitCooldown = null;
            lifeText = null;
            manaText = null;
            defenseText = null;
            moveSpeedText = null;
            deathTimer = null;
            levelText = null;
            bar = null;
            characterIcon = null;
            moveSpeedIcon = null;
            defenseIcon = null;
            ability1Panel = null;
            ability2Panel = null;
            ultimatePanel = null;
            traitPanel = null;
            lifeBar = null;
            manaBar = null;
            experienceBar = null;
        }
    }
}