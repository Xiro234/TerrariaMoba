using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Marie;

namespace TerrariaMoba.Abilities.Marie {
    public class BlessingOfTheGoddess : Ability, IOnHeal, IOnHealOtherPlayer {

        public BlessingOfTheGoddess(Player player) : base(player, "Blessing of the Goddess", 60, 0, AbilityType.Passive) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieUltimateOne").Value; }

        public const int BONUS_DAMAGE = 30;
        public const int BUFF_DURATION = 300;

        public void OnHeal(ref int amount, ref bool doText) {
            StatusEffectManager.AddEffect(User, new MarieTraitEffect(BONUS_DAMAGE, BUFF_DURATION, false, User.whoAmI));
        }

        public void OnHealOtherPlayer(Player target, ref int amount, ref bool doText) {
            StatusEffectManager.AddEffect(target, new MarieTraitEffect(BONUS_DAMAGE, BUFF_DURATION, false, User.whoAmI));
        }
    }
}