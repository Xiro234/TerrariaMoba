using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;
using TerrariaMoba.Abilities.Marie;

namespace TerrariaMoba.Characters {
    public class Marie : Character {
        public Marie(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Marie;
        }

        public override void InitializeCharacter() {
            CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieIcon");
        }

        public override void SetPlayer() {
            vanityHead.SetDefaults(3226);
            dyeHead.SetDefaults(1014);
            vanityLeg.SetDefaults(2500);
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("MarieStaff"));

            player.Male = false;
            player.hair = 5;
            player.hairColor = new Color(0, 133, 255);
            player.skinColor = new Color(235, 159, 125);
            player.eyeColor = new Color(0, 0, 255);
        }

        public override void SetStats() {
            baseMaxLife = 1460;

            QAbility = new WhirlpoolInABottle(player);
            EAbility = new TomeOfLacusia(player);
            RAbility = new EyeOfTheStorm(player);
            CAbility = new Floodboost(player);
            
            /*
            FountainOfTheGoddess ultimate = new FountainOfTheGoddess(player);
            abilities[2] = ultimate;
            */
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
            /*if (player.GetModPlayer<MobaPlayer>().MarieEffects.Floodboost) {
                player.moveSpeed *= 1.33f;
                player.maxRunSpeed *= 1.33f;
                player.accRunSpeed *= 1.33f;
            }*/
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