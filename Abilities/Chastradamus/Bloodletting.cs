using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Projectiles;

namespace TerrariaMoba.Abilities.Chastradamus {
    public class Bloodletting : Ability, IModifyHitPvpWithProj {
        public Bloodletting(Player player) : base(player, "Bloodletting", 0, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public int blood;
        public const int BASE_MAX_BLOOD = 100;

        public override void WhileActive() {
            //TODO - Chastradamus gathers blood from dealing damage to enemies, at different stages, gives him buffs, but is consumed by A3 to heal him. Lose on death.
        }

        public void ModifyHitPvpWithProj(Projectile proj, Player target, ref int phyiscalDamage, ref int magicalDamage, ref int trueDamage, ref bool crit) {
            var gProj = proj.GetGlobalProjectile<DamageTypeGlobalProj>();
            //Possibly re-evaluate to non-premitigated damage?

            AddBlood(gProj.TotalPremitigated); //modhitpvpwithproj is always called before mitigation so this would be full damage
        }

        private void AddBlood(int damage) {
            blood += (int)Math.Floor(damage * 0.05);

            if (blood >= BASE_MAX_BLOOD) {
                blood = BASE_MAX_BLOOD;
            }
        }
    }
}