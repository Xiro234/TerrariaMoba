﻿using System;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public Statistics FlatStats { get; set; }
        public Statistics MultiplicativeStats { get; set; }
        public int CurrentResource { get; set; }
        public Character Hero { get; set; }
        public Type selectedCharacter;
        
        public int lifeRegenTimer = 0;
        public int resourceRegenTimer = 0;

        public void RegenLife() {
            lifeRegenTimer++;
            
            if (lifeRegenTimer == 60) {
                float healthRegenFromMax = (player.statLifeMax2 * 0.125f / 60f);

                float healthRegen = (healthRegenFromMax + Hero.BaseStatistics.HealthRegen + FlatStats.HealthRegen) * (1 + MultiplicativeStats.HealthRegen);
                player.statLife += (int)Math.Ceiling(healthRegen);

                if (player.statLife > player.statLifeMax2) {
                    player.statLife = player.statLifeMax2;
                }

                lifeRegenTimer = 0;
            }
        }

        public void RegenResource() {
            resourceRegenTimer++;

            if (resourceRegenTimer == 60) {
                Hero.RegenResource();
                resourceRegenTimer = 0;
            }
        }

        public void ResetStats() {
            FlatStats.ResetStats();
            MultiplicativeStats.ResetStats();
        }

        public void SetPlayerStats() {
            player.statLifeMax2 = (int)Math.Ceiling(((Hero?.BaseStatistics.MaxHealth ?? 100f) + FlatStats.MaxHealth) * (1 + MultiplicativeStats.MaxHealth));
        }
    }
}