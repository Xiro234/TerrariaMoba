using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class JunglesWrathEffect : StatusEffect {

        public JunglesWrathEffect(int duration, int stacks) : base(duration, true) {
            Stacks = stacks;
        }

        public JunglesWrathEffect() { }

        public override string DisplayName {
            get => "Jungle's Wrath";
        }

        public override Texture2D Icon {
            get => ModContent.Request<Texture2D>("Textures/Blank").Value;
        }

        protected override bool ShowBar {
            get => false;
        }

        public int Stacks { get; set; }

        public override void SendEffectElements(ModPacket packet) {
            base.SendEffectElements(packet);
            packet.Write(Stacks);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            base.ReceiveEffectElements(reader);
            Stacks = reader.ReadInt32();
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
                drawPlayer.Top.Y - Main.screenPosition.Y - 40);
            DrawData data = new DrawData(texture, texturePos, Color.White);
            drawInfo.DrawDataCache.Add(data);
        }
    }
}