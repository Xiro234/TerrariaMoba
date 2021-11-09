using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Jorm;
using TerrariaMoba.Items.Jorm;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Jorm : Character {
        public Jorm() { }
        
        public Jorm(Player user) : base(user) { }
        
        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 1340f },
                { MAX_MANA, () => 500f },
                { ATTACK_DAMAGE, () => 75f },
                { ATTACK_SPEED, () => 1.5f },
                { ATTACK_VELOCITY, () => 9f }
            };
        }

        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new DanceOfTheGoldenhammer(),
                new Consecration(),
                new SealOfHephaesta(),
                new Hammerfall(),
                new PaladinsResolve()
            };
        }
        

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