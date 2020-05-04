using Terraria.ModLoader;

namespace TerrariaMoba {
	public class TerrariaMoba : Mod {
		public static ModHotKey AbilityOneHotKey;
		public static ModHotKey AbilityTwoHotKey;

		public int redTeamKills = 0;
		public int blueTeamKills = 0;

		public override void Load() {
			AbilityOneHotKey = RegisterHotKey("Ability One", "Q");
			AbilityTwoHotKey = RegisterHotKey("Ability Two", "F");
		}

		public override void Unload() {
			AbilityOneHotKey = null;
		}

	}
}