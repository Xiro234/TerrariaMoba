using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Jorm;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Jorm : Character {
        public Jorm() { }
        
        public Jorm(Player user) : base(user, new Statistics(2150f, 0f, 500f,
            0f, Resource.Mana, 99f, 0.91f, 9f), 
            new DanceOfTheGoldenhammer(), new Consecration(), new SealOfHephaesta(), new Hammerfall(), new PaladinsResolve()) { }

        public override string Name {
            get => "Jorm Goldenhammer";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Jorm/JormIcon");
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 58; }
        public override Color HairColor { get => Color.Gold; }
        public override Color SkinColor { get => Color.DarkSalmon; }
        public override Color EyeColor { get => Color.Goldenrod; }
        public override int PrimaryWeaponID { get => ItemID.PaladinsHammer; }
        public override int HeadVanityID { get => ItemID.PalladiumHelmet; }
        public override int HeadDyeID { get => ItemID.YellowandSilverDye; }
        public override int BodyVanityID { get => ItemID.PalladiumBreastplate; }
        public override int BodyDyeID { get => ItemID.YellowandSilverDye; }
        public override int LegVanityID { get => ItemID.PalladiumLeggings; }
        public override int LegDyeID { get => ItemID.YellowandSilverDye; }
    }
    
    //TODO - [Texture] Ability icons
    //TODO - [Char] Paladin's Shield slot
    //TODO - [Trait] Implement trait entirely.
    //TODO - [Trait] Implement bonuses for A1-3.
    //TODO - [A1] Possibly a special effect that overlays the player if they are dazed by Jorm.
    //TODO - [A2] Implement effects; allies = increased heal effect, enemies = reduced heal effect, purified effect
    //TODO - [U2] Implement effect for 100% phys damage immunity. 
    
}