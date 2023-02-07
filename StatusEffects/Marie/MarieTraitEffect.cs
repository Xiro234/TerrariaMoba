using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Marie
{
    public class MarieTraitEffect : StatusEffect, IModifyHitPvpWithProj {

        public override string DisplayName { get => "Goddess's Blessing"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        private int damageIncrease;

        public MarieTraitEffect() { }
        public MarieTraitEffect(int magicDamage, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { 
            damageIncrease = magicDamage;
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            target.GetModPlayer<MobaPlayer>().TakePvpDamage(0, damageIncrease, 0, User.whoAmI, false);
        }
    }
}
