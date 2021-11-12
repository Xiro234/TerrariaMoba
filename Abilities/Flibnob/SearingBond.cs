using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Flibnob;

namespace TerrariaMoba.Abilities.Flibnob {
    public class SearingBond : Ability {
        public SearingBond(Player player) : base(player, "Searing Bond", 0, 0, AbilityType.Passive) {
        }

        public override Texture2D Icon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Flibnob/FlibnobTrait").Value;
        }

        public const int BASE_ARMOR_GAIN = 5;
        //public const int BASE_MR_GAIN = 0;
        public const float BUFF_RANGE = 200f;
        public int finalStacks;

        public override void WhileActive() {
            //+10 to User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics.PhysicalArmor

            int total = 0;
            for (int i = 0; i < Main.maxPlayers; i++) {
                Player plr = Main.player[i];
                float dist = (plr.Center - User.Center).Length();
                if (plr.active && plr.team != User.team && dist <= BUFF_RANGE && i != User.whoAmI) { // && plr.whatever.hasEffect("fire")
                    total++;
                }
            }
            finalStacks = total;

            StatusEffectManager.AddEffect(User, new SearingBondEffect(total, BASE_ARMOR_GAIN, 60, false));
            
            //TODO - Remove effect, when xiro makes attribute stuff implement here
            
            //TODO - All physical damage has a chance to ignite, make sure effect works correctly
        }
    }
}