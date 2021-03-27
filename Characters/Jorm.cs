﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Jorm : Character {
        public Jorm(Player user) : base(user, new Statistics(2150f, 0f, 500f,
            0f, Statistics.Resource.Mana, 99f, 0.91f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Jorm Goldenhammer";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Jorm/JormIcon");
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 58; }
        public override Color HairColor { get => Color.Gold; }
        public override Color SkinColor { get => Color.PeachPuff; }
        public override Color EyeColor { get => Color.Goldenrod; }
        public override int PrimaryWeaponID { get => ItemID.PaladinsHammer; }
        public override int HeadVanityID { get => ItemID.PalladiumHelmet; }
        public override int HeadDyeID { get => ItemID.YellowandSilverDye; }
        public override int BodyVanityID { get => ItemID.PalladiumBreastplate; }
        public override int BodyDyeID { get => ItemID.YellowandSilverDye; }
        public override int LegVanityID { get => ItemID.PalladiumLeggings; }
        public override int LegDyeID { get => ItemID.YellowandSilverDye; }
        
        //TODO - Paladin's Shield slot
    }
}