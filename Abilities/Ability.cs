using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.UI;

namespace TerrariaMoba.Abilities {
    public abstract class Ability {
        public string Name { get; protected set; }
        public int BaseCooldown { get; }
        public int BaseResourceCost { get; }

        public AbilityType AbilityType { get; }
        public Player User { get; private set; }
        
        /// <summary>
        /// Display icon on the HUD.
        /// </summary>
        public abstract Texture2D Icon { get; }
        
        //TODO - Adding talents later.

        public int CooldownTimer { get; set; }
        public bool IsActive { get; set; }

        public Ability(String name, int baseCooldown, int baseResourceCost, AbilityType abilityType) {
            Name = name;
            BaseCooldown = baseCooldown;
            AbilityType = abilityType;
            BaseResourceCost = baseResourceCost;
            SetPlayer(Main.LocalPlayer);
        }

        public void SetPlayer(Player player) {
            User = player;
        }

        /*public static Ability CreateAbility(Player player) { //, Type type) {
            
        }*/

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
        /// Override this if there are ability/player based conditions.
        /// </summary>
        /// <returns></returns>
        public virtual bool CanCastAbility() {
            return true;
        }
        
        public virtual void AdditionalDrawing(SpriteBatch spriteBatch, UIAbilityIcon abilityIcon) { }
    }
}