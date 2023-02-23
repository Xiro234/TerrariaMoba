using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.IO;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class FlameBelchEffect : DamageOverTime {
    public override string DisplayName { get => "Burning"; }
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

    public FlameBelchEffect() { }
    public FlameBelchEffect(int dmg, int duration, bool canBeCleansed, int applierId) : base(0, dmg, 0, duration, canBeCleansed, applierId) { }

    public override void WhileActive() {
        var num1 = Dust.NewDust(User.position, User.width, User.height, DustID.Torch, User.velocity.X * 0.2f, User.velocity.Y * 0.2f, 100);
        Dust dust1;
        if (Main.rand.Next(3) < 1) {
            Main.dust[num1].noGravity = true;
            dust1 = Main.dust[num1];
            dust1.scale *= 3f;
            dust1.velocity.X *= 2f;
            dust1.velocity.Y *= 2f;
        }

        base.WhileActive();
    }
}