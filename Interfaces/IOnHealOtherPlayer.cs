using Terraria;

namespace TerrariaMoba.Interfaces; 

public interface IOnHealOtherPlayer {
    void OnHealOtherPlayer(Player target, ref int amount, ref bool doText);
}