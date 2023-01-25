using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.UI {
    public class MobaBar : UIState {
        private static UIText SlotOneCooldown;
        private static UIText SlotTwoCooldown;
        private static UIText SlotThreeCooldown;
        private static UIText SlotFourCooldown;
        private static UIText SlotFiveCooldown;
        private static UIText deathTimer;
        private static UIImage bar;
        private static UIImage characterIcon;
        private static UIAbilityIcon SlotOneIcon;
        private static UIAbilityIcon SlotTwoIcon;
        private static UIAbilityIcon SlotThreeIcon;
        private static UIAbilityIcon SlotFourIcon;
        private static UIAbilityIcon SlotFiveIcon;
        private ResourceBar lifeBar;
        private ResourceBar resourceBar;

        public override void OnInitialize() {
            bar = new UIImage(ModContent.Request<Texture2D>("TerrariaMoba/Textures/MobaBarBackground", AssetRequestMode.ImmediateLoad));
            bar.VAlign = 0.95f;
            bar.HAlign = 0.05f;
            Append(bar);

            SlotOneIcon = new UIAbilityIcon(ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock", AssetRequestMode.ImmediateLoad).Value);
            SlotOneIcon.Top.Set(48, 0);
            SlotOneIcon.Left.Set(98, 0);
            
            SlotOneCooldown = new UIText("");
            SlotOneCooldown.VAlign = 0.5f;
            SlotOneCooldown.HAlign = 0.5f;
            SlotOneIcon.Append(SlotOneCooldown);

            SlotTwoIcon = new UIAbilityIcon(ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock", AssetRequestMode.ImmediateLoad).Value);
            SlotTwoIcon.Top.Set(48, 0);
            SlotTwoIcon.Left.Set(160, 0);
            
            SlotTwoCooldown = new UIText("");
            SlotTwoCooldown.VAlign = 0.5f;
            SlotTwoCooldown.HAlign = 0.5f;
            SlotTwoIcon.Append(SlotTwoCooldown);
            
            SlotThreeIcon = new UIAbilityIcon(ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock", AssetRequestMode.ImmediateLoad).Value);
            SlotThreeIcon.Top.Set(48, 0);
            SlotThreeIcon.Left.Set(222, 0);
            
            SlotThreeCooldown = new UIText("");
            SlotThreeCooldown.VAlign = 0.5f;
            SlotThreeCooldown.HAlign = 0.5f;
            SlotThreeIcon.Append(SlotThreeCooldown);
            
            SlotFourIcon = new UIAbilityIcon(ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock", AssetRequestMode.ImmediateLoad).Value);
            SlotFourIcon.Top.Set(48, 0);
            SlotFourIcon.Left.Set(284, 0);
            
            SlotFourCooldown = new UIText("");
            SlotFourCooldown.VAlign = 0.5f;
            SlotFourCooldown.HAlign = 0.5f;
            SlotFourIcon.Append(SlotFourCooldown);
            
            SlotFiveIcon = new UIAbilityIcon(ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock", AssetRequestMode.ImmediateLoad).Value);
            SlotFiveIcon.Top.Set(48, 0);
            SlotFiveIcon.Left.Set(346, 0);
            
            SlotFiveCooldown = new UIText("");
            SlotFiveCooldown.VAlign = 0.5f;
            SlotFiveCooldown.HAlign = 0.5f;
            SlotFiveIcon.Append(SlotFiveCooldown);
            
            lifeBar = new ResourceBar(Resource.Life);
            lifeBar.Left.Set(146, 0);
            lifeBar.Top.Set(6, 0);
            
            resourceBar = new ResourceBar(Resource.Mana);
            resourceBar.Left.Set(146, 0);
            resourceBar.Top.Set(22, 0);

            characterIcon = new UIImage(ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock", AssetRequestMode.ImmediateLoad));
            characterIcon.Left.Set(18, 0);
            characterIcon.Top.Set(38, 0);

            deathTimer = new UIText("");
            deathTimer.VAlign = 0.5f;
            deathTimer.HAlign = 0.5f;
            characterIcon.Append(deathTimer);

            bar.Append(lifeBar);
            bar.Append(resourceBar);
            bar.Append(characterIcon);
            
            bar.Append(SlotOneIcon);
            bar.Append(SlotTwoIcon);
            bar.Append(SlotThreeIcon);
            bar.Append(SlotFourIcon);
            bar.Append(SlotFiveIcon);
        }

        public void SetIcons() {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            characterIcon.SetImage(mobaPlayer.Hero.CharacterIcon);
            SlotOneIcon.SetAbility(mobaPlayer.Hero.BasicAbilityOne);
            SlotTwoIcon.SetAbility(mobaPlayer.Hero.BasicAbilityTwo);
            SlotThreeIcon.SetAbility(mobaPlayer.Hero.BasicAbilityThree);
            SlotFourIcon.SetAbility(mobaPlayer.Hero.Ultimate);
            SlotFiveIcon.SetAbility(mobaPlayer.Hero.Trait);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            
            if (bar.ContainsPoint(Main.MouseScreen)) {
                Main.LocalPlayer.mouseInterface = true;
            }
            
            var Player = Main.LocalPlayer;

            if (Player.dead) {
                deathTimer.SetText(Math.Ceiling(Player.respawnTimer / 60f).ToString());
            }
            else {
                deathTimer.SetText("");
            }
        }

        public void DrawIcon(ref UIText text, ref UIAbilityIcon icon, int timer, Ability ability) {
            if(timer > 0) {
                if (timer >= 40) {
                    text.SetText(Math.Ceiling(timer / 60f).ToString());
                }
                else {
                    text.SetText((Math.Ceiling((timer / 60f) * 10) / 10f).ToString());
                }
            }
            else {
                text.SetText("");
            }
        }

        public void UnLoad() {
            SlotOneCooldown = null;
            SlotTwoCooldown = null;
            SlotThreeCooldown = null;
            SlotFourCooldown = null;
            deathTimer = null;
            bar = null;
            characterIcon = null;
            SlotOneIcon = null;
            SlotTwoIcon = null;
            SlotThreeIcon = null;
            SlotFourIcon = null;
            lifeBar = null;
            resourceBar = null;
        }
    }
}