using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.Interfaces;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.StatusEffects.Flibnob {
    public class TitaniumShellEffect : StatusEffect, IResetEffects, ITakePvpDamage {
        public override string DisplayName { get => "Titanium Shell"; }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        private int shellArmor;
        private int shellMagRes;
        private float moveSpeed;
        private float healDamage;
        private int mitigatedDamageTaken;

        public TitaniumShellEffect(int armor, int mr, float ms, float heal, int duration) : base(duration, true) {
            shellArmor = armor;
            shellMagRes = mr;
            moveSpeed = ms;
            healDamage = heal;
        }

        public void ResetEffects() {
            Statistics flibnob = User.GetModPlayer<MobaPlayer>().Hero.BaseStatistics;
            flibnob.PhysicalArmor += shellArmor; 
            flibnob.MagicalArmor += shellMagRes; 
            flibnob.MovementSpeed *= moveSpeed; 
        }

        public void TakePvpDamage(ref int physicalDamage, ref int magicalDamage, ref int trueDamage, ref int killer) {
            //TODO - Store mitigated damage correctly.
            mitigatedDamageTaken = 69;
        }

        public override void FallOff() {
            User.statLife += (int) (mitigatedDamageTaken * healDamage);
        }
    }
}