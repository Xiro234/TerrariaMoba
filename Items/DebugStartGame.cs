using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using TerrariaMoba.Network;

namespace TerrariaMoba.Items {
    public class DebugStartGame : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Starts the game");
            DisplayName.SetDefault("DebugStartGame");
        }

        public override void SetDefaults() {
            item.width = 20;
            item.height = 26;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 1;
            item.useTime = 1;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = false;
            item.rare = ItemRarityID.Orange;
            item.color = Color.Yellow;
        }
        
        public override string Texture => "Terraria/Item_" + ItemID.Abeemination;

        public override bool UseItem(Player player) {
            if (Main.netMode == NetmodeID.Server) {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Match Started!"), Color.Aqua);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer) {
                Main.NewText("Match Started!", Color.Aqua);
            }

            if (Main.netMode == NetmodeID.SinglePlayer) {
                TerrariaMobaUtils.StartGame();
            }

            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer) {
                NetworkHandler.SendStartGame();
            }

            return true;
        }
    }
}