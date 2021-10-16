using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Chat;
using Terraria.Localization;
using TerrariaMoba.Network;

namespace TerrariaMoba.Items {
    public class DebugStartGame : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Starts the game");
            DisplayName.SetDefault("DebugStartGame");
        }

        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 1;
            Item.useTime = 1;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = false;
            Item.rare = ItemRarityID.Orange;
            Item.color = Color.Yellow;
        }
        
        public override string Texture => "Terraria/Images/Item_" + ItemID.Abeemination;

        public override bool? UseItem(Player Player) {
            if (Main.netMode == NetmodeID.Server) {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Match Started!"), Color.Aqua);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer) {
                Main.NewText("Match Started!", Color.Aqua);
            }

            if (Main.netMode == NetmodeID.SinglePlayer) {
                TerrariaMobaUtils.StartGame();
            }

            if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer) {
                TerrariaMobaUtils.StartGame();
                NetworkHandler.SendStartGame();
            }

            return true;
        }
    }
}