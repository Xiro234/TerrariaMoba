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
        
        public override Dictionary<AttributeType, float> BaseAttributes => new Dictionary<AttributeType, float>() {
            { MAX_HEALTH, 1340f },
            { MAX_MANA, 500f },
            { ATTACK_DAMAGE, 75f },
            { ATTACK_SPEED, 1.5f },
            { ATTACK_VELOCITY, 9f }
        };

        public override Ability[] Skills => new Ability[] {
            new BrewConcoction(),
            new Incision(),
            new FlaskOfVitality(),
            new Crowstorm(),
            new Bloodletting()
        };

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