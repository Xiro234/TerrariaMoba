using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Flibnob;
using TerrariaMoba.Items.Flibnob;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Flibnob : Character {
        public Flibnob() { }

        public Flibnob(Player user) : base(user) { }
        
        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 2060f },
                { HEALTH_REGEN, () => 4.3f },
                { MAX_MANA, () => 500f },
                { MANA_REGEN, () => 2.1f },
                { PHYSICAL_ARMOR, () => 10f },
                { MAGICAL_ARMOR, () => 10f },
                { ATTACK_DAMAGE, () => 137f },
                { ATTACK_SPEED, () => -0.15f },
                { ATTACK_VELOCITY, () => 6f },
                { MOVEMENT_SPEED, () => 1f },
                { JUMP_SPEED, () => 1f }
            };
        }

        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new FlameBelch(User),
                new Rockwrecker(User),
                new TitaniumShell(User),
                new Earthsplitter(User),
                new SearingBond(User),
                new CullTheMeek(User)
            };
        }

        public override string Name {
            get => "Flibnob";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobIcon", AssetRequestMode.ImmediateLoad);
            
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 15; }
        public override Color HairColor { get => Color.Black; }
        public override Color SkinColor { get => Color.SaddleBrown; }
        public override Color EyeColor { get => Color.Red; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<FlibnobAxe>(); }
        public override int HeadVanityID { get => ItemID.BossMaskOgre; }
        public override int BodyVanityID { get => ItemID.RedsBreastplate; }
        public override int BodyDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int LegVanityID { get => ItemID.RedsLeggings; }
        public override int LegDyeID { get => ItemID.ReflectiveMetalDye; }
    }
}