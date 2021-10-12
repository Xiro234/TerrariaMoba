using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Marie {
    public class BlessingOfTheGoddess : Ability {
        public BlessingOfTheGoddess() : base("Blessing of the Goddess", 60, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Marie/MarieUltimateOne").Value; } //not trait anymore just texure name
    }
}
/*using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TerrariaMoba.Abilities.Marie {
    [Serializable]
    public class FountainOfTheGoddess : Ability {
        public FountainOfTheGoddess(Player myPlayer) : base(myPlayer) {
            Name = "Fountain of the Goddess";
            Icon = ModContent.Request<Texture2D>("Textures/Marie/MarieUltimateOne").Value;
        }
        
        public override void Cast() {
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Projectile.NewProjectile(User.Center, Vector2.Zero,
                    ModContent.ProjectileType<FountainOfLacusia"), 0, 0, User.whoAmI, 29f);
            }

            cooldownTimer = 20 * 60;
        }
    }
}*/