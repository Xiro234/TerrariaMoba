using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Items.Marie {
    public class MarieStaff : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Marie's main weapon.\nA staff imbued with the waters of Lacusia.\nIf you look close enough at the crystal, you can see crashing tides.");
            DisplayName.SetDefault("Staff of Tides");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults() {
            item.width = 34;
            item.height = 34;
            item.damage = 69;
            item.knockBack = 0;
            item.ranged = true;
            item.useTime = 54;
            item.useAnimation = 54;
            item.shootSpeed = 7.33f;
            item.shoot = mod.ProjectileType("SoTBolt");
            item.UseSound = SoundID.Item43;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.value = 10000;
            item.rare = ItemRarityID.Cyan;
            item.autoReuse = false;
        }
        
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat) {
            var plr = player.GetModPlayer<MobaPlayer>();
            if (plr.CharacterPicked) {
                if (plr.selectedCharacter == CharacterIdentity.Marie) {
                    if (plr.MyCharacter.Level > 1) {
                        add += (float)Math.Pow(1.04f, plr.MyCharacter.Level - 1) - 1;
                    }
                }
            }
        }
    }
}