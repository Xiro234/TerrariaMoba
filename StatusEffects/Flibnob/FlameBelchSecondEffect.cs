using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class FlameBelchSecondEffect : StatusEffect {
    
    public override string DisplayName { get => "Melting"; }
        
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

    private int applierId;

    public FlameBelchSecondEffect() { }

    public FlameBelchSecondEffect(int id, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
        applierId = id;
    }
    
    public override void WhileActive() {
        //cuts armor
        User.GetModPlayer<MobaPlayer>().TakePvpDamage(0, 420, 0, applierId, false);

        var num1 = Dust.NewDust(User.position, User.width, User.height, DustID.Torch, User.velocity.X * 0.2f, User.velocity.Y * 0.2f, 100);
        Dust dust1;
        if (Main.rand.Next(3) < 2) {
            Main.dust[num1].noGravity = true;
            dust1 = Main.dust[num1];
            dust1.scale *= 6f;
            dust1.velocity.X *= 3f;
            dust1.velocity.Y *= 3f;
        }
    }
}