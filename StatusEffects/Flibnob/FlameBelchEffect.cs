using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class FlameBelchEffect : StatusEffect {
    public override string DisplayName { get => "Ignited"; }
        
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

    public FlameBelchEffect() { }

    public FlameBelchEffect(int duration, bool canBeCleansed) : base(duration, canBeCleansed) { }

    public override void WhileActive() {
        User.Hurt(PlayerDeathReason.ByCustomReason("Fire burn all human to death!"), 69, -User.direction, true);
        var num1 = Dust.NewDust(User.position, User.width, User.height, 6, User.velocity.X * 0.2f, User.velocity.Y * 0.2f, 100);
        Dust dust1;
        if (Main.rand.Next(3) < 1) {
            Main.dust[num1].noGravity = true;
            dust1 = Main.dust[num1];
            dust1.scale *= 3f;
            dust1.velocity.X *= 2f;
            dust1.velocity.Y *= 2f;
        }
    }
}