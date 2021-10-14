using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class RaiseDead : Ability {
        public RaiseDead() : base("Raise Dead", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoAbilityOne").Value; }
    }
}

/*using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.NPCs;
using TerrariaMoba.Players;
using TerrariaMoba.UI;
using static Terraria.ModLoader.ModContent;


namespace TerrariaMoba.Abilities.Osteo {
    [Serializable]
    public class RaiseDead : Ability {
        public int marrowMax = 400;
        public int marrow = 400;

        
        public RaiseDead(Player myPlayer) : base(myPlayer) {
            Name = "Raise Dead";
            Icon = ModContent.Request<Texture2D>("Textures/Osteo/OsteoAbilityOne").Value;
        }

        public override void OnCast() {
            int numSkeletons = marrow / 100;
            if (numSkeletons > 0) {
                for (int i = 0; i < numSkeletons; i++) {
                    Vector2 position = BaseAI.Trace(
                        new Vector2(User.Center.X + ((96) * User.direction) + (i * 32), User.Center.Y - 64),
                        new Vector2(User.Center.X, Main.bottomWorld), new Vector2(-1, -1), -1, false, true);

                    int npc = NPC.NewNPC((int) position.X, (int) position.Y, NPCType<OsteoSkeleton>());

                    Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = User.whoAmI;
                    Main.npc[npc].direction = User.direction;
                    (User.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Add(Main.npc[npc]);
                    
                    marrow -= 100;
                }

                cooldownTimer = 360;
            }
        }

        public override void AdditionalDrawing(SpriteBatch spriteBatch, UIAbilityIcon abilityIcon) {
            Vector2 iconPos = abilityIcon.GetDimensions().Position();
            Vector2 pos = new Vector2(iconPos.X + 2, iconPos.Y + abilityIcon.Height.Pixels - 12);
            Color color = Color.Aqua;
            Utils.DrawBorderString(spriteBatch, marrow + "/" + marrowMax, pos, color, 0.65f);
        }
    }
}*/