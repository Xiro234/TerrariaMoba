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

namespace TerrariaMoba {
    public class TerrariaMobaPlayer : ModPlayer {
        public int animCounter = -1;
        private bool AbilityOneUsed = false;
        private bool AbilityTwoUsed = false;
        private bool UltimateUsed = false;
        private int xpPerKill = 100;
        public TerrariaMobaStats stats;

        public override void Initialize() {
            this.stats = new TerrariaMobaStats();
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                stats.MyCharacter.AbilityOne(player);
                AbilityOneUsed = true;
            }
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                Main.NewText("Level: " + stats.MyCharacter.level + " XP: " + stats.experience);
            }
        }

        public override void PreUpdate() {

        }

        public override void PostUpdate() {
            if (AbilityOneUsed == true) {
                if (animCounter != 0) {
                    stats.MyCharacter.AbilityOneAnimation(player, ref animCounter);
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
                MobaKill(damageSource.SourcePlayerIndex);
                
            }
        }
        
        public void MobaKill(int killWhoAmI) {
            //Credit to https://github.com/hamstar0/tml-playerstatistics-mod for modifications on his work
            if (killWhoAmI >= 0 && killWhoAmI < Main.player.Length) {
                var otherPlayer = Main.player[killWhoAmI].GetModPlayer<TerrariaMobaPlayer>();

                if (otherPlayer != null) {
                    for (int i = 0; i < Main.maxPlayers; i++) {
                        Player plr = Main.player[i];
                        
                        if (plr.active && plr.team == Main.player[killWhoAmI].team) {
                            Packets.SyncExperiencePacket.Write(xpPerKill, i, killWhoAmI);
                        }
                    }
                }
                else {
                    Main.NewText("Invalid ModPlayer for " + Main.player[killWhoAmI].name, Color.Red);
                }
            }
            else {
                Main.NewText("Invalid player whoAmI: " + killWhoAmI, Color.Red);
            }
        }
    }
}