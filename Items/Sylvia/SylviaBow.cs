using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using TerrariaMoba.Players;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Items.Sylvia {
    public class SylviaBow : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Sylvia's main weapon.");
            DisplayName.SetDefault("Luscious Longbow");
        }

        public override void SetDefaults() {
            item.damage = 75;
            item.ranged = true;
            item.shoot = mod.ProjectileType("SylviaArrow");
            item.width = 20;
            item.height = 12;
            item.useTime = 40;
            item.useAnimation = 40;
            item.UseSound = SoundID.Item5;
            item.shootSpeed = 9f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 0;
            item.value = 10000;
            item.rare = ItemRarityID.Green;
            item.autoReuse = false;
        }
        
        public override Vector2? HoldoutOffset() {
            return new Vector2(-5, 0);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
            var plr = player.GetModPlayer<MobaPlayer>();
            if (plr.CharacterPicked) {
                if (plr.MyCharacter.CharacterEnum == CharacterEnum.Sylvia) {
                    //Graceful Leap
                    if (plr.MyCharacter.talentArray[0, 1]) {
                        if (player.velocity.Y != 0) {
                            add += 0.08f;
                        }
                    }
                }
            }
        }
    }
}