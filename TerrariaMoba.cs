using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariaMoba.Characters;
using TerrariaMoba.Network;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.UI;

namespace TerrariaMoba {
	public class TerrariaMoba : Mod {
		public static ModKeybind AbilityOneHotkey;
		public static ModKeybind AbilityTwoHotkey;
		public static ModKeybind AbilityThreeHotkey;
 		public static ModKeybind UltimateHotkey;
		public static ModKeybind TraitHotkey;
        public static ModKeybind SecondUltHotkey;
        public static ModKeybind OpenCharacterSelect;
		public static TerrariaMoba Instance { get; private set; }

		public const float nonKillXpRatio = 0.75f;

		public TerrariaMoba() {
			Instance = this;
		}

		public override void Load() {
			On.Terraria.Main.DamageVar += (orig, dmg, luck) => (int)Math.Round(dmg);
			
			AbilityOneHotkey = KeybindLoader.RegisterKeybind(Instance, "Basic Ability One", "Q");
			AbilityTwoHotkey = KeybindLoader.RegisterKeybind(Instance, "Basic Ability Two", "E");
			AbilityThreeHotkey = KeybindLoader.RegisterKeybind(Instance, "Basic Ability Three", "F");
			UltimateHotkey = KeybindLoader.RegisterKeybind(Instance, "Ultimate", "R");
			TraitHotkey = KeybindLoader.RegisterKeybind(Instance, "Trait", "C");
			SecondUltHotkey = KeybindLoader.RegisterKeybind(Instance, "Second Ultimate (Testing)", "X");
			OpenCharacterSelect = KeybindLoader.RegisterKeybind(Instance, "Open Character Select", "P");

			StatusEffectManager.Load();
			CharacterManager.Load();
		}
		
		public override void Unload() {
			AbilityOneHotkey = null;
			AbilityTwoHotkey = null;
			AbilityThreeHotkey = null;
			UltimateHotkey = null;
			TraitHotkey = null;
            SecondUltHotkey = null;
            OpenCharacterSelect = null;

			Instance = null;
		}

		public override void HandlePacket(BinaryReader reader, int whoAmI) {
			NetworkHandler.HandlePacket(reader, whoAmI);
		}
	}
}