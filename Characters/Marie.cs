using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Characters {
    public class Marie : Character {
        public Marie(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Marie;
        }

        public override void ChooseCharacter() {
            Main.NewText("Marie");
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            Item vanityHelm = new Item();
            vanityHelm.SetDefaults(3226);
            Item dyeHelm = new Item();
            dyeHelm.SetDefaults(1014);
            Item vanityChest = new Item();
            vanityChest.SetDefaults(2499);
            Item vanityLeg = new Item();
            vanityLeg.SetDefaults(2500);
            Item primary = new Item();
            primary.SetDefaults(741);

            player.armor[10] = vanityHelm;
            player.armor[11] = vanityChest;
            player.armor[12] = vanityLeg;
            player.dye[0] = dyeHelm;
            player.inventory[0] = primary;
            player.Male = false;
            player.hair = 5;
            player.hairColor = new Color(0, 133, 255);
            player.skinColor = new Color(235, 159, 125);
            player.eyeColor = new Color(0, 0, 255);
            mobaPlayer.customStats.maxHealth = 1600;
            player.statLifeMax2 = 1600;
            player.statLife = 1600;
            
            WarpingMaelstrom abilityOne = new WarpingMaelstrom(player);
            abilities[0] = abilityOne;
            
            TomeOfLacusia abilityTwo = new TomeOfLacusia(player);
            abilities[1] = abilityTwo;
                
            FountainOfTheGoddess ultimate = new FountainOfTheGoddess(player);
            abilities[2] = ultimate;
            
            CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieIcon");
        }
        
        public override void PreUpdate() {
            
        }

        public override void PostUpdateBuffs() {

        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack) {
            return true;
        }

        public override float UseTimeMultiplier(Item item) {
            return 1f;
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers) {

        }
        
        public override void PostUpdateRunSpeeds() {

        }
        
        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {

        }

        public override void LevelUp() {
            level += 1;
            TalentSelect();
        }

        public override void TalentSelect() {
            switch (level) {
                case 1:
                    Main.NewText("We");
                    Main.NewText("Like");
                    Main.NewText("Fortnite");
                    canSelectTalent = true;
                    break;
                case 4:
                    Main.NewText("We");
                    Main.NewText("Like");
                    Main.NewText("Fortnite");
                    canSelectTalent = true;
                    break;
                case 7:
                    break;
                case 10:
                    break;
                case 13:
                    break;
                case 16:
                    break;
                case 20:
                    break;
            }
        }
    }
}