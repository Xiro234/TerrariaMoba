﻿using Terraria;

namespace TerrariaMoba.Effects {
    public abstract class StatusEffect {
        public string name;
        public int timeLeft;
        public string texture;
        
        public abstract void updateAndApply(Player player);
    }
}