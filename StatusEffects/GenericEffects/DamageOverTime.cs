using System.IO;
using Terraria.ModLoader;
using TerrariaMoba.Players;

namespace TerrariaMoba.StatusEffects.GenericEffects {
    public abstract class DamageOverTime : StatusEffect {
        private int physicalDamage;
        private int magicalDamage;
        private int trueDamage;
        private int timer;

        public DamageOverTime() { }
        public DamageOverTime(int p, int m, int t, int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) {
            physicalDamage = p;
            magicalDamage = m;
            trueDamage = t;
            timer = 0;
        }

        public override void WhileActive() {
            if(timer == 0) {
                timer = 60;
                User.GetModPlayer<MobaPlayer>().TakePvpDamage(physicalDamage, magicalDamage, trueDamage, ApplicantID, true);
            } else {
                timer--;
            }

            base.WhileActive();
        }

        public override void SendEffectElements(ModPacket packet) {
            packet.Write(physicalDamage);
            packet.Write(magicalDamage);
            packet.Write(trueDamage);
            packet.Write(timer);
            base.SendEffectElements(packet);
        }

        public override void ReceiveEffectElements(BinaryReader reader) {
            physicalDamage = reader.ReadInt32();
            magicalDamage = reader.ReadInt32();
            trueDamage = reader.ReadInt32();
            timer = reader.ReadInt32();
            base.ReceiveEffectElements(reader);
        }
    }
}
