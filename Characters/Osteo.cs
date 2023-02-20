using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Abilities;
using TerrariaMoba.Abilities.Osteo;
using TerrariaMoba.Items.Osteo;
using TerrariaMoba.Statistic;
using static TerrariaMoba.Statistic.AttributeType;

namespace TerrariaMoba.Characters {
    public class Osteo : Character {
        public Osteo() { }
        
        public Osteo(Player user) : base(user) { }
        
        protected override Dictionary<AttributeType, Func<float>> BaseAttributesFactory() {
            return new Dictionary<AttributeType, Func<float>>() {
                { MAX_HEALTH, () => 1440f },
                { HEALTH_REGEN, () => 3.0f },
                { MAX_MANA, () => 500f },
                { MANA_REGEN, () => 2.1f },
                { PHYSICAL_ARMOR, () => 0f },
                { MAGICAL_ARMOR, () => 0f },
                { ATTACK_DAMAGE, () => 83f },
                { ATTACK_SPEED, () => 0.00f },
                { ATTACK_VELOCITY, () => 7.66f },
                { MOVEMENT_SPEED, () => 1f },
                { JUMP_SPEED, () => 1f },
                { HEALING_EFFECTIVENESS, () => 0.5f },
                { STATUS_RESISTANCE, () => 0f }
            };
        }
        
        protected override Ability[] BaseSkillsFactory() {
            return new Ability[] {
                new LifedrainPulse(User),
                new FungalArmor(User),
                new EyeOfFright(User), 
                new RiteOfCursedBlades(User), 
                new Mucormycosis(User),
                new Reincarnation(User)
            };
        }

        public override string Name {
            get => "Osteo Prime";
        }
        
        public override Asset<Texture2D> CharacterIcon {
            get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoIcon", AssetRequestMode.ImmediateLoad);
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 15; }
        public override Color HairColor { get => Color.Black; }
        public override Color SkinColor { get => Color.WhiteSmoke; }
        public override Color EyeColor { get => Color.Red; }
        public override int PrimaryWeaponID { get => ModContent.ItemType<OsteoTome>(); }
        public override int HeadVanityID { get => ItemID.SkeletronPrimeMask; }
        public override int BodyVanityID { get => ItemID.ApprenticeAltShirt; }
        public override int LegVanityID { get => ItemID.ApprenticeAltPants; }
    }
}

/*
namespace TerrariaMoba.Characters {
    public class Osteo : Character {
        public override string FullName {
            get => "Osteo Prime, Last Necromancer of the Mudpits";
        }
        
        public List<NPC> skeleList;

        public Osteo(Player Player) : base(Player) {
            skeleList = new List<NPC>();
        }

        public override void SetPlayer() {
            vanityHead.SetDefaults(ItemID.SkeletronPrimeMask);
            vanityBody.SetDefaults(3875);
            vanityLeg.SetDefaults(3876);
            primary.SetDefaults(ModContent.ItemType<OsteoTome"));

            Player.Male = true;
            Player.hair = 15;
            Player.hairColor = new Color(52, 133, 34);
            Player.skinColor = new Color(198,134,66);
            Player.eyeColor = new Color(84,42,14);

            
        }

        public override void SetStats() {
            baseMaxLife = 1440;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = 500;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            baseArmor = 0;
            
            EAbility = new RaiseDead(Player);
            QAbility = new LifedrainPulse(Player);
            RAbility = new SoulSiphon(Player);
            CAbility = new SkeletalBond(Player);
            //abilities[2] = new SongOfTheDamned(Player);
        }
        /*
        public override void PostUpdateEquips() {
            var mobaPlayer = Player.GetModPlayer<MobaPlayer>();

            foreach (Ability ability in abilities) {
                var raiseDead = ability as RaiseDead;
                if (raiseDead != null) {
                    if (mobaPlayer.lifeRegenTimer == 30) {
                        raiseDead.marrow += (int) mobaPlayer.lifeRegen;
                        if (raiseDead.marrow > raiseDead.marrowMax) {
                            raiseDead.marrow = raiseDead.marrowMax;
                        }
                    }

                    mobaPlayer.lifeRegen = 0f;

                    for (int i = skeleList.Count - 1; i >= 0; i--) {
                        NPC npc = skeleList[i];
                        if (!npc.active || npc == null) {
                            skeleList.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit) {
            if (proj.type == ModContent.ProjectileType<LifedrainPulseThird")) {
                Player.GetModPlayer<MobaPlayer>().HealMe(20, true);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection) {
            if (proj.type == ModContent.ProjectileType<LifedrainPulseThird")) {
                Player.GetModPlayer<MobaPlayer>().HealMe(20, true);
            }
        }
        #1#
        public override void LevelUp() {
            level += 1;
            Player.GetModPlayer<MobaPlayer>().MarieStats.LevelUp();
            baseMaxLife = (int)Player.GetModPlayer<MobaPlayer>().MarieStats.MaxHealth.Value;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = (int)Player.GetModPlayer<MobaPlayer>().MarieStats.MaxResource.Value;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            //TalentSelect();
        }

        public override void HealMe(ref int amount) {
            foreach (Ability ability in abilities) {
                var raiseDead = ability as RaiseDead;
                if (raiseDead != null) {
                    raiseDead.marrow += amount / 10;
                    CombatText.NewText(Player.Hitbox, Color.Gray, amount / 10, false);
                    
                    for (int i = 0; i < 10; i++) {
                        int dust = Dust.NewDust(Player.position, Player.width, Player.height, 90, 0f, 0f, 0, Color.Gray,
                            1f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].noLight = true;
                    }

                    if (raiseDead.marrow > raiseDead.marrowMax) {
                        raiseDead.marrow = raiseDead.marrowMax;
                    }

                    amount = (int)(amount * (2f / 3f));
                }
            }
        }

        public override void TeamSlayEffect(Player deadPlayer) {
            foreach (Ability ability in abilities) {
                var soulSiphon = ability as SoulSiphon;
                if (soulSiphon != null) {
                    Projectile.NewProjectile(deadPlayer.position, Vector2.One,
                        ModContent.ProjectileType<OsteoSoul"), 0, 0, Player.whoAmI);
                }
            }
        }

        public override void ReadCharacter(BinaryReader reader) {
            base.ReadCharacter(reader);
        }

        public override void WriteCharacter(BinaryWriter writer) {
            base.WriteCharacter(writer);
        }
    }
}*/