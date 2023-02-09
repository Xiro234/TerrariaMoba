using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.NPCs;
using TerrariaMoba.Players;
using TerrariaMoba.Projectiles;

namespace TerrariaMoba.Abilities.Jorm {
    public class VexillumImmortalis : Ability {
        public VexillumImmortalis(Player player) : base(player, "Vexillum Immortalis", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Jorm/JormUltimateTwo").Value; }

        public const float BANNER_BUFF_RANGE = 25f;
        
        public override void OnCast() {
            PaladinsResolve pr = User.GetModPlayer<MobaPlayer>().Hero.Trait as PaladinsResolve;
            if (pr != null) {
                pr.AddStack();
            }

            int npc = NPC.NewNPC(new EntitySource_Ability(User, this), (int) User.Center.X, (int) User.Center.Y, ModContent.NPCType<JormBanner>(), 0, BANNER_BUFF_RANGE);
            Main.npc[npc].GetGlobalNPC<MobaGlobalNPC>().owner = User.whoAmI;
            Main.npc[npc].direction = User.direction;
        }
    }
}