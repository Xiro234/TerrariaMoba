using Terraria.UI;

namespace TerrariaMoba.UI.Chastradamus; 

public class ChastradamusUIState: UIState {
    public static ChastradamusUIState Instance;

    public override void OnInitialize() {
        Instance = this;

        concoctionWheel = new ConcoctionWheel();
        Instance.Append(concoctionWheel);
        concoctionWheel.Hi
    }
}