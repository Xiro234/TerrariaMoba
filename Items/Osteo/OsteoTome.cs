using BaseMod;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Items.Osteo {
    public class OsteoTome : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Osteo's main weapon.");
            DisplayName.SetDefault("Placeholder Name");
        }

        public override void SetDefaults() {
            item.damage = 20;
            item.ranged = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("OsteoSkull");
            item.UseSound = SoundID.Item104.WithVolume(0.3f).WithPitchVariance(0.5f);
            item.width = 20;
            item.height = 12;
            item.useTime = 20;
            item.shootSpeed = 6;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 0;
            item.value = 10000;
            item.rare = 2;
            item.autoReuse = false;
        }
    }
}