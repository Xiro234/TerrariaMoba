using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;
using TerrariaMoba.Network;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects {
    public abstract class StatusEffect {
        public abstract string DisplayName { get; }
        public Player User { get; private set; }
        public abstract Texture2D Icon { get; }

        public int Duration { get; protected set; }
        public int DurationTimer { get; protected set; }
        public bool CanBeCleansed { get; protected set; }
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

        public virtual void GetListOfPlayerLayers(List<PlayerLayer> playerLayers) { }

        public PlayerLayer GetEffectBar() {
            PlayerLayer playerLayer = null;
            if (ShowBar) {
                playerLayer = new PlayerLayer("TerrariaMoba", "EffectBar", PlayerLayer.MiscEffectsFront,
                    delegate(PlayerDrawSet drawInfo) {
                    Player drawPlayer = drawInfo.drawPlayer;
                    Mod mod = ModLoader.GetMod("TerrariaMoba");
                    MobaPlayer mobaPlayer = drawPlayer.GetModPlayer<MobaPlayer>();
                    Texture2D texture = TextureAssets.MagicPixel.Value;
                    Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - 46, 
                        drawPlayer.Top.Y - Main.screenPosition.Y - 10);

                    float ratio = (float) DurationTimer / (float) Duration;
                    int length = (int)(92f * ratio);

                    DrawData data = new DrawData(texture, texturePos, new Rectangle(0, 0, length, 2), Color.White);
                    Main.playerDrawData.Add(data);
                });
            }
            
            return playerLayer;
        }

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