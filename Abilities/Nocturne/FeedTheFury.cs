using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Nocturne;

namespace TerrariaMoba.Abilities.Nocturne {
    public class FeedTheFury : Ability {
        public FeedTheFury(Player player) : base(player, "Feed the Fury", 180, 0, AbilityType.Passive) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

        public const float DEATH_RANGE = 25f;
        public const int COOLDOWN_REDUC = 1;
        
        public override void WhileActive() {
            //TODO - Any character that dies near Nocturne reduces his non-ult cooldowns. Kills reduce Nocturnes cooldowns further.
            /*
             * if any player dies within DEATH_RANGE
             * go through all of nocturnes abilities
             * foreach ability on cooldown
             * reduce cooldown by COOLDOWN_REDUC
             *
             * if user kills enemy
             * go through all of nocturnes abilities
             * foreach ability on cooldown
             * reduce cooldown by COOLDOWN_REDUC * 2
             */
        }

        /*
        public const int ATKBUFF_REFRESH_TIME = 240;
        public const int ATKBUFF_DURATION = 60;

        public StatusEffect uoEffect;
        public int timer;

        public override void OnCast() {
            IsActive = true;
            timer = ATKBUFF_REFRESH_TIME;
        }

        public override void WhileActive() {
            //if(match start is true) {}
            uoEffect = new UnrelentingOnslaughtEffect(ATKBUFF_DURATION, false);
            
            if (timer == 0) {
                if (Main.netMode != NetmodeID.Server && Main.myPlayer == User.whoAmI) {
                    StatusEffectManager.AddEffect(User, uoEffect);
                }
                timer = ATKBUFF_REFRESH_TIME;
            }

            if (StatusEffectManager.HasEffect(User, uoEffect)) {
                Main.NewText("User has Unrelenting Onslaught Effect; Timer: " + timer);
            } else {
                Main.NewText("Timer: " + timer);
                timer--;
            } 
        }
        */
    }
}