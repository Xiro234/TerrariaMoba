using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Items.Sylvia;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities {
    public class TestUseItemAbility : Ability {
        public Item testItem { get; }

        public TestUseItemAbility(Player player) : base(player, "Test", 0, 0, AbilityType.Active) {
            testItem = new Item();
            testItem.SetDefaults(ModContent.ItemType<SylviaBow>());
        }

        public override Texture2D Icon { get; }

        public override void OnCast() {
            Main.NewText("test " + testItem.Name);
        }
    }
}