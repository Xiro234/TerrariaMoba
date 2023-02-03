using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
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

        public TitaniumShellEffect(int armor, int mr, float ms, float heal, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            shellArmor = armor;
            shellMagRes = mr;
            moveSpeed = ms;
            healDamage = heal;
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            var mobaPlayer = User.GetModPlayer<MobaPlayer>();
            int mitigatedPhysical = (int)Math.Ceiling(physicalDamage * mobaPlayer.GetCurrentAttributeValue(AttributeType.PHYSICAL_ARMOR) * 0.01f);
            int mitigatedMagical = (int)Math.Ceiling(magicalDamage * mobaPlayer.GetCurrentAttributeValue(AttributeType.MAGICAL_ARMOR) * 0.01f);
            mitigatedDamageTaken += mitigatedPhysical + mitigatedMagical;
        }

        public override void FallOff() {
            int finalDmg = (int)(mitigatedDamageTaken * healDamage);
            if (Main.netMode != NetmodeID.Server) {
                Main.NewText("Total damage mitigated by Titanium Shell: " + mitigatedDamageTaken);
            }
            User.statLife += finalDmg;
            CombatText.NewText(User.Hitbox, Color.Aqua, finalDmg);
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