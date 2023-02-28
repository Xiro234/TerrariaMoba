using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using TerrariaMoba.Projectiles.Osteo;
using TerrariaMoba.Projectiles;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.StatusEffects.Osteo {
    public class FungalArmorEffect : StatusEffect {
        public override string DisplayName { get => "Fungal Armor"; }
        public override Texture2D Icon { get { return ModContent.Request<Texture2D>("Textures/Blank").Value; } }

        private float AttackDamageModifier;
        private int SporeDamage;
        private int SporeDelay;
        private int SporeLifetime;
        private int Timer;

        public FungalArmorEffect() { }
        public FungalArmorEffect(float dmgMod, int sporeDmg, int sporeDel, int sporeLife, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { 
            AttackDamageModifier = dmgMod;
            SporeDamage = sporeDmg;
            SporeDelay = sporeDel;
            SporeLifetime = sporeLife;
            Timer = 0;
        }

        public override void WhileActive() {
            if(Timer > 0) {
                Timer--;
            }

            if(Timer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == ApplicantID) {
                    for (int i = 0; i < 8; i++) {
                        double x = Math.Cos(((Math.PI / 180) * ((360f / 8) * i)));
                        double y = Math.Sin(((Math.PI / 180) * ((360f / 8) * i)));
                        Vector2 direction = new Vector2((float)x, (float)y);
                        Vector2 position = User.Center + direction * 16;
                        Vector2 velocity = direction * 1.5f;
                        int index = Projectile.NewProjectile(new EntitySource_StatusEffect(User, this),
                            position, velocity, ModContent.ProjectileType<FungalSpore>(), 1, 0, ApplicantID);
                        Projectile proj = Main.projectile[index];
                        TerrariaMobaUtils.SetProjectileDamage(proj, MagicalDamage: SporeDamage);

                        
                        FungalSpore spore = proj.ModProjectile as FungalSpore;
                        if (spore != null) {
                            spore.SporeLifetime = SporeLifetime;
                        }
                    }
                }
                Timer = SporeDelay;
            }
            base.WhileActive();
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(AttackDamageModifier);
            packet.Write(SporeDamage);
            packet.Write(SporeDelay);
            packet.Write(Timer);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            AttackDamageModifier = reader.ReadSingle();
            SporeDamage = reader.ReadInt32();
            SporeDelay = reader.ReadInt32();
            Timer = reader.ReadInt32();
            base.ReceiveEffectElements(reader);
        }
    }
}
