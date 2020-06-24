using Terraria;

namespace TerrariaMoba.Abilities.Flibnob
{
    public class Earthsplitter : Ability
    {
        public Earthsplitter(Player myPlayer) : base(myPlayer) {
            Name = "Earthsplitter";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateOne");
        }

        public override void OnCast()
        {
            base.OnCast();
        }

        public override void InUse()
        {
            base.InUse();
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}