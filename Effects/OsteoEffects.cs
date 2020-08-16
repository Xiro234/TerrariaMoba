using System;
using Microsoft.Xna.Framework;

namespace TerrariaMoba.Effects {
    public class OsteoEffects {
        public static double GetSwordPosition(int timer) {
            double displace = 0;
            
            if (timer < 90) {
                displace = Math.Cos(MathHelper.ToRadians(timer) * 3) * 10; //Bobbing
            }
            else if (timer < 150) {
                displace = Math.Cos(MathHelper.ToRadians(90) * 3) * 10; //Previous position

                displace -= 40 * (1 - Math.Pow(Math.E, -0.1 * (timer - 90))); //Makes it raise in the air
            }
            else {
                displace = (Math.Cos(MathHelper.ToRadians(90) * 3) * 10) - (40 * (1 - Math.Pow(Math.E, -0.1 * (150 - 90)))); //Previous Position

                displace += Math.Pow(1.2, timer - 150); //Thrust down
            }
            
            return displace;
        }
    }
}