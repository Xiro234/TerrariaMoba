using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.OldMan;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class OldMan : Character {
        public OldMan() { }
        
        public OldMan(Player user) : base(user, new Statistics(2000f, 0f, 500f,
            0f, Resource.Mana, 0f, 0f, 75f, 1.5f, 9f), 
            new Frostbite(), new ExplosiveCatch(), new PerfectFillet(), new Riptide(), new WhatsInTheCrate()) { }

        public override string Name {
            get => "Balner Gaulish";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/OldMan/OldManIcon", AssetRequestMode.ImmediateLoad);
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 2; }
        public override Color HairColor { get => Color.LightGray; }
        public override Color SkinColor { get => Color.DarkSalmon; }
        public override Color EyeColor { get => Color.RoyalBlue; }
        public override int PrimaryWeaponID { get => ItemID.ReinforcedFishingPole; }
        public override int BodyVanityID { get => ItemID.RainCoat; }
        public override int BodyDyeID { get => ItemID.ReflectiveDye; }
        public override int LegVanityID { get => ItemID.AnglerPants; }
        public override int LegDyeID { get => ItemID.ReflectiveDye; }
    }
}