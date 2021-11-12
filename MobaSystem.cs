using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using TerrariaMoba.UI;

namespace TerrariaMoba {
    public class MobaSystem : ModSystem {
        public static bool MatchInProgress = false;
        
        static internal MobaBar MobaBar;
        static internal CharacterSelect CharacterSelect;
        static internal DebugWindow DebugWindow;
        static internal UserInterface BarInterface;
        static internal UserInterface SelectInterface;
        static internal UserInterface DebugInterface;

        public override void NetSend(BinaryWriter writer) {
            writer.Write(MatchInProgress);
        }

        public override void NetReceive(BinaryReader reader) {
            MatchInProgress = reader.ReadBoolean();
        }

        public static void StartGame() {
            MatchInProgress = true;
        }

        public override void Load() {
            if (!Main.dedServ) {
                MobaBar = new MobaBar();
                BarInterface = new UserInterface();
                BarInterface.SetState(null);
				
                CharacterSelect = new CharacterSelect();
                SelectInterface = new UserInterface();
                SelectInterface.SetState(null);
                
                DebugWindow = new DebugWindow();
                DebugInterface = new UserInterface();
                DebugInterface.SetState(null);
            }
        }

        public override void Unload() {
            MobaBar.UnLoad();
            MobaBar = null;
            DebugWindow.UnLoad();
            DebugWindow = null;
        }

        public override void UpdateUI(GameTime gameTime) {
            BarInterface?.Update(gameTime);
            SelectInterface?.Update(gameTime);
            DebugInterface?.Update(gameTime);
        }
		
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int LayerIndex;
            //LayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            //layers.RemoveAt(LayerIndex);
            LayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Death Text"));
            layers.RemoveAt(LayerIndex);

            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1) {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "TerrariaMoba: A Description",
                    delegate {
                        BarInterface.Draw(Main.spriteBatch, new GameTime());
                        SelectInterface.Draw(Main.spriteBatch, new GameTime());
                        DebugInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
        
        static internal void ShowBar() {
            BarInterface?.SetState(MobaBar);
            DebugInterface?.SetState(DebugWindow);
        }

        static internal void HideBar() {
            BarInterface?.SetState(null);
        }

        static internal void ShowSelect() {
            SelectInterface?.SetState(CharacterSelect);
        }

        static internal void HideSelect() {
            SelectInterface?.SetState(null);
        }
    }
}