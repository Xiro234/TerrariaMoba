using System;
using TerrariaMoba;
using TerrariaMoba.Characters;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Packets;
using Terraria.GameInput;
using Terraria.ID;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;
using TerrariaMoba.NPCs;

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
        
    public static void MobaKill(int killer, int deadPlayer) {
        //Credit to https://github.com/hamstar0/tml-playerstatistics-mod for modifications on his work
        if (killer >= 0 && killer < Main.player.Length) {
            var killerModPlayer = Main.player[killer].GetModPlayer<MobaPlayer>();

            if (killerModPlayer != null) {
                for (int i = 0; i < Main.maxPlayers; i++) {
                    Player plr = Main.player[i];
                        
                    if (plr.active && plr.team == Main.player[killer].team) {
                        //ExperiencePacket.Write(xpPerKill, i, killWhoAmI);
                    }
                }
                killerModPlayer.MyCharacter.SlayEffect(Main.player[deadPlayer]);

                for (int i = 0; i < Main.maxPlayers; i++) {
                    if (Main.player[i] != null && Main.player[i].active) {
                        if (Main.player[i].team == Main.player[killer].team) {
                            Main.player[i].GetModPlayer<MobaPlayer>().MyCharacter.TeamSlayEffect(Main.player[deadPlayer]);
                        }
                    }
                }
            }
            else {
                Main.NewText("Invalid ModPlayer for " + Main.player[killer].name, Color.Red);
            }
        }
        else {
            Main.NewText("Invalid player whoAmI: " + killer, Color.Red);
        }
    }
        
    public static bool TileIsSolidOrPlatform(int x, int y){
        Tile tile = Main.tile[x, y];
        return tile != null && (tile.nactive() && (Main.tileSolid[tile.type] || Main.tileSolidTop[tile.type] && tile.frameY == 0));
    }

    public static void AssignCharacter(ref Character MyCharacter, CharacterEnum character, Player player) {
        switch (character) {
            case CharacterEnum.Sylvia:
                MyCharacter = new Sylvia(player);
                break;
            case CharacterEnum.Marie:
                MyCharacter = new Marie(player);
                break;
            case CharacterEnum.Flibnob:
                MyCharacter = new Flibnob(player);
                break;
            case CharacterEnum.Osteo:
                MyCharacter = new Osteo(player);
                break;
            default:
                Main.NewText("Invalid Character: AssignCharacter");
                break;
        }
    }

    public static string GetHoverText(Texture2D texture) {
        return "";
    }
    
    public static double Conv2Rad(double angle) {
        return (Math.PI / 180) * angle;
    }
    
    public static byte[] ReadAllBytes(Stream stream) {
        using (var ms = new MemoryStream()) {
            stream.CopyTo(ms);
            return ms.ToArray();
        }
    }

    public static bool TargetClosestEnemy(NPC npc) {
        float distance = Single.MaxValue;
        int target = -1;
        Player sourcePlayer = Main.player[npc.GetGlobalNPC<MobaGlobalNPC>().owner];
        
        for (int i = 0; i < Main.maxPlayers; i++) {
            Player targetPlayer = Main.player[i];
            if (sourcePlayer.team != targetPlayer.team) {
                float length = Vector2.Distance(sourcePlayer.position, targetPlayer.position);
                if (length < distance) {
                    distance = length;
                    target = targetPlayer.whoAmI;
                }
            }
        }

        if (target >= 0 && target <= 255) {
            BaseAI.SetTarget(npc, target);
            return true;
        }
        else {
            return false;
        }
    }
}