using Terraria.ModLoader;

namespace TerrariaMoba.Projectiles {
    public class DamageTypeGlobalProj : GlobalProjectile {
        public int PhysicalDamage { get; set; }
        public int MagicalDamage { get; set; }
        public int TrueDamage { get; set; }

        public int TotalPremitigated => PhysicalDamage + MagicalDamage + TrueDamage;

        public override bool InstancePerEntity { get => true; }
    }
}