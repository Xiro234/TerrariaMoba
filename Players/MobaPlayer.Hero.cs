using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public int CurrentResource { get; set; }
        public Character Hero { get; set; }
        public Type selectedCharacter;
        public int lifeRegenTimer = 0;
        public int resourceRegenTimer = 0;

        public void RegenLife() {
            lifeRegenTimer++;

            bool isOsteo = Hero is Osteo;
            if (lifeRegenTimer == 60 && !isOsteo) {
                float healthRegenFromMax = (Player.statLifeMax2 * 0.125f / 60f);

                float healthRegen = (healthRegenFromMax + GetCurrentAttributeValue(AttributeType.HEALTH_REGEN));
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
                Hero.RegenResource(GetCurrentAttributeValue(AttributeType.MAX_MANA));
                resourceRegenTimer = 0;
            }
        }

        public void SetPlayerHealth() {
            Player.statLifeMax2 = (int)Math.Floor(GetCurrentAttributeValue(AttributeType.MAX_HEALTH));
        }

        public void SetPlayerResource() {
            CurrentResource = (int)Math.Floor(GetCurrentAttributeValue(AttributeType.MAX_MANA));
        }

        public float GetCurrentAttributeValue(AttributeType attribute) {
            float value = Hero.BaseAttributes.ContainsKey(attribute) ? Hero.BaseAttributes[attribute]() : 0f;
            float mult = 1f;

            value += Hero.Skills.Sum(e => e.PassiveFlatAttributes.ContainsKey(attribute) ? e.PassiveFlatAttributes[attribute]() : 0);
            mult += Hero.Skills.Sum(e => e.PassiveMultAttributes.ContainsKey(attribute) ? e.PassiveMultAttributes[attribute]() : 0);

            value += EffectList.Sum(e => e.FlatAttributes.ContainsKey(attribute) ? e.FlatAttributes[attribute]() : 0);
            mult += EffectList.Sum(e => e.MultAttributes.ContainsKey(attribute) ? e.MultAttributes[attribute]() : 0);

            switch (attribute) {
                case AttributeType.ATTACK_SPEED:
                    return value + mult;
                default:
                    return value * mult;
            }
        }

        public void HealOtherPlayer(Player target, int amount, bool doText) {
            AbilityEffectManager.OnHealOtherPlayer(Player, target, ref amount, ref doText);
            target.GetModPlayer<MobaPlayer>().HealMe(amount, doText);
        }
        
        public void HealMe(int amount, bool doText) {
            AbilityEffectManager.OnHeal(Player, ref amount, ref doText);

            int modifiedAmount = (int)Math.Ceiling(amount * GetCurrentAttributeValue(AttributeType.HEALING_EFFECTIVENESS));

            if (doText) {
                CombatText.NewText(Player.Hitbox, CombatText.HealLife, modifiedAmount, false);
            }
            Player.statLife += modifiedAmount;
            if (Player.statLife > Player.statLifeMax2) {
                Player.statLife = Player.statLifeMax2;
            }
        }
    }
}