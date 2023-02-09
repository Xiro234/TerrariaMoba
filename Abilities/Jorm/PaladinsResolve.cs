using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Abilities.Jorm {
    public class PaladinsResolve : Ability {
        public PaladinsResolve(Player player) : base(player, "Paladin's Resolve", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Jorm/JormTrait").Value; }

        public const int RESOLVE_STACK_CAP = 5;
        public const int COURAGE_ARMOR_BONUS = 5;
        public const float COURAGE_REGEN_BONUS = 0.5f;
        public const int WISDOM_MAGRES_BONUS = 5;
        public const float WISDOM_REGEN_BONUS = 0.5f;
        public const int STACK_DECAY_TIMER = 180;

        private int currentStacks;
        private int stackTimer;
        private bool abilityCasted;
        private bool onCourage;
        private bool onWisdom;

        public void AddStack() {
            if (IsActive) {
                if (currentStacks != RESOLVE_STACK_CAP) {
                    currentStacks++;
                }
                abilityCasted = true;
                stackTimer = STACK_DECAY_TIMER;
            }
        }

        public override void OnCast() {
            if (!IsActive) {
                IsActive = true;
            }

            if (!onCourage && !onWisdom) {
                onCourage = true;
            } else if (onCourage) {
                onCourage = false;
                stackTimer = 0;
                currentStacks = 0;
                onWisdom = true;
            } else if (onWisdom) {
                onWisdom = false;
                stackTimer = 0;
                currentStacks = 0;
                onCourage = true;
            }

            CooldownTimer = BaseCooldown;
        }

        public override void WhileActive() {
            if (stackTimer > 0) {
                stackTimer--;
            } else {
                if (currentStacks > 0) {
                    currentStacks--;
                    stackTimer = STACK_DECAY_TIMER;
                }
            }

            ConstructFlatAttributes();
            abilityCasted = false;
        }

        public override void ConstructFlatAttributes() {
            if (onCourage) {
                PassiveFlatAttributes = new Dictionary<AttributeType, Func<float>> {
                    { PHYSICAL_ARMOR, () => COURAGE_ARMOR_BONUS * currentStacks },
                    { HEALTH_REGEN, () => COURAGE_REGEN_BONUS * currentStacks }
                };
            } else if (onWisdom) {
                PassiveFlatAttributes = new Dictionary<AttributeType, Func<float>> {
                    { MAGICAL_ARMOR, () => WISDOM_MAGRES_BONUS * currentStacks },
                    { MANA_REGEN, () => WISDOM_REGEN_BONUS * currentStacks }
                };
            } else { 
                base.ConstructFlatAttributes();
            }
        }
    }
}