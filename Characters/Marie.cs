using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Marie : Character {
        public Marie(Player user) : base(user, new Statistics(1460f, 0f, 500f,
            0f, Resource.Mana, 75f, 1.5f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Marie Tidewrath";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Marie/MarieIcon");
        }
        
        public override bool IsMale { get => false; }
        public override int HairID { get => 5; }
        public override Color HairColor { get => Color.DodgerBlue; }
        public override Color SkinColor { get => Color.LightSalmon; }
        public override Color EyeColor { get => Color.Blue; }
        public override int PrimaryWeaponID { get => TerrariaMoba.Instance.ItemType("MarieStaff"); }
        public override int HeadVanityID { get => ItemID.BejeweledValkyrieHead; }
        public override int HeadDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int BodyVanityID { get => ItemID.FishCostumeShirt; }
        public override int LegVanityID { get => ItemID.FishCostumeFinskirt; }
    }
}

/*
namespace TerrariaMoba.Characters {
    public class Marie : Character {
        public override string FullName {
            get => "Marie Tidewrath, High Priestess of Lacusia";
        }
        public override void SetPlayer() {
            vanityHead.SetDefaults(3226);
            dyeHead.SetDefaults(1014);
            vanityLeg.SetDefaults(2500);
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("MarieStaff"));

            player.Male = false;
            player.hair = 5;
            player.hairColor = new Color(0, 133, 255);
            player.skinColor = new Color(235, 159, 125);
            player.eyeColor = new Color(0, 0, 255);
            baseMaxLife = 1460;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = 500;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            baseArmor = 0;
            
            QAbility = new WhirlpoolInABottle(player);
            EAbility = new TomeOfLacusia(player);
            RAbility = new EyeOfTheStorm(player);
            CAbility = new Floodboost(player);
            
            /*
            FountainOfTheGoddess ultimate = new FountainOfTheGoddess(player);
            abilities[2] = ultimate;
            #1#
        }
*/