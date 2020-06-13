using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;

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
            item.rare = 3;
            item.color = Color.Yellow;
        }
        
        public override string Texture => "Terraria/Item_" + ItemID.Abeemination;
        
        public override bool UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                bool canStart = true;
                for (int i = 0; i < Main.maxPlayers; i++) {
                    if (Main.player[i] != null && Main.player[i].active) {
                        if (!Main.player[i].GetModPlayer<MobaPlayer>().CharacterPicked) {
                            canStart = false;
                        }
                    }
                }

                if (canStart) {
                    player.GetModPlayer<MobaPlayer>().MyCharacter.ChooseCharacter();
                    player.GetModPlayer<MobaPlayer>().StartGame();
                    if (Main.netMode == NetmodeID.MultiplayerClient) {
                        Packets.SyncGameStartPacket.Write();
                    }

                    Main.NewText("Game Started!");
                }
                else {
                    Main.NewText("Not everyone has chosen a character!");
                }
            }

            return true;
        }
    }
}