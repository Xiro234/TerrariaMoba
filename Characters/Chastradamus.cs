using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Chastradamus : Character {
        public Chastradamus() { }

        public Chastradamus(Player user) : base(user, new Statistics(2000f, 0f, 500f,
            0f, Resource.Mana, 75f, 1.5f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Chastradamus";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Chastradamus/ChastradamusIcon");
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