using System;

namespace TerrariaMoba.Enums {
    public enum Message : Byte {
        SyncExperience = 1,
        SyncPvpHit,
        SyncCharacter,
        SyncGameStart,
        SyncAbilities,
        SyncAbilityValues,
        SyncTalents,
        SyncJunglesWrathAdd
    }
}