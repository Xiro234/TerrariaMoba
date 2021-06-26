using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaMoba.Enums;
using TerrariaMoba.NPCs;

namespace TerrariaMoba.Abilities.Jorm {
    public class VexillumImmortalis : Ability {
        public VexillumImmortalis() : base("Vexillum Immortalis", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => TerrariaMoba.Instance.GetTexture("Textures/Blank"); }

        public const float BANNER_BUFF_RANGE = 25f;
        
        public override void OnCast() {
            int npc = NPC.NewNPC((int) User.Center.X, (int) User.Center.Y, TerrariaMoba.Instance.NPCType("JormBanner"), 0, BANNER_BUFF_RANGE);
            
            Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = User.whoAmI;
            Main.npc[npc].direction = User.direction;
        }
    }
}