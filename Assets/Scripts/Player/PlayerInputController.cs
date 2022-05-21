using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NorskaLib.GUI;
using System;

public class PlayerInputController : MonoBehaviour
{
    SceneController SceneController => SceneController.Instance;
    World World => World.Instance;

	[SerializeField] Unit unit;
    FloatingJoystick floatingJoystick;

    private void Start()
    {
        NorskaLib.GUI.Events.onScreenShown += OnScreemShown;
        NorskaLib.GUI.Events.onScreenDestroyed += OnScreenDestroyed;
    }

    private void OnDestroy()
    {
        NorskaLib.GUI.Events.onScreenShown -= OnScreemShown;
        NorskaLib.GUI.Events.onScreenDestroyed -= OnScreenDestroyed;

    }

    private void Update()
    {
        if(SceneController.Phase == GamePhases.Gameplay)
        {
            var direction2D = floatingJoystick.Direction;
            var direction3DRaw = new Vector3(direction2D.x, 0, 1).normalized;
            var normal = World.GetNormal(unit.transform.position);
            var distance = normal.magnitude;

            if (distance < World.radius)
            {
                unit.MovementController.desiredDirection = direction3DRaw;
            }
            else
            {
                var dotProduct = Vector3.Dot(normal.normalized, direction3DRaw);

                if(dotProduct > 0)
                {
                    unit.MovementController.desiredDirection = new Vector3(0, 0, 1);
                }
                else
                {
                    unit.MovementController.desiredDirection = direction3DRaw;
                }
            }
        }
        else
        {
            unit.MovementController.desiredDirection = Vector3.zero;

        }
    }

    private void OnScreemShown(NorskaLib.GUI.Screen screen)
    {
        if(screen is GameplayScreen gameplayScreen)
        {
            floatingJoystick = gameplayScreen.Joystick;
        }
    }

    private void OnScreenDestroyed(NorskaLib.GUI.Screen screen)
    {
        if(screen is GameplayScreen gameplayScreen)
        {
            floatingJoystick = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (World == null)
            return;

        Gizmos.color = Color.cyan;
        var a =  World.Project(unit.transform.position);
        Gizmos.DrawLine(unit.transform.position, a);
    }
}