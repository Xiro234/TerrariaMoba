using System;
using TerrariaMoba;
using TerrariaMoba.Characters;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria;
using TerrariaMoba.Packets;
using Terraria.GameInput;
using Terraria.ID;
using TerrariaMoba.Players;

public static class TerrariaMobaUtils {
    public const int xpPerKill = 100;
    public static String GetTeamString(int team) {
        switch (team) {
            case 0:
                return "NONE";
            case 1:
                return "RED";
            case 2:
                return "GREEN";
            case 3:
                return "BLUE";
            case 4:
                return "YELLOW";
            case 5:
                return "PINK";
            default:
                return "NULL";
        }
    }
        
    public static Color GetTeamColor(int team) {
        switch (team) {
            case 0:
                return Color.White;
            case 1:
                return Color.Red;
            case 2:
                return Color.Green;
            case 3:
                return Color.Blue;
            case 4:
                return Color.Yellow;
            case 5:
                return Color.Pink;
            default:
                return Color.Brown;
        }
    }
        
    public static void MobaKill(int killWhoAmI) {
        //Credit to https://github.com/hamstar0/tml-playerstatistics-mod for modifications on his work
        if (killWhoAmI >= 0 && killWhoAmI < Main.player.Length) {
            var otherPlayer = Main.player[killWhoAmI].GetModPlayer<TerrariaMobaPlayer_Gameplay>();

            if (otherPlayer != null) {
                for (int i = 0; i < Main.maxPlayers; i++) {
                    Player plr = Main.player[i];
                        
                    if (plr.active && plr.team == Main.player[killWhoAmI].team) {
                        SyncExperiencePacket.Write(xpPerKill, i, killWhoAmI);
                    }
                }
            }
            else {
                Main.NewText("Invalid ModPlayer for " + Main.player[killWhoAmI].name, Color.Red);
            }
        }
        else {
            Main.NewText("Invalid player whoAmI: " + killWhoAmI, Color.Red);
        }
    }
        
    public static bool TileIsSolidOrPlatform(int x, int y){
        Tile tile = Main.tile[x, y];
        return tile != null && (tile.nactive() && (Main.tileSolid[tile.type] || Main.tileSolidTop[tile.type] && tile.frameY == 0));
    }

    public static void AssignCharacter(ref Character MyCharacter, String name, Player player) {
        switch (name) {
            case "sylvia":
                if (Main.netMode == NetmodeID.Server || Main.netMode == NetmodeID.MultiplayerClient) {
                    if (player.team == Main.LocalPlayer.team) {
                        Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().AllySylvia = player.whoAmI;
                    }
                    else {
                        Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().EnemySylvia = player.whoAmI;
                    }
                }
                else {
                    Main.LocalPlayer.GetModPlayer<TerrariaMobaPlayer_Gameplay>().AllySylvia = player.whoAmI;
                }

                if (Main.LocalPlayer == player) {
                    MyCharacter = new Sylvia();
                }
                else {
                    MyCharacter = new Sylvia(true);
                }
                break;
            default:
                Main.NewText("Invalid Character Name: AssignCharacter");
                break;
        }
    }
}