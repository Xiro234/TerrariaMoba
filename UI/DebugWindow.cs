using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariaMoba.Players;
using TerrariaMoba.Statistic;

namespace TerrariaMoba.UI {
    public class DebugWindow :  UIState {
        private static UIPanel debugPanel;
        private static UIText HPt;
        private static UIText HPRt;
        private static UIText MPt;
        private static UIText MPRt;
        private static UIText ARMt;
        private static UIText MRt;
        private static UIText ADt;
        private static UIText ASt;
        private static UIText AVt;
        private static UIText MSt;
        private static UIText JSt;

        public override void OnInitialize() {
            debugPanel = new UIPanel();
            debugPanel.Width.Set(200, 0);
            debugPanel.Height.Set(460, 0);
            debugPanel.VAlign = 0.50f;
            debugPanel.HAlign = 0.05f;
            Append(debugPanel);

            HPt = new UIText("Max Health: ");
            HPt.Top.Set(10, 0);
            debugPanel.Append(HPt);
            
            HPRt = new UIText("HP Regen: ");
            HPRt.Top.Set(50, 0);
            debugPanel.Append(HPRt);
            
            MPt = new UIText("Max Mana: ");
            MPt.Top.Set(90, 0);
            debugPanel.Append(MPt);
            
            MPRt = new UIText("Mana Regen: ");
            MPRt.Top.Set(130, 0);
            debugPanel.Append(MPRt);
            
            ARMt = new UIText("Phys. Resist: ");
            ARMt.Top.Set(170, 0);
            debugPanel.Append(ARMt);
            
            MRt = new UIText("Mag. Resist: ");
            MRt.Top.Set(210, 0);
            debugPanel.Append(MRt);
            
            ADt = new UIText("Attack Damage: ");
            ADt.Top.Set(250, 0);
            debugPanel.Append(ADt);
            
            ASt = new UIText("Attack Speed: ");
            ASt.Top.Set(290, 0);
            debugPanel.Append(ASt);
            
            AVt = new UIText("Attack Velocity: ");
            AVt.Top.Set(330, 0);
            debugPanel.Append(AVt);
            
            MSt = new UIText("Move Speed: ");
            MSt.Top.Set(370, 0);
            debugPanel.Append(MSt);
            
            JSt = new UIText("Jump Speed: ");
            JSt.Top.Set(410, 0);
            debugPanel.Append(JSt);
        }
        
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            
            if (debugPanel.ContainsPoint(Main.MouseScreen)) {
                Main.LocalPlayer.mouseInterface = true;
            }
            
            var plr = Main.LocalPlayer.GetModPlayer<MobaPlayer>();
            HPt.SetText("Max Health: " + plr.GetCurrentAttributeValue(AttributeType.MAX_HEALTH));
            HPRt.SetText("HP Regen: " + plr.GetCurrentAttributeValue(AttributeType.HEALTH_REGEN));
            MPt.SetText("Max Mana: " + plr.GetCurrentAttributeValue(AttributeType.MAX_MANA));
            MPRt.SetText("Mana Regen: " + plr.GetCurrentAttributeValue(AttributeType.MANA_REGEN));
            ARMt.SetText("Phys. Resist: " + plr.GetCurrentAttributeValue(AttributeType.PHYSICAL_ARMOR));
            MRt.SetText("Mag. Resist: " + plr.GetCurrentAttributeValue(AttributeType.MAGICAL_ARMOR));
            ADt.SetText("Attack Damage: " + plr.GetCurrentAttributeValue(AttributeType.ATTACK_DAMAGE));
            ASt.SetText("Attack Speed: " + plr.GetCurrentAttributeValue(AttributeType.ATTACK_SPEED));
            AVt.SetText("Attack Velocity: " + plr.GetCurrentAttributeValue(AttributeType.ATTACK_VELOCITY)); 
            MSt.SetText("Movement Speed: " + plr.GetCurrentAttributeValue(AttributeType.MOVEMENT_SPEED));
            JSt.SetText("Jump Speed: " + plr.GetCurrentAttributeValue(AttributeType.JUMP_SPEED));
        }
        
        public void UnLoad() {
            debugPanel = null;
            HPt = null;
            HPRt = null;
            MPt = null;
            MPRt = null;
            ARMt = null;
            MRt = null;
            ADt = null;
            ASt = null;
            AVt = null; 
            MSt = null;
            JSt = null;
        }
    }
}