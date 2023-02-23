using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrariaMoba.StatusEffects.GenericEffects;

namespace TerrariaMoba.StatusEffects.Flibnob; 

public class EarthsplitterStun : Stun {
    public override string DisplayName { get => "Earthsplitter stun."; }
    public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Blank").Value; }
    
    public EarthsplitterStun() { }
    public EarthsplitterStun(int duration, bool canBeCleansed, int applierId) : base(duration, canBeCleansed, applierId) { }
}