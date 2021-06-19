using Terraria.ModLoader;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public void StartGame() {
            TerrariaMobaUtils.AssignCharacter(player);
            TerrariaMoba.Instance.ShowBar();
            TerrariaMoba.Instance.MobaBar.SetIcons();
        }
    }
}