using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Items.Sylvia;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Sylvia : Character {
        public Sylvia() { }

        public Sylvia(Player user) : base(user) { }

        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 1340f },
                { HEALTH_REGEN, () => 2.8f },
                { MAX_MANA, () => 500f },
                { MANA_REGEN, () => 2.1f },
                { PHYSICAL_ARMOR, () => 0f },
                { MAGICAL_ARMOR, () => 0f },
                { ATTACK_DAMAGE, () => 75f },
                { ATTACK_SPEED, () => 1.5f },
                { ATTACK_VELOCITY, () => 9f },
                { MOVEMENT_SPEED, () => 1f },
                { JUMP_SPEED, () => 1f }
            };
        }

        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new EnsnaringVines(User),
                new WitheredRose(User),
                new VerdantFury(User),
                new Flourish(User),
                new JunglesWrath(User)
            };
        }

        public override string Name {
            get => "Sylvia Verda";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaIcon", AssetRequestMode.ImmediateLoad);
        }

        public override bool IsMale { get => false; }
        public override int HairID { get => 55; }
        public override Color HairColor { get => Color.ForestGreen; }
        public override Color SkinColor { get => Color.Peru; }
        public override Color EyeColor { get => Color.Sienna; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<SylviaBow>(); }
        public override int HeadVanityID { get => ItemID.JungleRose; }
        public override int BodyVanityID { get => ItemID.DryadCoverings; }
        public override int LegVanityID { get => ItemID.DryadLoincloth; }
    }
}