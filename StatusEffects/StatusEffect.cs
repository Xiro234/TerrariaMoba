using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects {
    public abstract class StatusEffect {
        public abstract string DisplayName { get; }
        public Player User { get; private set; }
        public abstract Texture2D Icon { get; }

        public int Duration { get; protected set; }
        public int DurationTimer { get; protected set; }
        public bool CanBeCleansed { get; protected set; }
        public virtual Dictionary<AttributeType, float> FlatAttributes { get; }
        public virtual Dictionary<AttributeType, float> MultAttributes { get; }
        protected virtual bool ShowBar {
            get => true;
        }

        public StatusEffect() { }

        public StatusEffect(int duration, bool canBeCleansed) {
            Duration = duration;
            CanBeCleansed = canBeCleansed;
        }

        public virtual void Apply() {
            DurationTimer = Duration;
        }

        public virtual void WhileActive() {
            DurationTimer--;
        }

        public virtual void FallOff() { }

        public void SetPlayer(Player Player) {
            User = Player;
        }

        public virtual void GetListOfPlayerDrawLayers(List<PlayerDrawLayer> playerLayers) { }

        public virtual void RefreshDuration() {
            DurationTimer = Duration;
        }
        
        /// <summary>
        /// Override this if the status effect contains more information to be synced. Should return base.SendEffectElements(packet).
        /// </summary>
        /// <param name="packet">The packet to be added onto.</param>
        public virtual void SendEffectElements(ModPacket packet) {
            packet.Write(Duration);
            packet.Write(CanBeCleansed);
        }
        
        /// <summary>
        /// Override this if the status effect contains more information to be synced. Should return base.ReceiveEffectElements(reader).
        /// </summary>
        /// <param name="reader">Reader that contains the additional information for the status effect.</param>
        public virtual void ReceiveEffectElements(BinaryReader reader) {
            Duration = reader.ReadInt32();
            CanBeCleansed = reader.ReadBoolean();
        }
    }
}