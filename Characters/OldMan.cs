using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class OldMan : Character {
        public OldMan(Player user) : base(user, new Statistics(2000f, 0f, 500f,
            0f, Statistics.Resource.Mana, 75f, 1.5f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Balner Gaulish";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/OldMan/OldManIcon");
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 3; }
        public override Color HairColor { get => Color.Gray; }
        public override Color SkinColor { get => Color.PeachPuff; }
        public override Color EyeColor { get => Color.RoyalBlue; }
        public override int PrimaryWeaponID { get => ItemID.ReinforcedFishingPole; }
        public override int BodyVanityID { get => ItemID.RainCoat; }
        public override int BodyDyeID { get => ItemID.ReflectiveDye; }
        public override int LegVanityID { get => ItemID.AnglerPants; }
        public override int LegDyeID { get => ItemID.ReflectiveDye; }
    }
}