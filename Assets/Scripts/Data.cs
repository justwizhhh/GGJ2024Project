using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralData
{
    public static class PlayerData
    {
        public const int PLAYER_STARTING_HEALTH = 100;
    }
}

namespace Enumerations
{
    public enum MusicType
    {
        MAIN_MENU = 0,
        GAME_MUSIC = 1,
        GAME_OVER = 2,
        CREDITS = 3
    }

    public enum ObjectType
    {
        NEUTRAL     = 0,
        HARMFUL     = 1,
        BENEFICIAL  = 2
    }

    public enum HarmfulObjectType
    {
        TOMATO = 0,
        MUG_OF_ALE = 1,
        DAGGER = 2
    }

    public enum BeneficialObjectType
    {
        ROSE = 0,
        PURSE = 1,
        DIAMOND = 2
    }
}

