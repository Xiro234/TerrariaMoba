using System;
using TerrariaMoba;
using TerrariaMoba.Characters;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria;
using Terraria.GameInput;


namespace TerrariaMoba {
    public class TerrariaMobaUtils {
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
    }
}