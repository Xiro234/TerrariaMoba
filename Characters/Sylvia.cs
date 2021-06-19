using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        public Sylvia() { }

        public Sylvia(Player user) : base(user, new Statistics(1340f, 0f, 500f,
            0f, Resource.Mana, 75f, 1.5f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Sylvia Verda";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon");
        }

        public override bool IsMale { get => false; }
        public override int HairID { get => 55; }
        public override Color HairColor { get => Color.ForestGreen; }
        public override Color SkinColor { get => Color.Peru; }
        public override Color EyeColor { get => Color.Sienna; }
        public override int PrimaryWeaponID { get => TerrariaMoba.Instance.ItemType("SylviaBow"); }
        public override int HeadVanityID { get => ItemID.JungleRose; }
        public override int BodyVanityID { get => ItemID.DryadCoverings; }
        public override int LegVanityID { get => ItemID.DryadLoincloth; }
    }
}