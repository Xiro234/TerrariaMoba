namespace TerrariaMoba.Statistic {
    public class Statistics {
        public float MaxHealth { get; set; }
        public float HealthRegen { get; set; }
        
        public float MaxResource { get; set; }
        public float ResourceRegen { get; set; }
        public Resource ResourceType { get; set; }
        
        public float PhysicalArmor { get; set; }
        public float MagicalArmor { get; set; }
        
        public float AttackDamage { get; set; }
        public float AttackSpeed { get; set; }
        public float AttackVelocity { get; set; }
        
        public float MovementSpeed { get; set; }
        public float JumpSpeed { get; set; }

        public Statistics(float maxHealth, float healthRegen, float maxResource, float resourceRegen, Resource resourceType, 
            float physicalArmor, float magicalArmor, float attackDamage, float attackSpeed, float attackVelocity) {
            MaxHealth = maxHealth;
            HealthRegen = healthRegen;
            MaxResource = maxResource;
            ResourceRegen = resourceRegen;
            ResourceType = resourceType;
            PhysicalArmor = physicalArmor;
            MagicalArmor = magicalArmor;
            AttackDamage = attackDamage;
            AttackSpeed = attackSpeed;
            AttackVelocity = attackVelocity;
        }

        public Statistics(Resource resourceType) {
            ResourceType = resourceType;
        }

        public Statistics() {
            ResetStats();
        }

        public void ResetStats() {
            MaxHealth = 0;
            HealthRegen = 0;
            MaxResource = 0;
            ResourceRegen = 0;
            PhysicalArmor = 0;
            MagicalArmor = 0;
            AttackDamage = 0;
            AttackSpeed = 0;
            AttackVelocity = 0;
        }
    }
}