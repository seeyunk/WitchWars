using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables
{
    public static float BULLET_SPEED = 12.5f;
    public static float WITCH_SPEED = 0.10f;
    //public static float WITCH_SPEED = 0.0f;
    public static float WITCH_UP_VELOCITY = 0.025f;
    public static float WITCH_DOWN_VELOCITY = 0.005f;
    public static float BOOST_SPEED = 0.20f;
    public static float BOOST_TIME_LIMIT = 1.0f;
    
    public static float OBJECT_HEIGHT = 10.0f;

    public enum MoveState
    {
        RIGHT,
        LEFT,
        NEUTRAL
    }

    public enum FireState
    {
        ON,
        OFF
    }

    public enum BoostState
    {
        OVERHEAT,
        ON,
        OFF

    }
}

    

