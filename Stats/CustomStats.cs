using System;
using System.Collections.Generic;
using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ID;
using TerrariaMoba.Characters;
using TerrariaMoba.Packets;
using TerrariaMoba.UI;
using static TerrariaMobaUtils;
using static Terraria.ModLoader.ModContent;

namespace TerrariaMoba {
    public class CustomStats {
        public float percentThorns = 0f;
        public int shield = 0;
        public int maxHealth = 0;
        public int maxResource = 0;
        public int currentResource = 0;
        public int resourceRegen = 0;
        public int armor = 0;

        public void Send(BinaryWriter writer) {
            writer.Write(percentThorns);
            writer.Write(shield);
            writer.Write(maxHealth);
            writer.Write(maxResource);
            writer.Write(currentResource);
            writer.Write(resourceRegen);
            writer.Write(armor);
        }

        public void Recieve(BinaryReader reader) {
            percentThorns = reader.ReadSingle();
            shield = reader.ReadInt32();
            maxHealth = reader.ReadInt32();
            maxResource = reader.ReadInt32();
            currentResource = reader.ReadInt32();
            resourceRegen = reader.ReadInt32();
            armor = reader.ReadInt32();
        }
    }
}