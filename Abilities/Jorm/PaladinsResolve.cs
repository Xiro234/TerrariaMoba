using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class PaladinsResolve : Ability {
        public PaladinsResolve() : base("Paladin's Resolve", 0, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public const int MIGHT_MAX_STACK = 4;
        public bool canEmpower = false;
        private int currentStacks;

        public void AddStack() {
            if (currentStacks < MIGHT_MAX_STACK) {
                currentStacks++;
            }
        }

        public override void OnCast() {
            //TODO - hephaestan might, 4 stacks = empowered ability | 10 armor
            if (currentStacks == MIGHT_MAX_STACK) {
                Main.NewText("hmight reached max stacks");
            } else {
                Main.NewText("hmight not max stacks - " + currentStacks);
            }
        }

        public override void WhileActive() {
            // armor += 10 (?)
            if (currentStacks == MIGHT_MAX_STACK) {
                canEmpower = true;
            }
        }
        
        //talent that upgrades trait - if nearest ally is below 25%hp, take 25% of damage they take.
    }
}