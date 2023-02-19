using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Statistic;
using TerrariaMoba.StatusEffects.GenericEffects;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class FlameBelchSecondEffect : DamageOverTime {
    
    public override string DisplayName { get => "Melting"; } 
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

    private int armorLoss;

    public FlameBelchSecondEffect() { }
    public FlameBelchSecondEffect(int armor, int dmg, int duration, bool canBeCleansed, int applierId) : base(0, 0, dmg, duration, canBeCleansed, applierId) {
        armorLoss = armor;
    }

    public override void WhileActive() {
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

    public override void ConstructFlatAttributes(){
        FlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { PHYSICAL_ARMOR, () => armorLoss }
            };
    }

    public override void SendEffectElements(ModPacket packet) {
        packet.Write(armorLoss);
        base.SendEffectElements(packet);
    }

    public override void ReceiveEffectElements(BinaryReader reader) {
        armorLoss = reader.ReadInt32();
        base.ReceiveEffectElements(reader);
    }
}