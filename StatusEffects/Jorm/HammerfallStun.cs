using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects.Jorm; 

public class HammerfallStun : Stun {
    public override string DisplayName { get => "Hammerfall stun."; }
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }
    
    public HammerfallStun() { }
    public HammerfallStun(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
}