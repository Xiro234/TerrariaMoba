using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba {
	public class TerrariaMoba : Mod {
		public static ModHotKey AbilityOneHotKey;
		public static ModHotKey AbilityTwoHotKey;
		public static TerrariaMoba Instance { get; private set; }
		
		public const float nonKillXpRatio = 0.75f;

		public TerrariaMoba() {
			Instance = this;
		}
		
		public override void Load() {
			AbilityOneHotKey = RegisterHotKey("Ability One", "Q");
			AbilityTwoHotKey = RegisterHotKey("Ability Two", "F");
		}
		
		public override void Unload() {
			AbilityOneHotKey = null;
		}
		
		public override void HandlePacket(BinaryReader reader, int whoAmI) {
			Message msg = (Message) reader.ReadByte();
			switch (msg) {
				case(Message.SyncExperience):
					Packets.SyncExperiencePacket.Read(reader);
					break;
			}
		}
	}
}