﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

 namespace TerrariaMoba.Abilities.Jorm {
    public class Consecration : Ability {
        public Consecration() : base("Consecration", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public const float CONSEC_SPREAD_RANGE = 500f;
        public const int CONSEC_DURATION = 300;

        public override void OnCast() {
            //TODO - consecrate ground in ~ radius, +healeff of allies, -healeff of enemies on top.
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile proj = Projectile.NewProjectileDirect(User.Center, Vector2.Zero,
                    TerrariaMoba.Instance.ProjectileType("Consecration"), 0, 0, User.whoAmI);

                Projectiles.Jorm.Consecration consec = proj.modProjectile as Projectiles.Jorm.Consecration;
                if (consec != null) {
                    consec.ConsecSpread = CONSEC_SPREAD_RANGE;
                    consec.ConsecDuration = CONSEC_DURATION;
                }
            }
            PaladinsResolve pr = User.GetModPlayer<MobaPlayer>().Hero.Trait as PaladinsResolve;
            if(pr != null) pr.AddStack();
        }
    }
}