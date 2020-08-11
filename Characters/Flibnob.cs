using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Flibnob;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Characters {
    public class Flibnob : Character {

        public Flibnob(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Flibnob;
        }
        
        public override void ChooseCharacter() {
            Main.NewText("Flibnob");
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            Item vanityHelm = new Item();
            vanityHelm.SetDefaults(3865);
            Item vanityChest = new Item();
            vanityChest.SetDefaults(667);
            Item dyeChest = new Item();
            dyeChest.SetDefaults(3555);
            Item vanityLeg = new Item();
            vanityLeg.SetDefaults(668);
            Item dyeLeg = new Item();
            dyeLeg.SetDefaults(3555);
            Item primary = new Item();
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("FlibnobAxe"));

            player.armor[10] = vanityHelm;
            player.armor[11] = vanityChest;
            player.armor[12] = vanityLeg;
            player.dye[1] = dyeChest;
            player.dye[2] = dyeLeg;
            player.inventory[0] = primary;
            player.Male = true;
            player.hair = 15;
            player.hairColor = new Color(0, 0, 0);
            player.skinColor = new Color(120, 63, 4);
            player.eyeColor = new Color(255, 0, 0);
            baseMaxHealth = 2400;
            player.statLifeMax2 = baseMaxHealth;
            player.statLife = 2400;
            
            FlameBelch abilityOne = new FlameBelch(player);
            abilities[0] = abilityOne;
            
            TitaniumShell abilityTwo = new TitaniumShell(player);
            abilities[1] = abilityTwo;
            
            Earthsplitter ultimate = new Earthsplitter(player);
            abilities[2] = ultimate;
            
            BattleHardened trait = new BattleHardened(player);
            abilities[3] = trait;
            
            /*
            CullTheMeek ultimate = new CullTheMeek(player);
            abilities[2] = ultimate;
            */
            
            
            CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobIcon");
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
            if (player.GetModPlayer<MobaPlayer>().FlibnobEffects.TitaniumShell) {
                player.moveSpeed *= 0.5f;
                player.maxRunSpeed *= 0.5f;
                player.accRunSpeed *= 0.5f;
            }
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