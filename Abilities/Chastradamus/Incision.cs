﻿using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class Incision : Ability {
        public Incision() : base("Incision", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Chastradamus slashes his surgical blade with confidence, inflicting Ruptured for 3s.
        }
    }
}