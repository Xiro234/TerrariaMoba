using Terraria;

namespace TerrariaMoba.Abilities.Osteo {
    public class LifedrainAura : Ability {
        public LifedrainAura(Player myPlayer) : base(myPlayer) {
            Name = "Lifedrain Aura";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }
    }
}