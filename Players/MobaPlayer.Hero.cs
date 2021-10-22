﻿using System;
using System.Linq;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public int CurrentResource { get; set; }
        public Character Hero { get; set; }
        public Type selectedCharacter;
        //TODO - STATISTACTS
        public int lifeRegenTimer = 0;
        public int resourceRegenTimer = 0;

        public void RegenLife() {
            lifeRegenTimer++;
            
            if (lifeRegenTimer == 60) {
                float healthRegenFromMax = (Player.statLifeMax2 * 0.125f / 60f);

                float healthRegen = (healthRegenFromMax + Hero.BaseStatistics.HealthRegen + FlatStats.HealthRegen) * (1 + MultiplicativeStats.HealthRegen);
                Player.statLife += (int)Math.Ceiling(healthRegen);

                if (Player.statLife > Player.statLifeMax2) {
                    Player.statLife = Player.statLifeMax2;
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
            Player.statLifeMax2 = (int)Math.Ceiling(((Hero?.BaseStatistics.MaxHealth ?? 100f) + FlatStats.MaxHealth) * (1 + MultiplicativeStats.MaxHealth));
        }

        public float GetCurrentAttributeValue(AttributeType attribute) {
            float value;
            Hero.BaseAttributes.TryGetValue(attribute, out value);

            value += EffectList.Where(effect => effect.FlatAttributes.ContainsKey(attribute)).Sum(effect => effect.FlatAttributes[attribute]);
            float mult = EffectList.Where(effect => effect.MultAttributes.ContainsKey(attribute)).Sum(effect => effect.MultAttributes[attribute]);

            return value * mult;
        }
    }
}