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
                MobaSystem.ShowBar();
                MobaSystem.MobaBar.SetIcons();
                Main.NewText(Player.GetModPlayer<MobaPlayer>().Hero.Name);
            }
            
            SetPlayerHealth();
            SetPlayerResource();
            Player.statLife = Player.statLifeMax2;
        }
        
        public bool AssignCharacter() {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();
            mobaPlayer.Hero = (Character)Activator.CreateInstance(mobaPlayer.selectedCharacter, Player);
            if (mobaPlayer.Hero != null) {
                mobaPlayer.Hero.InitializePlayer();
            }
            return true;
        }
    }
}