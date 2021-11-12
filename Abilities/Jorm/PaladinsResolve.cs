using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Jorm;

namespace TerrariaMoba.Abilities.Jorm {
    public class PaladinsResolve : Ability {
        public PaladinsResolve(Player player) : base(player, "Paladin's Resolve", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }

        public const int RESOLVE_CAP = 5;
        public const int STACK_TIMER = 120;
        public const int COURAGE_DURATION = 180;
        public const int WISDOM_DURATION = 180;
        
        private int currentStacks;
        private int stackDecayTimer;
        private bool abilityCasted;
        private bool onCourage;
        private bool onWisdom;

        public void AddStack() {
            if (currentStacks != RESOLVE_CAP) {
                currentStacks++;
            }
            abilityCasted = true;
        }

        public override void OnCast() {
            if (!onCourage && !onWisdom) {
                onCourage = true;
            } else if (onCourage) {
                onCourage = false;
                //clear courage buff
                currentStacks = 0;
                onWisdom = true;
            } else if (onWisdom) {
                onWisdom = false;
                //clear wisdom buff
                currentStacks = 0;
                onCourage = true;
            }

            if (!IsActive) {
                IsActive = true;
            }

            CooldownTimer = BaseCooldown;
        }

        public override void WhileActive() {
            if (currentStacks > 0 && abilityCasted) {
                stackDecayTimer = STACK_TIMER;
            }

            if (stackDecayTimer > 0) {
                stackDecayTimer--;
                if (stackDecayTimer == 0 && currentStacks > 0) {
                    currentStacks--;
                    stackDecayTimer = STACK_TIMER;
                    Main.NewText("Stack decayed!");
                }
            }

            if (onCourage && abilityCasted) {
                StatusEffectManager.AddEffect(User, new ResolveCourage(currentStacks, COURAGE_DURATION, true));
                Main.NewText("Courage @ " + currentStacks + " Stacks.\nCurrent HPR: " + User.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.HEALTH_REGEN) + "\nCurrent Armor: " + User.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.PHYSICAL_ARMOR));
            } else if (onWisdom && abilityCasted) {
                StatusEffectManager.AddEffect(User, new ResolveWisdom(currentStacks, WISDOM_DURATION, true));
                Main.NewText("Wisdom @ " + currentStacks + " Stacks.\nCurrent MPR: " + User.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.MANA_REGEN) + "\nCurrent MR: " + User.GetModPlayer<MobaPlayer>().GetCurrentAttributeValue(AttributeType.MAGICAL_ARMOR));
            }

            abilityCasted = false;
        }
    }
}