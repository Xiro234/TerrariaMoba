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
            Icon = TerrariaMoba.Instance.GetTexture("Textures/Osteo/OsteoTrait");
        }

        public override void Cast() {
            if ((User.GetModPlayer<MobaPlayer>().MyCharacter as Characters.Osteo).skeleList.Count > 0) {
                float distance = Single.MaxValue;
                Player newTargetPlayer = null;
                foreach(Player targetPlayer in Main.player){
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