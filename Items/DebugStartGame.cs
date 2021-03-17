using Terraria;
using Terraria.ModLoader;
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
            item.useAnimation = 10;
            item.useTime = 10;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = false;
            item.rare = 3;
            item.color = Color.Yellow;
        }
        
        public override string Texture => "Terraria/Item_" + ItemID.Abeemination;
        
        public override bool UseItem(Player player) {
            if (!Main.dedServ) {
                bool canStart = true;
                for (int i = 0; i < Main.maxPlayers; i++) {
                    if (Main.player[i].active) {
                        if (Main.player[i].GetModPlayer<MobaPlayer>().Hero == null) {
                            canStart = false;
                            break;
                        }
                    }
                }

                if (canStart) {
                    for (int i = 0; i < Main.maxPlayers; i++) {
                        if (Main.player[i].active) {
                            var plr = Main.player[i].GetModPlayer<MobaPlayer>();
                            plr.StartGame();
                        }
                    }
                    Main.NewText("Game Started!");
                }
                
            }
            else {
                Main.NewText("Not everyone has chosen a character!");
            }

            return true;
        }
    }
}