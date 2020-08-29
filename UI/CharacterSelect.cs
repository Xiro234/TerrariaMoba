using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using TerrariaMoba.Characters;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.UI {
    public class CharacterSelect : UIState {
        private const int fromEdge = 18;
        private const int spacing = 68;
        private List<CharacterIcon> iconList;
        private const int numCharacterPortraits = 10;
        private UIImage background;
        private UIImage checkmark;
        
        public override void OnInitialize() {
            iconList = new List<CharacterIcon>();
            
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon"),
                CharacterEnum.Sylvia));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieIcon"),
                CharacterEnum.Marie));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobIcon"),
                CharacterEnum.Flibnob));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Osteo/OsteoIcon"),
                CharacterEnum.Osteo));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Nocturne/NocturneIcon"),
                CharacterEnum.Nocturne));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Chastradamus/ChastradamusIcon"),
                CharacterEnum.Chastradamus));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/OldMan/OldManIcon"),
                CharacterEnum.OldMan));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Jorm/JormIcon"),
                CharacterEnum.Jorm));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            
            background = new UIImage(TerrariaMoba.Instance.GetTexture("Textures/CharacterSelect"));
            background.VAlign = 0.5f;
            background.HAlign = 0.5f;
            Append(background);

            for (int i = 0; i < numCharacterPortraits; i++) {
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

            if (mobaPlayer.CharacterSelected == CharacterEnum.Null) {
                checkmark.SetImage(TerrariaMoba.Instance.GetTexture("Textures/CheckMarkUnselected"));
            }
            else {
                checkmark.SetImage(TerrariaMoba.Instance.GetTexture("Textures/CheckMarkSelected"));
            }
        }

        public void OnCheckClick(UIMouseEvent evt, UIElement listeningElement) {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            if (mobaPlayer.CharacterSelected != CharacterEnum.Null) {
                switch (mobaPlayer.CharacterSelected) {
                    case (CharacterEnum.Sylvia):
                        mobaPlayer.MyCharacter = new Sylvia(Main.LocalPlayer);
                        mobaPlayer.MyCharacter.SyncTalents();
                        break;
                    case (CharacterEnum.Marie):
                        mobaPlayer.MyCharacter = new Marie(Main.LocalPlayer);
                        mobaPlayer.MyCharacter.SyncTalents();
                        break;
                    case (CharacterEnum.Flibnob):
                        mobaPlayer.MyCharacter = new Flibnob(Main.LocalPlayer);
                        mobaPlayer.MyCharacter.SyncTalents();
                        break;
                    case (CharacterEnum.Osteo):
                        mobaPlayer.MyCharacter = new Osteo(Main.LocalPlayer);
                        mobaPlayer.MyCharacter.SyncTalents();
                        break;
                }

                Main.PlaySound(11);
                TerrariaMoba.Instance.HideSelect();
            }
        }

        public void OnCheckMouseOver(UIMouseEvent evt, UIElement listeningElement) {
            Main.PlaySound(12);
        }
    }
}