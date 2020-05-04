using TerrariaMoba;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria;
using Terraria.GameInput;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using TerrariaMoba.Characters;
using TerrariaMoba.Stats;

namespace TerrariaMoba {
    public class TerrariaMobaPlayer : ModPlayer {
        public int animCounter = -1;
        private bool AbilityOneUsed = false;
        private bool AbilityTwoUsed = false;
        private bool UltimateUsed = false;
        private int xpPerKill = 100;
        private TerrariaMobaStats stats;

        public override void Initialize() {
            this.stats = new TerrariaMobaStats();
        }

        public override void ProcessTriggers(TriggersSet triggersSet) {
            if (TerrariaMoba.AbilityOneHotKey.JustPressed) {
                stats.MyCharacter.AbilityOne(player);
                AbilityOneUsed = true;
            }
            if (TerrariaMoba.AbilityTwoHotKey.JustPressed) {
                stats.MyCharacter.AbilityTwo(player);
                AbilityTwoUsed = true;
            }
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

            if (Main.netMode == 1 && pvp) {
                MobaKill(damageSource.SourcePlayerIndex);
            }
        }
        
        public void MobaKill(int killWhoAmI) {
            if (killWhoAmI >= 0 && killWhoAmI < Main.player.Length) {
                var otherPlayer = Main.player[killWhoAmI].GetModPlayer<TerrariaMobaPlayer>();

                if (otherPlayer != null) {
                    for (int i = 0; i < Main.maxPlayers; i++) {
                        Player plr = Main.player[i];
                        
                        if (plr.active && plr.team == Main.player[killWhoAmI].team) {
                            plr.GetModPlayer<TerrariaMobaPlayer>().stats.GainExperience(xpPerKill, plr.team);
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