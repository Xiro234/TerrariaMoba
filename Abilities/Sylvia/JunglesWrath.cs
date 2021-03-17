using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles.Sylvia;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Sylvia;

namespace TerrariaMoba.Abilities.Sylvia {
    public class JunglesWrath : Ability, IModifyHitPvpWithProj  {
        public JunglesWrath() : base("Jungles Wrath", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Sylvia/SylviaTrait"); }

        public const int EFFECT_DURATION = 120;
        public const float DAMAGE_PERCENT = 0.04f;

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            var modProjectile = proj.modProjectile;
            SylviaArrow arrow = modProjectile as SylviaArrow;
            if (arrow != null) {
                var mobaPlayer = target.GetModPlayer<MobaPlayer>();
                foreach (var effect in mobaPlayer.EffectList) {
                    var JWrath = effect as JunglesWrathEffect;

                    if (JWrath != null) {
                        if (JWrath.Stacks < 5) {
                            JWrath.Stacks += 1;
                            JWrath.NeedSyncing = true;
                        }
                        else {
                            JWrath.Stacks = 0;
                        }
                    }
                }
                StatusEffectManager.AddEffect(target, new JunglesWrathEffect(EFFECT_DURATION, 1));
            }
        }
    }
}