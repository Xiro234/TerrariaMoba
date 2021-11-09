using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
            0f, Resource.Mana, 0f, 0f, 103f, 1.11f, 9f), 
            new VersatileCombatant(), new ViolentRetaliation(), new RallyingCry(), new BastionOfTitanium(), new FeedTheFury()) { }

        public override string Name {
            get => "Nocturne Umbra";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Nocturne/NocturneIcon", AssetRequestMode.ImmediateLoad);
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