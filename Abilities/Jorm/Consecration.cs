﻿using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class Consecration : Ability {
        public Consecration() : base("Consecration", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - consecrate ground in ~ radius, +healeff of allies, -healeff of enemies on top.
        }
    }
}