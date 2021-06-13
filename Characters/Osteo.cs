using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using TerrariaMoba.Abilities.Sylvia;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.Characters {
    public class Osteo : Character {
        public Osteo(Player user) : base(user, new Statistics(1440f, 0f, 500f,
            0f, Resource.Mana, 75f, 1.5f, 9f), new EnsnaringVinesAbility()) { }

        public override string Name {
            get => "Osteo Prime";
        }
        
        public override Texture2D CharacterIcon {
            get => TerrariaMoba.Instance.GetTexture("Textures/Osteo/OsteoIcon");
        }

        public override bool IsMale { get => true; }
        public override int HairID { get => 15; }
        public override Color HairColor { get => Color.Black; }
        public override Color SkinColor { get => Color.WhiteSmoke; }
        public override Color EyeColor { get => Color.Red; }
        public override int PrimaryWeaponID { get => TerrariaMoba.Instance.ItemType("OsteoTome"); }
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

        public Osteo(Player player) : base(player) {
            skeleList = new List<NPC>();
        }

        public override void SetPlayer() {
            vanityHead.SetDefaults(ItemID.SkeletronPrimeMask);
            vanityBody.SetDefaults(3875);
            vanityLeg.SetDefaults(3876);
            primary.SetDefaults(TerrariaMoba.Instance.ItemType("OsteoTome"));

            player.Male = true;
            player.hair = 15;
            player.hairColor = new Color(52, 133, 34);
            player.skinColor = new Color(198,134,66);
            player.eyeColor = new Color(84,42,14);

            
        }

        public override void SetStats() {
            baseMaxLife = 1440;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = 500;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            baseArmor = 0;
            
            EAbility = new RaiseDead(player);
            QAbility = new LifedrainPulse(player);
            RAbility = new SoulSiphon(player);
            CAbility = new SkeletalBond(player);
            //abilities[2] = new SongOfTheDamned(player);
        }
        /*
        public override void PostUpdateEquips() {
            var mobaPlayer = player.GetModPlayer<MobaPlayer>();

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
            if (proj.type == TerrariaMoba.Instance.ProjectileType("LifedrainPulseThird")) {
                player.GetModPlayer<MobaPlayer>().HealMe(20, true);
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection) {
            if (proj.type == TerrariaMoba.Instance.ProjectileType("LifedrainPulseThird")) {
                player.GetModPlayer<MobaPlayer>().HealMe(20, true);
            }
        }
        #1#
        public override void LevelUp() {
            level += 1;
            player.GetModPlayer<MobaPlayer>().MarieStats.LevelUp();
            baseMaxLife = (int)player.GetModPlayer<MobaPlayer>().MarieStats.MaxHealth.Value;
            baseLifeRegen = (baseMaxLife * 0.125f) / 60;
            baseMaxResource = (int)player.GetModPlayer<MobaPlayer>().MarieStats.MaxResource.Value;
            baseResourceRegen = (baseMaxResource * 0.125f) / 30;
            //TalentSelect();
        }

        public override void HealMe(ref int amount) {
            foreach (Ability ability in abilities) {
                var raiseDead = ability as RaiseDead;
                if (raiseDead != null) {
                    raiseDead.marrow += amount / 10;
                    CombatText.NewText(player.Hitbox, Color.Gray, amount / 10, false);
                    
                    for (int i = 0; i < 10; i++) {
                        int dust = Dust.NewDust(player.position, player.width, player.height, 90, 0f, 0f, 0, Color.Gray,
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
                        TerrariaMoba.Instance.ProjectileType("OsteoSoul"), 0, 0, player.whoAmI);
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