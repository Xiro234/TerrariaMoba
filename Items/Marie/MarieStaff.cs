using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerrariaMoba.Projectiles.Marie;

namespace TerrariaMoba.Items.Marie {
    public class MarieStaff : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Marie's main weapon.\nA staff imbued with the waters of Lacusia.\nIf you look close enough at the crystal, you can see crashing tides.");
            DisplayName.SetDefault("Staff of Tides");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults() {
            Item.width = 34;
            Item.height = 34;
            Item.damage = 69;
            Item.knockBack = 0;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 54;
            Item.useAnimation = 54;
            Item.shootSpeed = 7.33f;
            Item.shoot = ModContent.ProjectileType<SoTBolt>();
            Item.UseSound = SoundID.Item43;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.autoReuse = false;
        }
    }
}