using Terraria;

namespace TerrariaMoba.Characters {
    public abstract class Character {
        public abstract void AbilityOne(Player player);
        public abstract void AbilityOneAnimation(Player player, ref int animCounter);
        public abstract void AbilityTwo(Player player);

        public abstract void LevelUp();
    }
}