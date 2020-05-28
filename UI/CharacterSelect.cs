using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using TerrariaMoba.UI;
using TerrariaMoba.Enums;

namespace TerrariaMoba.UI {
    public class CharacterSelect : UIState {
        private const int fromEdge = 18;
        private const int spacing = 68;
        private List<CharacterIcon> iconList;
        private const int numCharacterPortraits = 10;
        private UIImage background;
        
        public override void OnInitialize() {
            iconList = new List<CharacterIcon>();
            
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon"),
                CharacterEnum.Sylvia));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
            iconList.Add(new CharacterIcon(TerrariaMoba.Instance.GetTexture("Textures/Lock"),
                CharacterEnum.Null));
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
                iconList[i].Top.Set(fromEdge + (spacing * (i / 5) + 1), 0);
                background.Append(iconList[i]);
            }
        }
    }
}