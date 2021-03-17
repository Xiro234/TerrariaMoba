using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        public Sylvia(Player user) : base(user, new Statistics(1340f, 0f, 500f,
            0f, Statistics.Resource.Mana, 75f, 1.5f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Sylvia Verda";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaIcon");
        }

        public override int PrimaryWeaponID { get => TerrariaMoba.Instance.ItemType("SylviaBow"); }
        public override int HeadVanityID { get => ItemID.JungleRose; }
        public override int BodyVanityID { get => ItemID.DryadCoverings; }
        public override int LegVanityID { get => ItemID.DryadLoincloth; }
    }
}