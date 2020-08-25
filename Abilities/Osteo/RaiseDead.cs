using System.Collections.Generic;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.NPCs;
using TerrariaMoba.Players;
using TerrariaMoba.UI;
using static Terraria.ModLoader.ModContent;


namespace TerrariaMoba.Abilities.Osteo {
    public class RaiseDead : Ability {
        public int marrowMax = 400;
        public int marrow = 400;

        
        public RaiseDead(Player myPlayer) : base(myPlayer) {
            Name = "Raise Dead";
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Lock");
        }

        public override void Cast() {
            int numSkeletons = marrow / 100;
            if (numSkeletons > 0) {
                for (int i = 0; i < numSkeletons; i++) {
                    Vector2 position = BaseAI.Trace(
                        new Vector2(player.Center.X + ((96) * player.direction) + (i * 32), player.Center.Y - 64),
                        new Vector2(player.Center.X, Main.bottomWorld), new Vector2(-1, -1), -1, false, true);

                    int npc = NPC.NewNPC((int) position.X, (int) position.Y, NPCType<OsteoSkeleton>());

                    Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = player.whoAmI;
                    Main.npc[npc].direction = player.direction;
                    (player.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Add(Main.npc[npc]);
                    
                    marrow -= 100;
                }

                Cooldown = 360;
            }
        }

        public override void DrawSelf(SpriteBatch spriteBatch, UIAbilityIcon abilityIcon) {
            Vector2 iconPos = abilityIcon.GetDimensions().Position();
            Vector2 pos = new Vector2(iconPos.X + 2, iconPos.Y + abilityIcon.Height.Pixels - 12);
            Color color = Color.Aqua;
            Utils.DrawBorderString(spriteBatch, marrow + "/" + marrowMax, pos, color, 0.65f);
        }
    }
}