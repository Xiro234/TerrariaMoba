using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class FlameBelchSecondEffect : DamageOverTime {
    
    public override string DisplayName { get => "Melting"; } 
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

    public FlameBelchSecondEffect() { }
    public FlameBelchSecondEffect(int dmg, int duration, bool canBeCleansed, int applierId) : base(0, 0, dmg, duration, canBeCleansed, applierId) { }

    public override void WhileActive() {
        //cuts armor
        var num1 = Dust.NewDust(User.position, User.width, User.height, DustID.Torch, User.velocity.X * 0.2f, User.velocity.Y * 0.2f, 100);
        Dust dust1;
        if (Main.rand.Next(3) < 2) {
            Main.dust[num1].noGravity = true;
            dust1 = Main.dust[num1];
            dust1.scale *= 6f;
            dust1.velocity.X *= 3f;
            dust1.velocity.Y *= 3f;
        }

        base.WhileActive();
    }

    public override void SendEffectElements(ModPacket packet) {

        base.SendEffectElements(packet);
    }

    public override void ReceiveEffectElements(BinaryReader reader) {

        base.ReceiveEffectElements(reader);
    }
}