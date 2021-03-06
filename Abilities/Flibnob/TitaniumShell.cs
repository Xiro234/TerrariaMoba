/*using System;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba.Abilities.Flibnob {
    [Serializable]
    public class TitaniumShell : Ability {
        public TitaniumShell(Player myPlayer) : base(myPlayer) {
            Name = "Titanium Shell";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobAbilityTwo");
        }
        
        public override void Cast() {
            User.AddBuff(BuffType<Buffs.TitaniumReflection>(), 3 * 60);
            cooldownTimer = 10 * 60;
        }
    }
}*/