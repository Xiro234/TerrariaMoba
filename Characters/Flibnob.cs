using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Flibnob;
using TerrariaMoba.Items.Flibnob;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Flibnob : Character {
        public Flibnob() { }

        public Flibnob(Player user) : base(user) { }
        
        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 1340f },
                { MAX_MANA, () => 500f },
                { ATTACK_DAMAGE, () => 75f },
                { ATTACK_SPEED, () => 1.5f },
                { ATTACK_VELOCITY, () => 9f }
            };
        }

        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new FlameBelch(),
                new TitaniumShell(),
                new Rockwrecker(),
                new Earthsplitter(),
                new BattleHardened()
            };
        }

        public override string Name {
            get => "Flibnob";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobIcon", AssetRequestMode.ImmediateLoad);
            
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 15; }
        public override Color HairColor { get => Color.Black; }
        public override Color SkinColor { get => Color.SaddleBrown; }
        public override Color EyeColor { get => Color.Red; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<FlibnobAxe>(); }
        public override int HeadVanityID { get => ItemID.BossMaskOgre; }
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

        public Flibnob(Player Player) : base(Player) { }
        
        public override void InitializeCharacter() { }
        
        public override void SetPlayer() {
            vanityHead.SetDefaults(3865);
            vanityBody.SetDefaults(667);
            dyeBody.SetDefaults(3555);
            vanityLeg.SetDefaults(668);
            dyeLeg.SetDefaults(3555);
            primary.SetDefaults(ModContent.ItemType<FlibnobAxe"));

            Player.Male = true;
            Player.hair = 15;
            Player.hairColor = new Color(0, 0, 0);
            Player.skinColor = new Color(120, 63, 4);
            Player.eyeColor = new Color(255, 0, 0);
        }

        public override void SetStats() {
            baseMaxLife = 2060;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = 500;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            baseArmor = 0;

            QAbility = new FlameBelch(Player);
            EAbility = new TitaniumShell(Player);
            RAbility = new Earthsplitter(Player);
            CAbility = new BattleHardened(Player);

            /*
            CullTheMeek ultimate = new CullTheMeek(Player);
            abilities[2] = ultimate;
            #1#
        }
*/