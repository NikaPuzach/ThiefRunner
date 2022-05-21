using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NorskaLib.GUI;

public class GameplayScreen : NorskaLib.GUI.Screen
{
    [SerializeField] FloatingJoystick joystick;
    public FloatingJoystick Joystick => joystick;
}
