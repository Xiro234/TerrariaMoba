using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;
using TerrariaMoba.StatusEffects;
using TerrariaMoba.StatusEffects.Nocturne;

namespace TerrariaMoba.Abilities.Nocturne {
    public class UnrelentingOnslaught : Ability {
        public UnrelentingOnslaught() : base("Unrelenting Onslaught", 180, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("Textures/Blank").Value; }

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
    }
}