using Microsoft.Xna.Framework.Graphics;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Nocturne {
    public class EclipteranLightbringer : Ability {
        public EclipteranLightbringer() : base("Eclipteran Lightbringer", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public override void OnCast() {
            //TODO - Light shines through the titanium on nocturnes armor; changes A1,2,3 and Trait:
            //T = leave behind damaging trail
            //A1 = throws a spear of light that burns enemies with a dot
            //A2 = grants selfshield that gives 40/40 armor/mr, armor/mr decays when shield falls
            //A3 = aoe ally ms buff
        }
    }
}