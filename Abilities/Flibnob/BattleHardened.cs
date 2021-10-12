using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Flibnob {
    public class BattleHardened : Ability {
        public BattleHardened() : base("Battle Hardened", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Flibnob/FlibnobTrait").Value; }

        public const int BASE_ARMOR_GAIN = 1;
        public const int BASE_MR_GAIN = 0;
    }
}
/*
public override void WhileActive() {
    float change = (float) (User.statLifeMax2 - User.statLife) / User.statLifeMax2 * 100f;
    int defBoost = (int) Math.Floor(change / 2f);
    if (defBoost != oldDefBoost) {
        User.GetModPlayer<MobaPlayer>().armor += defBoost - oldDefBoost;
        oldDefBoost = defBoost;
    }
}*/