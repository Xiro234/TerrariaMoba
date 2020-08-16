using Terraria;
using Terraria.ModLoader;
using TerrariaMoba;
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
            item.damage = 20;
            item.ranged = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("SylviaArrow");
            item.width = 20;
            item.height = 12;
            item.useTime = 20;
            item.UseSound = SoundID.Item5;
            item.shootSpeed = 10;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.knockBack = 0;
            item.value = 10000;
            item.rare = 2;
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