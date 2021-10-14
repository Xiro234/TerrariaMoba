using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Network;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class CharacterSelect : UIState {
        private const int fromEdge = 18;
        private const int spacing = 68;
        private List<CharacterIcon> iconList;
        private UIImage background;
        private UIImage checkmark;
        
        public override void OnInitialize() {
            iconList = new List<CharacterIcon>();
            
            foreach (var heroType in CharacterManager.CharacterTypesList) {
                iconList.Add(new CharacterIcon((Character)Activator.CreateInstance(heroType)));
            }
            
            background = new UIImage(ModContent.Request<Texture2D>("TerrariaMoba/Textures/CharacterSelect", AssetRequestMode.ImmediateLoad));
            background.VAlign = 0.5f;
            background.HAlign = 0.5f;
            Append(background);

            for (int i = 0; i < iconList.Count; i++) {
                iconList[i].Left.Set(fromEdge + (spacing * (i % 5)), 0);
                iconList[i].Top.Set(fromEdge + (spacing * (i / 5)), 0);
                background.Append(iconList[i]);
            }
            
            checkmark = new UIImage(ModContent.Request<Texture2D>("TerrariaMoba/Textures/CheckMarkUnselected", AssetRequestMode.ImmediateLoad));
            checkmark.Left.Set(306, 0);
            checkmark.Top.Set(156, 0);
            checkmark.OnClick += OnCheckClick;
            checkmark.OnMouseOver += OnCheckMouseOver;

            background.Append(checkmark);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            
            if (ContainsPoint(Main.MouseScreen)) {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (mobaPlayer.selectedCharacter == null) {
                checkmark.SetImage(ModContent.Request<Texture2D>("TerrariaMoba/Textures/CheckMarkUnselected", AssetRequestMode.ImmediateLoad));
            }
            else {
                checkmark.SetImage(ModContent.Request<Texture2D>("TerrariaMoba/Textures/CheckMarkSelected", AssetRequestMode.ImmediateLoad));
            }
        }

        public void OnCheckClick(UIMouseEvent evt, UIElement listeningElement) {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            if (mobaPlayer.selectedCharacter != null) {
                
                if (Main.netMode != NetmodeID.SinglePlayer) {
                    NetworkHandler.SendAssignCharacter(Main.LocalPlayer.whoAmI);
                }
                
                SoundEngine.PlaySound(SoundID.MenuClose);
                MobaSystem.HideSelect();
            }
        }

        public void OnCheckMouseOver(UIMouseEvent evt, UIElement listeningElement) {
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
    }
}