﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Flibnob : Character {
        public Flibnob(Player user) : base(user, new Statistics(2060f, 0f, 500f,
            0f, Statistics.Resource.Mana, 75f, 1.5f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Flibnob";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Flibnob/FlibnobIcon");
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 15; }
        public override Color HairColor { get => Color.Black; }
        public override Color SkinColor { get => Color.SaddleBrown; }
        public override Color EyeColor { get => Color.Red; }
        public override int PrimaryWeaponID { get => TerrariaMoba.Instance.ItemType("FlibnobAxe"); }
        public override int HeadVanityID { get => ItemID.OgreMask; }
        public override int BodyVanityID { get => ItemID.RedsBreastplate; }
        public override int BodyDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int LegVanityID { get => ItemID.RedsLeggings; }
        public override int LegDyeID { get => ItemID.ReflectiveMetalDye; }
    }
}

/*
namespace TerrariaMoba.Characters {
    public class Flibnob : Character {
        public override string FullName { get => "Flibnob, the Chieftain of Krommock"; }

        public Flibnob(Player player) : base(player) { }
        
        public override void InitializeCharacter() { }
        
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
            baseMaxLife = 2060;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = 500;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            baseArmor = 0;

            QAbility = new FlameBelch(player);
            EAbility = new TitaniumShell(player);
            RAbility = new Earthsplitter(player);
            CAbility = new BattleHardened(player);

            /*
            CullTheMeek ultimate = new CullTheMeek(player);
            abilities[2] = ultimate;
            #1#
        }
*/