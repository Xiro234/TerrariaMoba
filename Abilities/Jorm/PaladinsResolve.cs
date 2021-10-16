using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Jorm {
    public class PaladinsResolve : Ability {
        public PaladinsResolve() : base("Paladin's Resolve", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        private int CurrentStacks;
        private bool OnCourage = false;
        private bool OnWisdom = false;

        public override void OnCast() {
            if (!OnCourage && !OnWisdom) {
                OnCourage = true;
            }

            if (OnCourage) {
                OnCourage = false;
                //clear courage buff
                OnWisdom = true;
            }

            if (OnWisdom) {
                OnWisdom = false;
                //clear wisdom buff
                OnCourage = true;
            }
            
            CooldownTimer = BaseCooldown;
        }

        public override void WhileActive() {
            /*
             * on ability cast, CurrentStacks++ [this might be on each ability but not sure]
             * if(OnCourage)
             *      give player courage with current CurrentStacks
             * elseif(OnWisdom)
             *      give player wisdom with current CurrentStacks
             */
        }
    }
}