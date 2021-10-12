using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Nocturne;
using TerrariaMoba.Items.Nocturne;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Nocturne : Character {
        public Nocturne() { }
        
        public Nocturne(Player user) : base(user, new Statistics(1825f, 0f, 500f,
            0f, Resource.Mana, 103f, 1.11f, 9f), 
            new UmbralBlade(), new TitaniumGuard(), new IronRush(), new BastionOfTitanium(), new UnrelentingOnslaught()) { }

        public override string Name {
            get => "Nocturne Umbra";
        }
        
        public override Texture2D CharacterIcon {
            get => ModContent.Request<Texture2D>("Textures/Nocturne/NocturneIcon").Value;
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 20; }
        public override Color HairColor { get => Color.Brown; }
        public override Color SkinColor { get => Color.DarkSalmon; }
        public override Color EyeColor { get => Color.Firebrick; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<NocturneSword>(); }
        public override int HeadVanityID { get => ItemID.GladiatorHelmet; }
        public override int HeadDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int BodyVanityID { get => ItemID.RedsBreastplate; }
        public override int BodyDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int LegVanityID { get => ItemID.RedsLeggings; }
        public override int LegDyeID { get => ItemID.ReflectiveMetalDye; }
    }
}