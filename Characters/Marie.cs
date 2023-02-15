using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Marie;
using TerrariaMoba.Items.Marie;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Marie : Character {
        public Marie() { }
        
        public Marie(Player user) : base(user) { }

        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 1460f },
                { HEALTH_REGEN, () => 3.0f },
                { MAX_MANA, () => 500f },
                { MANA_REGEN, () => 2.1f },
                { PHYSICAL_ARMOR, () => 0f },
                { MAGICAL_ARMOR, () => 0f },
                { ATTACK_DAMAGE, () => 69f },
                { ATTACK_SPEED, () => 0.10f },
                { ATTACK_VELOCITY, () => 7.33f },
                { MOVEMENT_SPEED, () => 1f },
                { JUMP_SPEED, () => 1f },
                { HEALING_EFFECTIVENESS, () => 1f },
                { STATUS_RESISTANCE, () => 0f }
            };
        }
        
        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new Tidecaller(User),
                new FlowOfLife(User),
                new EyeOfTheStorm(User),
                new Surge(User),
                new BlessingOfTheGoddess(User),
                new Confluence(User)
            };
        }
        
        public override string Name {
            get => "Marie Tidewrath";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieIcon", AssetRequestMode.ImmediateLoad);
        }
        
        public override bool IsMale { get => false; }
        public override int HairID { get => 5; }
        public override Color HairColor { get => Color.DodgerBlue; }
        public override Color SkinColor { get => Color.LightSalmon; }
        public override Color EyeColor { get => Color.Blue; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<MarieStaff>(); }
        public override int HeadVanityID { get => ItemID.BejeweledValkyrieHead; }
        public override int HeadDyeID { get => ItemID.SkyBlueDye; }
        public override int BodyVanityID { get => ItemID.FishCostumeShirt; }
        public override int LegVanityID { get => ItemID.FishCostumeFinskirt; }
    }
}

/*
namespace TerrariaMoba.Characters {
    public class Marie : Character {
        public override string FullName {
            get => "Marie Tidewrath, High Priestess of Lacusia";
        }
        public override void SetPlayer() {
            vanityHead.SetDefaults(3226);
            dyeHead.SetDefaults(1014);
            vanityLeg.SetDefaults(2500);
            primary.SetDefaults(ModContent.ItemType<MarieStaff"));

            Player.Male = false;
            Player.hair = 5;
            Player.hairColor = new Color(0, 133, 255);
            Player.skinColor = new Color(235, 159, 125);
            Player.eyeColor = new Color(0, 0, 255);
            baseMaxLife = 1460;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = 500;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            baseArmor = 0;
            
            QAbility = new WhirlpoolInABottle(Player);
            EAbility = new TomeOfLacusia(Player);
            RAbility = new EyeOfTheStorm(Player);
            CAbility = new Floodboost(Player);
            
            /*
            FountainOfTheGoddess ultimate = new FountainOfTheGoddess(Player);
            abilities[2] = ultimate;
            #1#
        }
*/