using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Items.Osteo {
    public class OsteoTome : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Osteo's main weapon.\nA very elegant looking book on the outside.\nAttempting to read it is highly inadvisable.");
            DisplayName.SetDefault("Forbidden Tome");
        }

        public override void SetDefaults() {
            item.width = 20;
            item.height = 12;
            item.damage = 83;
            item.ranged = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("OsteoSkull");
            item.UseSound = SoundID.Item104.WithVolume(0.3f).WithPitchVariance(0.5f);
            item.useTime = 57;
            item.useAnimation = 57;
            item.shootSpeed = 7.66f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 0;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = false;
        }
        
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
            var plr = player.GetModPlayer<MobaPlayer>();
            if (plr.CharacterPicked) {
                if (plr.MyCharacter.CharacterEnum == CharacterEnum.Osteo) {
                    if (plr.MyCharacter.level > 1) {
                        add += (float)Math.Pow(1.04f, plr.MyCharacter.level - 1) - 1;
                    }
                }
            }
        }
    }
}