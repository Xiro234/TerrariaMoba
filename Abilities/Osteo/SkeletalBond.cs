using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaMoba.Enums;

namespace TerrariaMoba.Abilities.Osteo {
    public class SkeletalBond: Ability {
        public SkeletalBond() : base("Skeletal Bond", 10, 0, AbilityType.Active) { }

        public override Texture2D Icon { get => ModContent.Request<Texture2D>("TerrariaMoba/Textures/Osteo/OsteoTrait").Value; }
    }
}

/*using System;
using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaMoba.Enums;
using TerrariaMoba.Players;

namespace TerrariaMoba.Abilities.Osteo {
    [Serializable]
    public class SkeletalBond : Ability {
        public SkeletalBond(Player myPlayer) : base(myPlayer) {
            AbilityType = Enums.AbilityType.Active;
            Name = "Skeletal Bond";
            IsActive = true;
            Icon = ModContent.Request<Texture2D>("Textures/Osteo/OsteoTrait").Value;
        }

        public override void Cast() {
            if ((User.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Count > 0) {
                float distance = Single.MaxValue;
                Player newTargetPlayer = null;
                foreach(Player targetPlayer in Main.Player){
                    if (targetPlayer.active && targetPlayer != null) {
                        if (targetPlayer.team != User.team && !targetPlayer.dead) {
                            float tempDistance = Vector2.Distance(targetPlayer.Center, User.Center);

                            if (tempDistance < distance) {
                                distance = tempDistance;
                                newTargetPlayer = targetPlayer;
                            }
                        }
                    }
                }

                if (newTargetPlayer != null) {
                    foreach (NPC npc in (User.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList) {
                        BaseAI.SetTarget(npc, newTargetPlayer.whoAmI);
                        npc.FaceTarget();
                    }
                }

                cooldownTimer = 120;
            }
        }
    }
}*/