using System;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public Statistics Stats { get; set; }
        public float CurrentResource { get; set; }
        public Character Hero { get; set; }
        public Type selectedCharacter;


        public int lifeRegenTimer = 0;
        public int resourceRegenTimer = 0;

        public void RegenLife() {
            /*
            lifeRegenTimer++;
            
            if (lifeRegenTimer == 60) {
                float healthRegenFromMax = (player.statLifeMax2 * 0.125f / 60f);
                player.statLife += (int)Math.Ceiling(healthRegenFromMax + Hero.BaseStatistics.HealthRegen + Stats.HealthRegen);

                if (player.statLife > player.statLifeMax2) {
                    player.statLife = player.statLifeMax2;
                }

                lifeRegenTimer = 0;
            }
            */
        }

        public void RegenResource() {
            /*
            resourceRegenTimer++;
            
            if (resourceRegenTimer == 60) {
                Hero.RegenResource();

                if (CurrentResource > Hero.BaseStatistics.MaxResource + Stats.MaxResource) {
                    CurrentResource = Hero.BaseStatistics.MaxResource + Stats.MaxResource;
                }

                resourceRegenTimer = 0;
            }
            */
        }
    }
}