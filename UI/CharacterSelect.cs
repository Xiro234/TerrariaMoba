using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
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
            
            background = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/CharacterSelect"));
            background.VAlign = 0.5f;
            background.HAlign = 0.5f;
            Append(background);

            for (int i = 0; i < iconList.Count; i++) {
                iconList[i].Left.Set(fromEdge + (spacing * (i % 5)), 0);
                iconList[i].Top.Set(fromEdge + (spacing * (i / 5)), 0);
                background.Append(iconList[i]);
            }

            checkmark = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/CheckMarkUnselected"));
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
                checkmark.SetImage(TerrariaMoba.Instance.GetTexture("Textures/CheckMarkUnselected"));
            }
            else {
                checkmark.SetImage(TerrariaMoba.Instance.GetTexture("Textures/CheckMarkSelected"));
            }
        }

        public void OnCheckClick(UIMouseEvent evt, UIElement listeningElement) {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            if (mobaPlayer.selectedCharacter != null) {
                
                if (Main.netMode != NetmodeID.Server) {
                    NetworkHandler.SendAssignCharacter(Main.LocalPlayer.whoAmI);
                }
                
                Main.PlaySound(SoundID.MenuClose);
                TerrariaMoba.Instance.HideSelect();
            }
        }

        public void OnCheckMouseOver(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(SoundID.MenuTick);
        }
    }
}