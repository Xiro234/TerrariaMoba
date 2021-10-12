using Terraria.ModLoader;
using Terraria.ID;

namespace TerrariaMoba.Items.Flibnob {
    public class FlibnobAxe : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Flibnob's main weapon.\nA titanium-infused axe with molten edges.\nVery heavy and hard to swing.");
            DisplayName.SetDefault("Ogre Headsplitter");
        }
        
        public override void SetDefaults() {
            Item.width = 56;
            Item.height = 56;
            Item.damage = 137;
            Item.knockBack = 0;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = 70;
            Item.useAnimation = 70;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;
            Item.autoReuse = false;
        }
    }
}