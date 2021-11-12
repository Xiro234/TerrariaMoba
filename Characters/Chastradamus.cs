using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Chastradamus;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Chastradamus : Character {
        public Chastradamus() { }

        public Chastradamus(Player user) : base(user) { }
        
        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 1675f },
                { HEALTH_REGEN, () => 3.5f },
                { MAX_MANA, () => 500f },
                { MANA_REGEN, () => 2.1f },
                { PHYSICAL_ARMOR, () => 0f },
                { MAGICAL_ARMOR, () => 0f },
                { ATTACK_DAMAGE, () => 1f },
                { ATTACK_SPEED, () => 1.23f },
                { ATTACK_VELOCITY, () => 7.33f },
                { MOVEMENT_SPEED, () => 1f },
                { JUMP_SPEED, () => 1f }
            };
        }
        
        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new BrewConcoction(User),
                new Incision(User),
                new FlaskOfVitality(User),
                new Crowstorm(User),
                new Bloodletting(User)
            };
        }

        public override string Name {
            get => "Chastradamus";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Chastradamus/ChastradamusIcon", AssetRequestMode.ImmediateLoad);
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 3; }
        public override Color HairColor { get => Color.Black; }
        public override Color SkinColor { get => Color.PeachPuff; }
        public override Color EyeColor { get => Color.Red; }
        public override int PrimaryWeaponID { get => ItemID.ToxicFlask; }
        public override int HeadVanityID { get => ItemID.CrownosMask; }
        public override int HeadDyeID { get => ItemID.SilverDye; }
        public override int BodyVanityID { get => ItemID.TaxCollectorSuit; }
        public override int BodyDyeID { get => ItemID.BrightSilverDye; }
        public override int LegVanityID { get => ItemID.TaxCollectorPants; }
        public override int LegDyeID { get => ItemID.BrightSilverDye; }
        
        //TODO - Add an accessory slot for Titan Glove w/ Shadow Dye
    }
}