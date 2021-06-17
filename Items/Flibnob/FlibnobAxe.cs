using Terraria.ModLoader;
using Terraria.ID;

namespace TerrariaMoba.Items.Flibnob {
    public class FlibnobAxe : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Flibnob's main weapon.\nA titanium-infused axe with molten edges.\nVery heavy and hard to swing.");
            DisplayName.SetDefault("Ogre Headsplitter");
        }
        
        public override void SetDefaults() {
            item.width = 56;
            item.height = 56;
            item.damage = 137;
            item.knockBack = 0;
            item.melee = true;
            item.useTime = 70;
            item.useAnimation = 70;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = 10000;
            item.rare = ItemRarityID.Orange;
            item.autoReuse = false;
        }
    }
}