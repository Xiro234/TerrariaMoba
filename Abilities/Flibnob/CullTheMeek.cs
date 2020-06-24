using Terraria;

namespace TerrariaMoba.Abilities.Flibnob
{
    public class CullTheMeek : Ability
    {
        public CullTheMeek(Player myPlayer) : base(myPlayer) {
            Name = "Cull the Meek";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobUltimateTwo");
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