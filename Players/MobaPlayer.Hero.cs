using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Characters;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Players {
    public partial class MobaPlayer : ModPlayer {
        public Statistics Stats { get; set; }
        public float CurrentResource { get; set; }
        public Character Hero { get; set; }
        public Type selectedCharacter;


        public int lifeRegenTimer = 0;
        public int resourceRegenTimer = 0;

        /*
        public void InitCharacter() {
            Item primary = new Item();
            primary.SetDefaults(Hero.PrimaryWeaponID);
            Item vanityHead = new Item();
            vanityHead.SetDefaults(Hero.HeadVanityID);
            Item vanityBody = new Item();
            vanityBody.SetDefaults(Hero.BodyVanityID);
            Item vanityLegs = new Item();
            vanityLegs.SetDefaults(Hero.LegVanityID);
            Item dyeHead = new Item();
            dyeHead.SetDefaults(Hero.HeadDyeID);
            Item dyeBody = new Item();
            dyeBody.SetDefaults(Hero.BodyDyeID);
            Item dyeLegs = new Item();
            dyeLegs.SetDefaults(Hero.LegDyeID);
            
            player.inventory[0] = primary;
            player.armor[10] = vanityHead;
            player.armor[11] = vanityBody;
            player.armor[12] = vanityLegs;
            player.dye[0] = dyeHead;
            player.dye[1] = dyeBody;
            player.dye[2] = dyeLegs;

            player.Male = Hero.IsMale;
            player.hair = Hero.HairID;
            player.hairColor = Hero.HairColor;
            player.eyeColor = Hero.EyeColor;
            player.skinColor = Hero.SkinColor;
        }
        */

        public void RegenLife() {
            /*
            lifeRegenTimer++;
            
            if (lifeRegenTimer == 60) {
                float healthRegenFromMax = (player.statLifeMax2 * 0.125f / 60f);
                player.statLife += (int)Math.Ceiling(healthRegenFromMax + Hero.BaseStatistics.HealthRegen + Stats.HealthRegen);

                if (player.statLife > player.statLifeMax2) {
                    player.statLife = player.statLifeMax2;
                }

                lifeRegenTimer = 0;
            }
            */
        }

        public void RegenResource() {
            /*
            resourceRegenTimer++;
            
            if (resourceRegenTimer == 60) {
                Hero.RegenResource();

                if (CurrentResource > Hero.BaseStatistics.MaxResource + Stats.MaxResource) {
                    CurrentResource = Hero.BaseStatistics.MaxResource + Stats.MaxResource;
                }

                resourceRegenTimer = 0;
            }
            */
        }
    }
}