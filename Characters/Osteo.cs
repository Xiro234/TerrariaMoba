using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Osteo;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Characters {
    public class Osteo : Character {
        public bool EarthsplitterJump = false;
        
        public Osteo(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Osteo;
        }
        
        public override void ChooseCharacter() {
            Main.NewText("Osteo");
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            Item vanityHelm = new Item();
            vanityHelm.SetDefaults(2107);
            Item vanityChest = new Item();
            vanityChest.SetDefaults(3875);
            Item vanityLeg = new Item();
            vanityLeg.SetDefaults(3876);
            Item primary = new Item();
            primary.SetDefaults(1313);

            player.armor[10] = vanityHelm;
            player.armor[11] = vanityChest;
            player.armor[12] = vanityLeg;
            player.inventory[0] = primary;
            player.Male = true;
            player.hair = 15;
            player.hairColor = new Color(0, 0, 0);
            player.skinColor = new Color(105, 107, 111);
            player.eyeColor = new Color(255, 0, 0);
            baseMaxHealth = 1600;
            player.statLifeMax2 = baseMaxHealth;
            player.statLife = 1600;
            
            RaiseDead abilityOne = new RaiseDead(player);
            abilities[0] = abilityOne;
            
            LifedrainAura abilityTwo = new LifedrainAura(player);
            abilities[1] = abilityTwo;
            
            SongOfTheDamned ultimate = new SongOfTheDamned(player);
            abilities[2] = ultimate;
            
            SkeletalRemains trait = new SkeletalRemains(player);
            abilities[3] = trait;
            
            /*
            Osteo2 ultimate = new Osteo2(player);
            abilities[2] = ultimate;
            */
            
            CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Osteo/OsteoIcon");
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