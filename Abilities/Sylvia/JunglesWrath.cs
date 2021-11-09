using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class JunglesWrath : Ability, IModifyHitPvpWithProj  {
        public JunglesWrath() : base("Jungle's Wrath", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Sylvia/SylviaTrait").Value; }

        public const int EFFECT_DURATION = 2000;
        public const float DAMAGE_PERCENT = 0.04f;

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var ModProjectile = proj.ModProjectile;
            SylviaArrow arrow = ModProjectile as SylviaArrow;
            
            if (arrow != null) {
                var mobaPlayer = target.GetModPlayer<MobaPlayer>();
                foreach (var effect in mobaPlayer.EffectList) {
                    var JWrath = effect as JunglesWrathEffect;

                    if (JWrath != null) {
                        if (JWrath.Stacks < 4) {
                            JWrath.Stacks += 1;
                            StatusEffectManager.SyncSingleEffect(target, JWrath);
                        }
                        else {
                            StatusEffectManager.RemoveEffect(target, JWrath);
                            damage += (int)Math.Ceiling(target.statLifeMax2 * DAMAGE_PERCENT);
                        }
                        return;
                    }
                }
                Main.NewText("woo");
                StatusEffectManager.AddEffect(target, new JunglesWrathEffect(EFFECT_DURATION, 1));
            }
        }
    }
}