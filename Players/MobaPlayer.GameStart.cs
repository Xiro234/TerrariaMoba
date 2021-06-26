using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Characters;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public void StartGame() {
            AssignCharacter();
            Hero.StartGame();
            if (Main.LocalPlayer == player) {
                TerrariaMoba.Instance.ShowBar();
                TerrariaMoba.Instance.MobaBar.SetIcons();
                Main.NewText(player.GetModPlayer<MobaPlayer>().Hero.Name);
            }
        }
        
        public bool AssignCharacter() {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            mobaPlayer.Hero = (Character)Activator.CreateInstance(mobaPlayer.selectedCharacter, player);
            mobaPlayer.Hero.InitializePlayer();
            return true;
        }
    }
}