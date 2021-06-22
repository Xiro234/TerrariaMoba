using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariaMoba.Abilities;
using TerrariaMoba.Characters;
using TerrariaMoba.Network;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.UI;

namespace TerrariaMoba {
	public class TerrariaMoba : Mod {
		public static ModHotKey AbilityOneHotkey;
		public static ModHotKey AbilityTwoHotkey;
		public static ModHotKey AbilityThreeHotkey;
 		public static ModHotKey UltimateHotkey;
		public static ModHotKey TraitHotkey;
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
			On.Terraria.Main.DamageVar += (orig, dmg) => (int)Math.Round(dmg);
			
			AbilityOneHotkey = RegisterHotKey("Basic Ability One", "Q");
			AbilityTwoHotkey = RegisterHotKey("Basic Ability Two", "E");
			AbilityThreeHotkey = RegisterHotKey("Basic Ability Three", "F");
			UltimateHotkey = RegisterHotKey("Ultimate", "R");
			TraitHotkey = RegisterHotKey("Trait", "C");
			OpenCharacterSelect = RegisterHotKey("Open Character Select", "P");

			if (!Main.dedServ) {
				MobaBar = new MobaBar();
				BarInterface = new UserInterface();
				BarInterface.SetState(null);
				
				CharacterSelect = new CharacterSelect();
				SelectInterface = new UserInterface();
				SelectInterface.SetState(null);
			}
			
			StatusEffectManager.Load();
			CharacterManager.Load();
		}

		public override void UpdateUI(GameTime gameTime) {
			BarInterface?.Update(gameTime);
			SelectInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int LayerIndex;
			//LayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			//layers.RemoveAt(LayerIndex);
			LayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Death Text"));
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
			AbilityOneHotkey = null;
			AbilityTwoHotkey = null;
			AbilityThreeHotkey = null;
			UltimateHotkey = null;
			TraitHotkey = null;

			Instance = null;
			MobaBar.UnLoad();
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI) {
			NetworkHandler.HandlePacket(reader, whoAmI);
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