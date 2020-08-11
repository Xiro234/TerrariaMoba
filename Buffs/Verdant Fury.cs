using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using TerrariaMoba.Players;

namespace TerrariaMoba.Buffs {
    public class VerdantFuryBuff : ModBuff {
        public override void SetDefaults() {
            DisplayName.SetDefault("Verdant Fury");
            Description.SetDefault("The Jungle's power flows through you!");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex) {
            player.GetModPlayer<MobaPlayer>().SylviaEffects.VerdantFury = true;
            Dust.NewDust(player.position, player.width, player.height, 57, 0,
                0, 150, Color.LightGreen, 0.7f);
        }
    }
}