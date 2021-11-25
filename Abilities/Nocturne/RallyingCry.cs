using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Projectiles;
using TerrariaMoba.Projectiles.Nocturne;

namespace TerrariaMoba.Abilities.Nocturne {
    public class RallyingCry : Ability {
        public RallyingCry(Player player) : base(player, "Rallying Cry", 60, 0, AbilityType.Active) { }
        
        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Nocturne/NocturneAbilityThree").Value; }
        
        //TODO - Globally buffs allies depending on which weapon nocturne is holding: melee = 25% status resistance (reduces duration of negative effects) / ranged = bonus atkspd

        public override void OnCast() {
            /*
             * if nocturnes primary is sword
             * for all players on nocturnes team that are alive including himself
             * grant effect that increases status resistance
             *
             * if nocturnes primary is throwing spear
             * for all players on nocturnes team that are alive including himself
             * grant effect that increases attack speed
             */
        }

        /*
        public const float DASH_X_VELOCITY = 8f;
        public const int WAIT_TIME = 60;

        public Projectile dash = null;
        public int timer;
        
        public override void OnCast() {
            IsActive = true;
            timer = WAIT_TIME;
            
            if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                Vector2 position = User.Center;
                position.Y -= 8f; // ups height of proj to avoid collision
                int direction = User.direction;

                Vector2 velocity = new Vector2(direction * DASH_X_VELOCITY, 0f);

                dash = Main.projectile[Projectile.NewProjectile(new ProjectileSource_Ability(User, this), position, velocity,
                    ModContent.ProjectileType<NocturneDash>(),
                    0, 0, User.whoAmI)];
            }
        }

        public override void WhileActive() {
            timer--;
            
            if (timer > 0) {
                User.controlLeft = false;
                User.controlRight = false;
                User.controlJump = false;
                User.controlUp = false;
            }

            if (timer == 0 || !dash.active) {
                User.controlLeft = true;
                User.controlRight = true;
                User.controlJump = true;
                User.controlUp = true;
                
                User.position = dash.position;
                NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, Main.myPlayer);
                dash.Kill();
                
                TimeOut();
            }
        }

        public override void TimeOut() {
            timer = 0;
            IsActive = false;
        }
        */
    }
}