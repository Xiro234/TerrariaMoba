using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using TerrariaMoba.Characters;
using TerrariaMoba.Stats;

namespace TerrariaMoba.Players {
    partial class TerrariaMobaPlayer_Gameplay : ModPlayer {
        private bool AbilityOneUsed = false;
        private bool AbilityTwoUsed = false;
        private bool UltimateUsed = false;

        public TerrariaMobaStats stats;
        
        public override void Initialize() {
            this.stats = new TerrariaMobaStats();
        }
        
        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                stats.MyCharacter.AbilityOne();
                AbilityOneUsed = true;
            }
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                Main.NewText("Level: " + stats.MyCharacter.level + " XP: " + stats.experience);
            }
            if (TerrariaMoba.LevelTalentOneHotKey.JustPressed) {
                stats.MyCharacter.LevelTalentOne();
            }
            if (TerrariaMoba.LevelTalentTwoHotKey.JustPressed) {
                stats.MyCharacter.LevelTalentTwo();
            }
            if (TerrariaMoba.LevelTalentThreeHotKey.JustPressed) {
                stats.MyCharacter.LevelTalentThree();
            }
        }
        
        public int animCounter = -1;
        
        public override void PreUpdate() {

        }

        public override void PostUpdate() {
            if (AbilityOneUsed == true) {
                if (animCounter != 0) {
                    stats.MyCharacter.AbilityOneAnimation(ref animCounter);
                }
                else {
                    animCounter = -1;
                    AbilityOneUsed = false;
                }
            }
            
            if (AbilityTwoUsed == true) {
                
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            if (this.player.whoAmI != Main.myPlayer) {
                return;
            }

            if (Main.netMode == NetmodeID.MultiplayerClient && pvp) {
                TerrariaMobaUtils.MobaKill(damageSource.SourcePlayerIndex);
            }
        }
    }
}