using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPlayerActionHandler : IEventSystemHandler
{
    void TurnLeft();
    void TurnRight();
    void TurnNeutral();
    void FireOn();
    void FireOff();
    void BoostOn();
    void BoostOff();
}