using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Characters;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public void StartGame() {
            AssignCharacter();
            Hero.StartGame();
            if (Main.LocalPlayer == Player) {
                TerrariaMoba.Instance.ShowBar();
                TerrariaMoba.Instance.MobaBar.SetIcons();
                Main.NewText(Player.GetModPlayer<MobaPlayer>().Hero.Name);
            }
        }
        
        public bool AssignCharacter() {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();
            mobaPlayer.Hero = (Character)Activator.CreateInstance(mobaPlayer.selectedCharacter, Player);

            return true;
        }
    }
}