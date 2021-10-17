using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Jorm;
using TerrariaMoba.Items.Jorm;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Jorm : Character {
        public Jorm() { }
        
        public Jorm(Player user) : base(user, new Statistics(2150f, 0f, 500f,
            0f, Resource.Mana, 10f, 0f, 99f, 0.91f, 9f), 
            new DanceOfTheGoldenhammer(), new Consecration(), new SealOfHephaesta(), new Hammerfall(), new PaladinsResolve()) { }

        public override string Name {
            get => "Jorm Goldenhammer";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Jorm/JormIcon", AssetRequestMode.ImmediateLoad);
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 58; }
        public override Color HairColor { get => Color.Gold; }
        public override Color SkinColor { get => Color.DarkSalmon; }
        public override Color EyeColor { get => Color.Goldenrod; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<JormHammer>(); }
        public override int HeadVanityID { get => ItemID.PalladiumHelmet; }
        public override int HeadDyeID { get => ItemID.YellowandSilverDye; }
        public override int BodyVanityID { get => ItemID.PalladiumBreastplate; }
        public override int BodyDyeID { get => ItemID.YellowandSilverDye; }
        public override int LegVanityID { get => ItemID.PalladiumLeggings; }
        public override int LegDyeID { get => ItemID.YellowandSilverDye; }
    }
    
    //TODO - [Texture] Ability icons
    //TODO - [Char] Paladin's Shield slot
    
}