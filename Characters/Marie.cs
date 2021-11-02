using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities.Marie;
using TerrariaMoba.Items.Marie;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Marie : Character {
        public Marie() { }
        
        public Marie(Player user) : base(user, new Statistics(1460f, 0f, 500f,
            0f, Resource.Mana, 0f, 0f, 75f, 1.5f, 9f), 
            new WhirlpoolInABottle(),  new SurgingVitality(), new RefreshingRipple(), new EyeOfTheStorm(), new PendantOfTorrents()) { }

        public override string Name {
            get => "Marie Tidewrath";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieIcon", AssetRequestMode.ImmediateLoad);
        }
        
        public override bool IsMale { get => false; }
        public override int HairID { get => 5; }
        public override Color HairColor { get => Color.DodgerBlue; }
        public override Color SkinColor { get => Color.LightSalmon; }
        public override Color EyeColor { get => Color.Blue; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<MarieStaff>(); }
        public override int HeadVanityID { get => ItemID.BejeweledValkyrieHead; }
        public override int HeadDyeID { get => ItemID.SkyBlueDye; }
        public override int BodyVanityID { get => ItemID.FishCostumeShirt; }
        public override int LegVanityID { get => ItemID.FishCostumeFinskirt; }
    }
}