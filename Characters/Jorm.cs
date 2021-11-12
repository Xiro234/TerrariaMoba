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
                { MAX_HEALTH, () => 2150f },
                { HEALTH_REGEN, () => 4.5f },
                { MAX_MANA, () => 500f },
                { MANA_REGEN, () => 2.1f },
                { PHYSICAL_ARMOR, () => 10f },
                { MAGICAL_ARMOR, () => 10f },
                { ATTACK_DAMAGE, () => 99f },
                { ATTACK_SPEED, () => 0.91f },
                { ATTACK_VELOCITY, () => 6f },
                { MOVEMENT_SPEED, () => 1f },
                { JUMP_SPEED, () => 1f }
            };
        }

        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new DanceOfTheGoldenhammer(User),
                new Consecration(User),
                new SealOfHephaesta(User),
                new Hammerfall(User),
                new PaladinsResolve(User)
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
    //TODO - Paladin's Shield slot
    
}