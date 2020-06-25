using Terraria;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Flibnob
{
    public class TitaniumShell : Ability
    {
        public TitaniumShell(Player myPlayer) : base(myPlayer) {
            Name = "Titanium Shell";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobAbilityTwo");
        }
        
        public override void Cast()
        {
            player.AddBuff(BuffType<Buffs.TitaniumReflection>(), 3 * 60);
            Cooldown = 10 * 60;
        }
    }
}