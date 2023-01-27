using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class FlameBelchSecondEffect : StatusEffect {
    
    public override string DisplayName { get => "Melting"; }
        
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

    private int applierId;
    private int damageDealt;
    private int dotTimer;

    public FlameBelchSecondEffect() { }

    public FlameBelchSecondEffect(int id, int dmg, int duration, bool canBeCleansed) : base(duration, canBeCleansed) {
        applierId = id;
        damageDealt = dmg;
        dotTimer = 0;
    }

    public override void SendEffectElements(ModPacket packet) {
        packet.Write(applierId);
        packet.Write(damageDealt);
        base.SendEffectElements(packet);
    }

    public override void ReceiveEffectElements(BinaryReader reader) {
        applierId = reader.ReadInt32();
        damageDealt = reader.ReadInt32();
        base.ReceiveEffectElements(reader);
    }

    public override void WhileActive() {
        //cuts armor
        if (dotTimer == 0) {
            dotTimer = 60;
            User.GetModPlayer<MobaPlayer>().TakePvpDamage(0, 0, damageDealt, applierId, false);
        } else {
            dotTimer--;
        }

        var num1 = Dust.NewDust(User.position, User.width, User.height, DustID.Torch, User.velocity.X * 0.2f, User.velocity.Y * 0.2f, 100);
        Dust dust1;
        if (Main.rand.Next(3) < 2) {
            Main.dust[num1].noGravity = true;
            dust1 = Main.dust[num1];
            dust1.scale *= 6f;
            dust1.velocity.X *= 3f;
            dust1.velocity.Y *= 3f;
        }

        DurationTimer--;
    }
}