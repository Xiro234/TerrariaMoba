using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;

namespace TerrariaMoba.Abilities.Jorm {
    public class Consecration : Ability {
        public Consecration(Player player) : base(player, "Consecration", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Lock").Value; }

        public const float CONSEC_SPREAD_RANGE = 500f;
        public const int CONSEC_DURATION = 300;

        public override void OnCast() {
            //TODO - ally in contact = +HPR / enemy in contact = -HPR
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                PaladinsResolve pr = User.GetModPlayer<MobaPlayer>().Hero.Trait as PaladinsResolve;
                if (pr != null) {
                    pr.AddStack();
                }
                
                Projectile proj = Projectile.NewProjectileDirect(new ProjectileSource_Ability(User, this), User.Center, Vector2.Zero,
                    ModContent.ProjectileType<Projectiles.Jorm.Consecration>(), 0, 0, User.whoAmI);

                Projectiles.Jorm.Consecration consec = proj.ModProjectile as Projectiles.Jorm.Consecration;
                if (consec != null) {
                    consec.ConsecSpread = CONSEC_SPREAD_RANGE;
                    consec.ConsecDuration = CONSEC_DURATION;
                }
            }
        }
    }
}