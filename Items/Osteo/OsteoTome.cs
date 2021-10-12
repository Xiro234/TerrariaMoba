using Terraria.ModLoader;
using Terraria.ID;
using TerrariaMoba.Projectiles.Osteo;

namespace TerrariaMoba.Items.Osteo {
    public class OsteoTome : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Osteo's main weapon.\nA very elegant looking book on the outside.\nAttempting to read it is highly inadvisable.");
            DisplayName.SetDefault("Forbidden Tome");
        }

        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 12;
            Item.damage = 83;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<OsteoSkull>();
            Item.UseSound = SoundID.Item104.WithVolume(0.3f).WithPitchVariance(0.5f);
            Item.useTime = 57;
            Item.useAnimation = 57;
            Item.shootSpeed = 7.66f;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 0;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = false;
        }
    }
}