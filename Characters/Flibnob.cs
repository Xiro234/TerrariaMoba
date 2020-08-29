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
        
        public override void InitializeCharacter() {
            CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobIcon");
        }
        
        public override void SetPlayer() {
            vanityHead.SetDefaults(3865);
            vanityBody.SetDefaults(667);
            dyeBody.SetDefaults(3555);
            vanityLeg.SetDefaults(668);
            dyeLeg.SetDefaults(3555);
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("FlibnobAxe"));

            player.Male = true;
            player.hair = 15;
            player.hairColor = new Color(0, 0, 0);
            player.skinColor = new Color(120, 63, 4);
            player.eyeColor = new Color(255, 0, 0);
        }

        public override void SetStats() {
            baseMaxHealth = 2060;

            QAbility = new FlameBelch(player);
            EAbility = new TitaniumShell(player);
            RAbility = new Earthsplitter(player);
            CAbility = new BattleHardened(player);

            /*
            CullTheMeek ultimate = new CullTheMeek(player);
            abilities[2] = ultimate;
            */
        }

        public override void PreUpdate() {
        }

        public override void PostUpdateEquips() {
            /*if (player.GetModPlayer<MobaPlayer>().FlibnobEffects.TitaniumShell) {
                player.GetModPlayer<MobaPlayer>().percentThorns += 0.25f;
            }*/
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
            /*if (player.GetModPlayer<MobaPlayer>().FlibnobEffects.TitaniumShell) {
                player.moveSpeed *= 0.5f;
                player.maxRunSpeed *= 0.5f;
                player.accRunSpeed *= 0.5f;
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