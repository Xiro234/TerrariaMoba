using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.Sylvia {
    public class JunglesWrathEffect : StatusEffect {

        public JunglesWrathEffect(int duration, int stacks) : base(duration, true) {
            Stacks = stacks;
        }

        public override string DisplayName { get => "Jungle's Wrath"; }
        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }
        
        public int Stacks { get; set; }

        public override void SendEffectElements(ModPacket packet) {
            base.SendEffectElements(packet);
            packet.Write(Stacks);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            base.ReceiveEffectElements(reader);
            Stacks = reader.ReadInt32();
        }

        public override void GetListOfPlayerLayers(List<PlayerLayer> playerLayers) {
            var playerLayer = new PlayerLayer("TerrariaMoba", DisplayName, PlayerLayer.MiscEffectsFront, delegate(PlayerDrawInfo drawInfo) {
                Player drawPlayer = drawInfo.drawPlayer;
                Mod mod = ModLoader.GetMod("TerrariaMoba");
                MobaPlayer mobaPlayer = drawPlayer.GetModPlayer<MobaPlayer>();
                
                Texture2D texture = null;
                switch (Stacks) {
                    case 1:
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath1");
                        break;
                    case 2:
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath2");
                        break;
                    case 3:
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath3");
                        break;
                    case 4:
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath4");
                        break;
                    default:
                        base.GetListOfPlayerLayers(playerLayers);
                        return;
                }
                
                Vector2 texturePos = new Vector2(drawPlayer.Top.X - Main.screenPosition.X - (texture.Width/2) - 10,
                    drawPlayer.Top.Y - Main.screenPosition.Y - 44);
                DrawData data = new DrawData(texture, texturePos, Color.White);
                Main.playerDrawData.Add(data);
            });
            
            base.GetListOfPlayerLayers(playerLayers);
        }
    }
}