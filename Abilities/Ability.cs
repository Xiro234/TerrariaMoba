using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Abilities {
    public abstract class Ability {
        public string Name { get; protected set; }
        public int BaseCooldown { get; }
        public int BaseResourceCost { get; }
        public Dictionary<AttributeType, Func<float>> PassiveFlatAttributes { get; protected set; }
        public Dictionary<AttributeType, Func<float>> PassiveMultAttributes { get; protected set; }

        public AbilityType AbilityType { get; }
        public Player User { get; private set; }
        
        /// <summary>
        /// Display icon on the HUD.
        /// </summary>
        public abstract Texture2D Icon { get; }
        
        //TODO - Adding talents later.

        public int CooldownTimer { get; set; }
        public bool IsActive { get; set; }

        public Ability(Player player, String name, int baseCooldown, int baseResourceCost, AbilityType abilityType) {
            Name = name;
            BaseCooldown = baseCooldown;
            AbilityType = abilityType;
            BaseResourceCost = baseResourceCost;
            User = player;
        }

        /*public static Ability CreateAbility(Player Player) { //, Type type) {
            
        }*/

        
        /// <summary>
        /// Will cast if able to, override if resources are not a factor.
        /// </summary>
        public virtual bool CastIfAble() {
            var mobaPlayer = User.GetModPlayer<MobaPlayer>();

            if (mobaPlayer.CurrentResource >= BaseResourceCost && CanCastAbility() && CooldownTimer == 0) {
                OnCast();
                AbilityEffectManager.OnCast(User, this);
                ReduceResource();
                Logging.PublicLogger.Debug(Name + " casted by " + User.name);
                return true;
            }

            return false;
        }

        public virtual void ReduceResource() {
            var mobaPlayer = User.GetModPlayer<MobaPlayer>();
            if (mobaPlayer.CurrentResource - BaseResourceCost < 0) {
                mobaPlayer.CurrentResource = 0;
            }
            else {
                mobaPlayer.CurrentResource -= BaseResourceCost;
            }
        }

        /// <summary>
        /// Action the frame the ability is cast.
        /// </summary>
        public virtual void OnCast() { }

        /// <summary>
        /// Override this if the ability does something over a duration.
        /// </summary>
        public virtual void WhileActive() { }
        
        /// <summary>
        /// Override this if cleanup/something needs to be done at the end of an ability's duration.
        /// </summary>
        public virtual void TimeOut() { }

        /// <summary>
        /// Override this if there are ability/Player based conditions.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanCastAbility() {
            return true;
        }

        /// <summary>
        /// Override this if there are any specific cooldown logic.
        /// </summary>
        /// <returns></returns>
        public virtual void TickCooldown() {
            if (CooldownTimer > 0) {
                CooldownTimer--;
            }
        }

        public virtual void ActivatePassives() {
            switch (AbilityType) {
                case AbilityType.Passive: {    
                    IsActive = true;
                    return;
                }
            }
        }
        
        public virtual void ConstructFlatAttributes() {
            PassiveFlatAttributes = new Dictionary<AttributeType, Func<float>>();
        }
        
        public virtual void ConstructMultAttributes() {
            PassiveMultAttributes = new Dictionary<AttributeType, Func<float>>();
        }
    }
}