using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.UI;
using WebmilioCommons.Networking.Packets;
using WebmilioCommons.Networking.Serializing;

namespace TerrariaMoba.Abilities {
    public abstract class Ability : INetworkSerializable {
        public string Name { get; protected set; }
        public int BaseCooldown { get; }
        public int BaseResourceCost { get; }
        public AbilityType AbilityType { get; }
        public Player User { get; private set; }
        public abstract Texture2D Icon { get; }
        
        //Adding talents later.

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
            return (CooldownTimer == 0) && (User.GetModPlayer<MobaPlayer>().currentResource > BaseResourceCost);
        }
        
        public void Receive(NetworkPacket networkPacket, BinaryReader reader) {
            /*
            Name = reader.ReadString();
            cooldownTimer = reader.ReadInt32();
            Timer = reader.ReadInt32();
            IsActive = reader.ReadBoolean();
            ResourceCost = reader.ReadInt32();
            AbilityType = (AbilityType)reader.ReadByte();
            playerIndex = reader.ReadInt32();
            */
        }

        public void Send(NetworkPacket networkPacket, ModPacket modPacket) {
            /*
            modPacket.Write(Name);
            modPacket.Write(cooldownTimer);
            modPacket.Write(Timer);
            modPacket.Write(IsActive);
            modPacket.Write(ResourceCost);
            modPacket.Write((byte)AbilityType);
            modPacket.Write(playerIndex);
            */
        }

        public virtual void AdditionalDrawing(SpriteBatch spriteBatch, UIAbilityIcon abilityIcon) { }
    }
}