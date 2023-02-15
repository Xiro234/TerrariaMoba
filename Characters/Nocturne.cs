using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Nocturne;
using TerrariaMoba.Items.Nocturne;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Nocturne : Character {
        public Nocturne() { }
        
        public Nocturne(Player user) : base(user) { }
        
        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 1825f },
                { HEALTH_REGEN, () => 3.8f },
                { MAX_MANA, () => 500f },
                { MANA_REGEN, () => 2.1f },
                { PHYSICAL_ARMOR, () => 5f },
                { MAGICAL_ARMOR, () => 5f },
                { ATTACK_DAMAGE, () => 103f },
                { ATTACK_SPEED, () => 0.12f },
                { ATTACK_VELOCITY, () => 8f },
                { MOVEMENT_SPEED, () => 1f },
                { JUMP_SPEED, () => 1f },
                { HEALING_EFFECTIVENESS, () => 1f },
                { STATUS_RESISTANCE, () => 0f }
            };
        }
        
        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new VersatileCombatant(User),  
                new ViolentRetaliation(User), 
                new RallyingCry(User), 
                new BastionOfTitanium(User), 
                new FeedTheFury(User),
                new EclipteranLightbringer(User)
            };
        }

        public override string Name {
            get => "Nocturne Umbra";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Nocturne/NocturneIcon", AssetRequestMode.ImmediateLoad);
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 20; }
        public override Color HairColor { get => Color.Brown; }
        public override Color SkinColor { get => Color.DarkSalmon; }
        public override Color EyeColor { get => Color.Firebrick; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<NocturneSword>(); }
        public override int HeadVanityID { get => ItemID.GladiatorHelmet; }
        public override int HeadDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int BodyVanityID { get => ItemID.RedsBreastplate; }
        public override int BodyDyeID { get => ItemID.ReflectiveMetalDye; }
        public override int LegVanityID { get => ItemID.RedsLeggings; }
        public override int LegDyeID { get => ItemID.ReflectiveMetalDye; }
    }
}