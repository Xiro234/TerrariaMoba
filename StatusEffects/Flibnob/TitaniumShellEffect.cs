using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public sealed class TitaniumShellEffect : StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Titanium Shell"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private int shellArmor;
        private int shellMagRes;
        private float moveSpeed;
        private float healDamage;
        private int mitigatedDamageTaken;
        
        public TitaniumShellEffect() { }

        public TitaniumShellEffect(int armor, int mr, float ms, float heal, int duration) : base(duration, true) {
            shellArmor = armor;
            shellMagRes = mr;
            moveSpeed = ms;
            healDamage = heal;
            
            ConstructFlatAttributes();
            ConstructMultAttributes();
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            //TODO - Store mitigated damage correctly.
            mitigatedDamageTaken = 500;
        }

        public override void FallOff() {
            User.statLife += (int) (mitigatedDamageTaken * healDamage);
            CombatText.NewText(User.Hitbox, Color.Aqua, 69420);
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(shellArmor);
            packet.Write(shellMagRes);
            packet.Write(moveSpeed);
            packet.Write(healDamage);
            packet.Write(mitigatedDamageTaken);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            shellArmor = reader.ReadInt32();
            shellMagRes = reader.ReadInt32();
            moveSpeed = reader.ReadSingle();
            healDamage = reader.ReadSingle();
            mitigatedDamageTaken = reader.ReadInt32();
            base.ReceiveEffectElements(reader);
        }

        public override void ConstructFlatAttributes() {
            FlatAttributes = new Dictionary<AttributeType, Func<float>> {
                { PHYSICAL_ARMOR, () => shellArmor },
                { MAGICAL_ARMOR, () => shellMagRes }
            };
        }
        
        public override void ConstructMultAttributes() {
            MultAttributes = new Dictionary<AttributeType, Func<float>>() {
                { MOVEMENT_SPEED, () => -moveSpeed }
            };
        }
    }
}