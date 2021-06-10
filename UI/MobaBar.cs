using System;
using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.UI {
    public class MobaBar : UIState {
        private static UIText QCooldown;
        private static UIText ECooldown;
        private static UIText RCooldown;
        private static UIText CCooldown;
        private static UIText deathTimer;
        private static UIImage bar;
        private static UIImage characterIcon;
        private static UIAbilityIcon QPanel;
        private static UIAbilityIcon EPanel;
        private static UIAbilityIcon RPanel;
        private static UIAbilityIcon CPanel;
        private ResourceBar lifeBar;
        private ResourceBar resourceBar;

        public override void OnInitialize() {
            bar = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/MobaBarBackground"));
            bar.VAlign = 0.99f;
            bar.HAlign = 0.05f;
            Append(bar);

            QPanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"));
            QPanel.Top.Set(50, 0);
            QPanel.Left.Set(268, 0);
            
            QCooldown = new UIText("");
            QCooldown.VAlign = 0.5f;
            QCooldown.HAlign = 0.5f;
            QPanel.Append(QCooldown);

            EPanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"));
            EPanel.Top.Set(50, 0);
            EPanel.Left.Set(338, 0);
            
            ECooldown = new UIText("");
            ECooldown.VAlign = 0.5f;
            ECooldown.HAlign = 0.5f;
            EPanel.Append(ECooldown);
            
            RPanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"));
            RPanel.Top.Set(50, 0);
            RPanel.Left.Set(408, 0);
            
            RCooldown = new UIText("");
            RCooldown.VAlign = 0.5f;
            RCooldown.HAlign = 0.5f;
            RPanel.Append(RCooldown);
            
            CPanel = new UIAbilityIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"));
            CPanel.Top.Set(50, 0);
            CPanel.Left.Set(478, 0);
            
            CCooldown = new UIText("");
            CCooldown.VAlign = 0.5f;
            CCooldown.HAlign = 0.5f;
            CPanel.Append(CCooldown);
            
            lifeBar = new ResourceBar(Resource.Life);
            lifeBar.Left.Set(146, 0);
            lifeBar.Top.Set(6, 0);
            
            resourceBar = new ResourceBar(Resource.Mana);
            resourceBar.Left.Set(146, 0);
            resourceBar.Top.Set(22, 0);

            /*lifeText = new UIText("");
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
            levelText.Height.Set(6, 0);*/

            characterIcon = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/Lock"));
            characterIcon.Left.Set(180, 0);
            characterIcon.Top.Set(50, 0);

            deathTimer = new UIText("");
            deathTimer.VAlign = 0.5f;
            deathTimer.HAlign = 0.5f;
            characterIcon.Append(deathTimer);

            bar.Append(lifeBar);
            /*bar.Append(manaBar);
            bar.Append(experienceBar);
            bar.Append(armorText);
            bar.Append(moveSpeedText);
            bar.Append(QPanel);
            bar.Append(EPanel);
            bar.Append(RPanel);
            bar.Append(CPanel);
            bar.Append(characterIcon);
            bar.Append(levelText);*/
        }

        public void SetIcons() {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();

            characterIcon.SetImage(mobaPlayer.Hero.CharacterIcon);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (bar.ContainsPoint(Main.MouseScreen)) {
                Main.LocalPlayer.mouseInterface = true;
            }
            
            var player = Main.LocalPlayer;
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            /*DrawIcon(ref QCooldown, ref QPanel, mobaPlayer.Hero.SlotOne.CooldownTimer, mobaPlayer.Hero.SlotOne);
            DrawIcon(ref ECooldown, ref EPanel, mobaPlayer.Hero.SlotTwo.CooldownTimer, mobaPlayer.Hero.SlotTwo);
            DrawIcon(ref RCooldown, ref RPanel, mobaPlayer.Hero.SlotFour.CooldownTimer, mobaPlayer.Hero.SlotFour);
            DrawIcon(ref CCooldown, ref CPanel, mobaPlayer.Hero.SlotFive.CooldownTimer, mobaPlayer.Hero.SlotFive);*/ //TODO - Fix icon drawing on bar
            //levelText.SetText(mobaPlayer.Hero.Level.ToString(), 0.75f, false);
            
            //lifeText.SetText(player.statLife + "/" + player.statLifeMax2 + " (+" + mobaPlayer.lifeRegen + ")", 0.75f, false);
            
            //manaText.SetText(mobaPlayer.currentResource + "/" + mobaPlayer.maxResource + " (+" + mobaPlayer.resourceRegen + ")", 0.75f, false);

            //armorText.SetText(mobaPlayer.armor.ToString(), 0.6f, false);
            //moveSpeedText.SetText(((player.maxRunSpeed / 3) * 100).ToString() + "%", 0.6f, false);

            if (player.dead) {
                deathTimer.SetText(Math.Ceiling(player.respawnTimer / 60f).ToString());
            }
            else {
                deathTimer.SetText("");
            }
        }

        public void DrawIcon(ref UIText text, ref UIAbilityIcon icon, int timer, Ability ability) {
            if(timer > 0) {
                if (timer >= 40) {
                    text.SetText(Math.Ceiling(timer / 60f)
                        .ToString());
                }
                else {
                    text.SetText((Math.Ceiling((timer / 60f) * 10) / 10f)
                        .ToString());
                }
            }
            else {
                text.SetText("");
            }

            icon.ability = ability;
        }

        public void UnLoad() {
            QCooldown = null;
            ECooldown = null;
            RCooldown = null;
            CCooldown = null;
            deathTimer = null;
            bar = null;
            characterIcon = null;
            QPanel = null;
            EPanel = null;
            RPanel = null;
            CPanel = null;
            lifeBar = null;
            resourceBar = null;
        }
    }
}