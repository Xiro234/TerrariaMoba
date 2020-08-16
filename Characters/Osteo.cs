using TerrariaMoba;
using TerrariaMoba.Buffs;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariaMoba.Players;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Osteo;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Characters {
    public class Osteo : Character {
        public Osteo(Player player) : base(player) {
            CharacterEnum = CharacterEnum.Osteo;
        }

        public override void ChooseCharacter() {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();
            Item vanityHelm = new Item();
            vanityHelm.SetDefaults(ItemID.SkeletronPrimeMask);
            Item vanityChest = new Item();
            vanityChest.SetDefaults(3875);
            Item vanityLeg = new Item();
            vanityLeg.SetDefaults(3876);
            Item primary = new Item();
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("SylviaBow"));

            player.armor[10] = vanityHelm;
            player.armor[11] = vanityChest;
            player.armor[12] = vanityLeg;
            player.inventory[0] = primary;
            player.Male = true;
            player.hair = 15;
            player.hairColor = new Color(52, 133, 34);
            player.skinColor = new Color(198,134,66);
            player.eyeColor = new Color(84,42,14);
            baseMaxHealth = 2000;
            baseLifeRegen = 2f;
            player.statLifeMax2 = baseMaxHealth;
            player.statLife = 2000;

            CharacterIcon = TerrariaMoba.Instance.GetTexture("Textures/Osteo/OsteoIcon");
            
            abilities[0] = new RaiseDead(player);
            abilities[1] = new LifedrainPulse(player);
            abilities[3] = new SkeletalBond(player);
            abilities[2] = new SongOfTheDamned(player);
            //TalentSelect();
        }

        public override void PostUpdateEquips() {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();

            foreach (Ability ability in abilities) {
                var raiseDead = ability as RaiseDead;
                if (raiseDead != null) {
                    if (mobaPlayer.lifeRegenTimer == 30) {
                        raiseDead.marrow += (int)mobaPlayer.lifeRegen;
                        if (raiseDead.marrow > raiseDead.marrowMax) {
                            raiseDead.marrow = raiseDead.marrowMax;
                        }
                    }

                    mobaPlayer.lifeRegen = 0f;
                }
            }
        }

        public override void HealMe(ref int amount) {
            foreach (Ability ability in abilities) {
                var raiseDead = ability as RaiseDead;
                if (raiseDead != null) {
                    raiseDead.marrow += amount / 10;
                    CombatText.NewText(player.Hitbox, Color.Gray, amount / 10, false);
                    
                    for (int i = 0; i < 10; i++) {
                        int dust = Dust.NewDust(player.position, player.width, player.height, 90, 0f, 0f, 0, Color.Gray,
                            1f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                    }

                    if (raiseDead.marrow > raiseDead.marrowMax) {
                        raiseDead.marrow = raiseDead.marrowMax;
                    }

                    amount = (int)(amount * (2f / 3f));
                }
            }
        }

        public override void ReadCharacter(BinaryReader reader) {
            base.ReadCharacter(reader);
        }

        public override void WriteCharacter(BinaryWriter writer) {
            base.WriteCharacter(writer);
        }
    }
}