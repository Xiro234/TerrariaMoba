using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.UI;

namespace TerrariaMoba {
	public class TerrariaMoba : Mod {
		public static ModHotKey AbilityOneHotKey;
		public static ModHotKey AbilityTwoHotKey;
		public static ModHotKey LevelTalentOneHotKey;
		public static ModHotKey LevelTalentTwoHotKey;
		public static ModHotKey LevelTalentThreeHotKey;
		public static ModHotKey UltimateHotkey;
		public static ModHotKey BecomeSylvia;
		public static ModHotKey OpenCharacterSelect;
		public static TerrariaMoba Instance { get; private set; }

		internal MobaBar MobaBar;
		internal CharacterSelect CharacterSelect;
		internal UserInterface BarInterface;
		internal UserInterface SelectInterface;

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
			OpenCharacterSelect = RegisterHotKey("Open Character Select", "P");

			if (!Main.dedServ) {
				MobaBar = new MobaBar();
				BarInterface = new UserInterface();
				BarInterface.SetState(null);
				
				CharacterSelect = new CharacterSelect();
				SelectInterface = new UserInterface();
				SelectInterface.SetState(null);
			}
		}

		public override void UpdateUI(GameTime gameTime) {
			BarInterface?.Update(gameTime);
			SelectInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int LayerIndex;
			LayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			layers.RemoveAt(LayerIndex);
			LayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Death Text"));
			layers.RemoveAt(LayerIndex);
			LayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Entity Health Bars"));
			layers.RemoveAt(LayerIndex);
			
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1) {
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"TerrariaMoba: A Description",
					delegate {
						BarInterface.Draw(Main.spriteBatch, new GameTime());
						SelectInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}

		public override void Unload() {
			AbilityOneHotKey = null;
			AbilityTwoHotKey = null;
			UltimateHotkey = null;
			LevelTalentOneHotKey = null;
			LevelTalentTwoHotKey = null;
			LevelTalentThreeHotKey = null;
			BecomeSylvia = null;

			MobaBar.UnLoad();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI) {
			Message msg = (Message) reader.ReadByte();
			switch (msg) {
				case(Message.SyncExperience):
					Packets.ExperiencePacket.Read(reader);
					break;
				case(Message.SyncPvpHit):
					Packets.PvpHitPacket.Read(reader);
					break;
				case(Message.SyncTalents):
					Packets.TalentsPacket.Read(reader);
					break;
				case(Message.SyncAbilities):
					Packets.AbilityCastPacket.Read(reader);
					break;
				case(Message.SyncGameStart):
					Packets.GameStartPacket.Read(reader);
					break;
				case(Message.SyncAbilityValues):
					Packets.ReadWriteAbilityPacket.Read(reader);
					break;
				case(Message.SyncJunglesWrathAdd):
					Packets.JunglesWrathAddPacket.Read(reader);
					break;
			}
		}

		internal void ShowBar() {
			BarInterface?.SetState(MobaBar);
		}

		internal void HideBar() {
			BarInterface?.SetState(null);
		}

		internal void ShowSelect() {
			SelectInterface?.SetState(CharacterSelect);
		}

		internal void HideSelect() {
			SelectInterface?.SetState(null);
		}
	}
}