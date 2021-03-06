using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using TerrariaMoba.Effects;

namespace TerrariaMoba.Players {
    public static class MobaLayers {
        public static readonly PlayerLayer MiscEffectsFront = new PlayerLayer("TerrariaMoba", "MiscEffects", PlayerLayer.MiscEffectsFront, delegate(PlayerDrawInfo drawInfo) {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModLoader.GetMod("TerrariaMoba");
            MobaPlayer modPlayer = drawPlayer.GetModPlayer<MobaPlayer>();

            if (modPlayer.SylviaEffects.EnsnaringVines) {
                Texture2D texture = mod.GetTexture("Textures/Sylvia/EnsnaringVines");
                
                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + (drawPlayer.height - 2f) - Main.screenPosition.Y);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }
            
            if(modPlayer.SylviaEffects.JunglesWrath) {
                Texture2D texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath1");
                bool draw = true;
                switch (modPlayer.SylviaEffects.JunglesWrathCount) {
                    case(1):
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath1");
                        break;
                    case(2):
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath2");
                        break;
                    case(3):
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath3");
                        break;
                    case(4):
                        texture = mod.GetTexture("Textures/Sylvia/JunglesWrath/JunglesWrath4");
                        break;
                    default:
                        draw = false;
                        break;
                }

                if (draw) {
                    int drawX = (int) (drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                    int drawY = (int) (drawInfo.position.Y + (drawPlayer.height - 56f) - Main.screenPosition.Y);
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY),
                        new Rectangle(0, 0, texture.Width, texture.Height),
                        Lighting.GetColor((int) ((drawInfo.position.X + drawPlayer.width / 2f) / 16f),
                            (int) ((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f,
                        new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                    Main.playerDrawData.Add(data);
                }
            }
            
            if (modPlayer.FlibnobEffects.TitaniumShell) {
                Texture2D texture = mod.GetTexture("Textures/Flibnob/TitaniumShell");

                int drawX = (int)(drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.position.Y + drawPlayer.height / 2f - Main.screenPosition.Y);
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), new Rectangle(0, 0, texture.Width, texture.Height), Lighting.GetColor((int)((drawInfo.position.X + drawPlayer.width / 2f) / 16f), (int)((drawInfo.position.Y + drawPlayer.height) / 16f)), 0f, new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                Main.playerDrawData.Add(data);
            }

            if (modPlayer.ultTimer >= 0) {
                Texture2D texture = mod.GetTexture("Textures/Osteo/SongOfTheDamnedSword");
                int drawX = (int) (drawInfo.position.X + drawPlayer.width / 2f - Main.screenPosition.X);
                
                double displace = 0;
                int timer = modPlayer.ultTimer;

                displace = OsteoEffects.GetSwordPosition(timer);

                int drawY = (int) (drawInfo.position.Y - 80 + displace - Main.screenPosition.Y);

                if (displace < 100) { //Checks if it goes past the player
                    DrawData data = new DrawData(texture, new Vector2(drawX, drawY),
                        new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 2.35619f,
                        new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                    Main.playerDrawData.Add(data);
                }
                
            }
        });

        public static readonly PlayerLayer MiscEffectsBack = new PlayerLayer("TerrariaMoba", "MiscEffects",
            PlayerLayer.MiscEffectsBack,
            delegate(PlayerDrawInfo drawInfo) {
                
            });
    }
}