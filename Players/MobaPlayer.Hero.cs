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
            lifeRegenTimer++;
            
            if (lifeRegenTimer == 60) {
                float healthRegenFromMax = (player.statLifeMax2 * 0.125f / 60f);
                player.statLife += (int)Math.Ceiling(healthRegenFromMax + Stats.HealthRegen);

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

                if (CurrentResource > Stats.MaxResource) {
                    CurrentResource = Stats.MaxResource;
                }

                resourceRegenTimer = 0;
            }
        }

        public void ResetStats() {
            Stats.ResetStats();
            Stats.AttackDamage = Hero?.BaseStatistics.AttackDamage ?? 0;
            Stats.AttackSpeed = Hero?.BaseStatistics.AttackSpeed ?? 0;
            Stats.AttackVelocity = Hero?.BaseStatistics.AttackVelocity ?? 0;
            Stats.HealthRegen = Hero?.BaseStatistics.HealthRegen ?? 0;
            Stats.MagicalArmor = Hero?.BaseStatistics.MagicalArmor ?? 0;
            Stats.PhysicalArmor = Hero?.BaseStatistics.PhysicalArmor ?? 0;
            Stats.MaxHealth = Hero?.BaseStatistics.MaxHealth ?? 100;
            Stats.MaxResource = Hero?.BaseStatistics.MaxResource ?? 0;
            Stats.ResourceRegen = Hero?.BaseStatistics.ResourceRegen ?? 0;
            Stats.ResourceType = Hero?.BaseStatistics.ResourceType ?? Resource.Mana;
            
            player.statLifeMax2 = (int) Math.Ceiling(Stats.MaxHealth);
        }
    }
}