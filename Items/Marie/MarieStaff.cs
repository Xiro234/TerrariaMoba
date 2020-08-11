using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TerrariaMoba.Items.Marie {
    public class MarieStaff : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Marie's main weapon.\nA staff imbued with the waters of Lacusia.\nIf you look close enough at the crystal, you can see crashing tides.");
            DisplayName.SetDefault("Staff of Tides");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults() {
            item.width = 34;
            item.height = 34;
            item.damage = 62;
            item.knockBack = 0;
            item.ranged = true;
            item.useTime = 60;
            item.useAnimation = 60;
            item.shootSpeed = 9;
            item.shoot = mod.ProjectileType("SoTBolt");
            item.UseSound = SoundID.Item43;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.value = 10000;
            item.rare = ItemRarityID.Cyan;
            item.autoReuse = false;
        }
    }
}