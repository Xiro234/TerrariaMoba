using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Nocturne : Character {
        public Nocturne(Player user) : base(user, new Statistics(1825f, 0f, 500f,
            0f, Statistics.Resource.Mana, 103f, 1.11f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Nocturne Umbra";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Nocturne/NocturneIcon");
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 21; }
        public override Color HairColor { get => Color.Brown; }
        public override Color SkinColor { get => Color.PeachPuff; }
        public override Color EyeColor { get => Color.Firebrick; }
        public override int PrimaryWeaponID { get => ItemID.TitaniumSword; }
        public override int HeadVanityID { get => ItemID.GladiatorHelmet; }
        public override int HeadDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int BodyVanityID { get => ItemID.RedsBreastplate; }
        public override int BodyDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int LegVanityID { get => ItemID.RedsLeggings; }
        public override int LegDyeID { get => ItemID.ReflectiveMetalDye; }
    }
}