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

        public override Dictionary<AttributeType, float> BaseAttributes => new Dictionary<AttributeType, float>() {
            { MAX_HEALTH, 1340f },
            { MAX_MANA, 500f },
            { ATTACK_DAMAGE, 75f },
            { ATTACK_SPEED, 1.5f },
            { ATTACK_VELOCITY, 9f }
        };

        public override Ability[] Skills => new Ability[] {
            new EnsnaringVinesAbility(),
            new GracefulLeap(),
            new VerdantFury(),
            new Flourish(),
            new JunglesWrathAbility()
        };

        public Sylvia(Player user) : base(user) { }
        
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