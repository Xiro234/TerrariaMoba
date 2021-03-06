using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Networking.Packets;
using WebmilioCommons.Networking.Serializing;

namespace TerrariaMoba.Effects {
    public abstract class Effect : INetworkSerializable {
        public string Name { get; protected set; }
        public Player User { get; private set; }
        public virtual Texture2D Icon {
            get { return TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        }

        public int DurationTimer { get; set; }

        public Effect(string name) {
            Name = name;
        }

        public void Receive(NetworkPacket networkPacket, BinaryReader reader) {
            
        }

        public void Send(NetworkPacket networkPacket, ModPacket modPacket) {
            
        }
    }
}