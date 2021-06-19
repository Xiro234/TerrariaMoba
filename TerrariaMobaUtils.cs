using System;
using TerrariaMoba.Characters;
using Microsoft.Xna.Framework;
using System.IO;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;
using TerrariaMoba.NPCs;

namespace  TerrariaMoba {
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

        
        public static bool TileIsSolidOrPlatform(int x, int y) {
            Tile tile = Main.tile[x, y];
            return tile != null && (tile.nactive() && (Main.tileSolid[tile.type] || Main.tileSolidTop[tile.type] && tile.frameY == 0));
        }

        public static bool AssignCharacter(Player player) {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            mobaPlayer.Hero = (Character)Activator.CreateInstance(mobaPlayer.selectedCharacter, player);
            
            Item primary = new Item();
            primary.SetDefaults(mobaPlayer.Hero.PrimaryWeaponID);
            Item vanityHead = new Item();
            vanityHead.SetDefaults(mobaPlayer.Hero.HeadVanityID);
            Item vanityBody = new Item();
            vanityBody.SetDefaults(mobaPlayer.Hero.BodyVanityID);
            Item vanityLegs = new Item();
            vanityLegs.SetDefaults(mobaPlayer.Hero.LegVanityID);
            Item dyeHead = new Item();
            dyeHead.SetDefaults(mobaPlayer.Hero.HeadDyeID);
            Item dyeBody = new Item();
            dyeBody.SetDefaults(mobaPlayer.Hero.BodyDyeID);
            Item dyeLegs = new Item();
            dyeLegs.SetDefaults(mobaPlayer.Hero.LegDyeID);
            
            player.inventory[0] = primary;
            player.armor[10] = vanityHead;
            player.armor[11] = vanityBody;
            player.armor[12] = vanityLegs;
            player.dye[0] = dyeHead;
            player.dye[1] = dyeBody;
            player.dye[2] = dyeLegs;

            player.Male = mobaPlayer.Hero.IsMale;
            player.hair = mobaPlayer.Hero.HairID;
            player.hairColor = mobaPlayer.Hero.HairColor;
            player.eyeColor = mobaPlayer.Hero.EyeColor;
            player.skinColor = mobaPlayer.Hero.SkinColor;
            
            return true;
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


        public static void ClearInventory(MobaPlayer mobaPlayer) {
            for (int i = 0; i < mobaPlayer.player.inventory.Length; i++) {
                mobaPlayer.player.inventory[i] = new Item();
            }

            for (int i = 0; i < mobaPlayer.player.armor.Length; i++) {
                mobaPlayer.player.armor[i] = new Item();
            }

            for (int i = 0; i < mobaPlayer.player.dye.Length; i++) {
                mobaPlayer.player.armor[i] = new Item();
            }
        }

        public static float CoordsPerTickToTilesPerSecond(int velocity) {
            return velocity * (60f / 16f);
        }

        public static void StartGame() {
            var mobaPlayer = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            
            MobaWorld.StartGame();
            mobaPlayer.StartGame();
        }
    }
}