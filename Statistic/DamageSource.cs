using Terraria;

namespace TerrariaMoba.Statistic; 

public sealed class DamageSource {
    public readonly Entity source;

    public DamageSource(Entity src) {
        source = src;
    }
}