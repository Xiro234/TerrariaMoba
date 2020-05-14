using System.IO;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba {
	public class TerrariaMoba : Mod {
		public static ModHotKey AbilityOneHotKey;
		public static ModHotKey AbilityTwoHotKey;
		public static ModHotKey LevelTalentOneHotKey;
		public static ModHotKey LevelTalentTwoHotKey;
		public static ModHotKey LevelTalentThreeHotKey;
		public static ModHotKey UltimateHotkey;
		public static ModHotKey BecomeSylvia;
		public static TerrariaMoba Instance { get; private set; }

		public const float nonKillXpRatio = 0.75f;

		public TerrariaMoba() {
			Instance = this;
		}
		
		public override void Load() {
			AbilityOneHotKey = RegisterHotKey("Ability One", "Q");
			AbilityTwoHotKey = RegisterHotKey("Ability Two", "F");
			UltimateHotkey = RegisterHotKey("Ultimate", "R");
			LevelTalentOneHotKey = RegisterHotKey("Level Talent One", "Z");
			LevelTalentTwoHotKey = RegisterHotKey("Level Talent Two", "X");
			LevelTalentThreeHotKey = RegisterHotKey("Level Talent Three", "C");
			BecomeSylvia = RegisterHotKey("Become Sylvia", "V");
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
				case(Message.SyncJunglesWrath):
					Packets.SyncJunglesWrathPacket.Read(reader);
					break;
				case(Message.SyncPvpHit):
					Packets.SyncPvpHitPacket.Read(reader);
					break;
				case(Message.SyncSylviaUlt1):
					Packets.SyncSylviaUlt1Packet.Read(reader);
					break;
				case(Message.SyncCharacter):
					Packets.SyncCharacterPacket.Read(reader);
					break;
				case(Message.SyncWeakened):
					Packets.SyncWeakenedPacket.Read(reader);
					break;
			}
		}
	}
}