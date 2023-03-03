using System;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class JunglesWrathEffect : StatusEffect, ITakePvpDamage {
        public override string DisplayName { get => "Jungle's Wrath"; }
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public int Stacks { get; set; }
        public float DamagePercent { get; set; }

        public JunglesWrathEffect() { }
        public JunglesWrathEffect(int duration, int applicantId, float damagePercent, int stacks) : base(duration, true, applicantId) {
            Stacks = stacks;
            DamagePercent = damagePercent;
        }

        protected override bool ShowBar {
            get => false;
        }

        public override void ReApply() {
            base.ReApply();

            if (Stacks < 5) {
                Stacks += 1;
            }
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            if (Stacks >= 5) {
                trueDamage += (int)Math.Ceiling(User.statLifeMax2 * DamagePercent);
                SoundEngine.PlaySound(SoundID.Item100, User.position);
                const float distance = 50f;
                for (int i = 0; i < 20; i++) {
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * distance);
                    offset.Y += (float)(Math.Cos(angle) * distance);
                    Dust dust = Main.dust[Dust.NewDust(User.Center + offset, 0, 0, DustID.Venom, 0, 0, 150, Color.Crimson, 0.9f)];
                    dust.velocity += Vector2.Normalize(offset) * -5f;
                    dust.noGravity = true;
                }
                DurationTimer = 0;
            }
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(Stacks);
            packet.Write(DamagePercent);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            Stacks = reader.ReadInt32();
            DamagePercent = reader.ReadSingle();
            base.ReceiveEffectElements(reader);
        }
    }
    
    public class JunglesWrathDrawLayer : PlayerDrawLayer {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) {
            return StatusEffectManager.PlayerHasEffectType<JunglesWrathEffect>(drawInfo.drawPlayer);
        }

        public override Position GetDefaultPosition() => new Between(PlayerDrawLayers.ProjectileOverArm,
            PlayerDrawLayers.FrozenOrWebbedDebuff);

        protected override void Draw(ref PlayerDrawSet drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            var effect = drawPlayer.GetModPlayer<MobaPlayer>().EffectList.OfType<JunglesWrathEffect>().First();

            Texture2D texture;
            switch (effect.Stacks) {
                case 1:
                    texture = Mod.Assets.Request<Texture2D>("Textures/Sylvia/JunglesWrath/JunglesWrath1").Value;
                    break;
                case 2:
                    texture = Mod.Assets.Request<Texture2D>("Textures/Sylvia/JunglesWrath/JunglesWrath2").Value;
                    break;
                case 3:
                    texture = Mod.Assets.Request<Texture2D>("Textures/Sylvia/JunglesWrath/JunglesWrath3").Value;
                    break;
                case 4:
                    texture = Mod.Assets.Request<Texture2D>("Textures/Sylvia/JunglesWrath/JunglesWrath4").Value;
                    break;
                default:
                    return;
            }

            Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width / 2),
                drawPlayer.Top.Y - Main.screenPosition.Y - 60);
            DrawData data = new DrawData(texture, texturePos, Color.White);
            drawInfo.DrawDataCache.Add(data);
        }
    }
}