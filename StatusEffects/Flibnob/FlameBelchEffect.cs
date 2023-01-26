using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using Terraria.ID;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class FlameBelchEffect : StatusEffect {
    public override string DisplayName { get => "Burning"; }
        
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

    private int applierId;

    public FlameBelchEffect() { }

    public FlameBelchEffect(int id, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
        applierId = id;
    }

    public override void WhileActive() {
        User.GetModPlayer<MobaPlayer>().TakePvpDamage(physicalDamage: 0, magicalDamage: 69, trueDamage: 0, applierId, false);

        var num1 = Dust.NewDust(User.position, User.width, User.height, DustID.Torch, User.velocity.X * 0.2f, User.velocity.Y * 0.2f, 100);
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