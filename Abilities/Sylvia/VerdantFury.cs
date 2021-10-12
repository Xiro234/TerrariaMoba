using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class VerdantFury : Ability {
        public VerdantFury() : base("Verdant Fury", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Sylvia/SylviaAbilityTwo").Value; }
        
        public const float BUFF_ATKSPD_MODIFIER = 1.3f;
        public const float BUFF_ATKVEL_MODIFIER = 1.3f;
        public const int BUFF_BASE_DURATION = 240;

        public override void OnCast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                StatusEffectManager.AddEffect(User, new VerdantFuryEffect(BUFF_BASE_DURATION, BUFF_ATKSPD_MODIFIER, BUFF_ATKVEL_MODIFIER, false));
            }
        }
    }
}

/*using Terraria;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Sylvia {
    public class VerdantFury : Ability {
        public VerdantFury(Player myPlayer) : base(myPlayer) {
            Name = "Verdant Fury";
            Icon = ModContent.Request<Texture2D>("Textures/Sylvia/SylviaAbilityTwo").Value;
        }

        public override void OnCast() {
            //Player.AddBuff(BuffType<Buffs.VerdantFuryBuff>(), 60 * 4);
            if (Main.LocalPlayer == User) {
                User.GetModPlayer<MobaPlayer>().AddEffect(new Slow(360, User.whoAmI,0.3f));
            }
            cooldownTimer = 10 * 60;
        }
    }
}*/