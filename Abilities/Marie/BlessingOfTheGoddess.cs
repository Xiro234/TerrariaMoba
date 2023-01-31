using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Marie;

namespace TerrariaMoba.Abilities.Marie {
    public class BlessingOfTheGoddess : Ability {

        public BlessingOfTheGoddess(Player player) : base(player, "Blessing of the Goddess", 60, 0, AbilityType.Passive) { }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Marie/MarieTrait").Value; }

        public const int BONUS_DAMAGE = 30;
        
        public void PlayerHealed(Player plr) {
            StatusEffectManager.AddEffect(plr, new MarieTraitEffect(BONUS_DAMAGE, 600, false));
        }

        //TODO - Whenever Marie heals herself or an ally, they are blessed for 3 seconds. For this duration, basic attacks and abilities deal a bonus 30(4 % sc) magic damage.
    }
}