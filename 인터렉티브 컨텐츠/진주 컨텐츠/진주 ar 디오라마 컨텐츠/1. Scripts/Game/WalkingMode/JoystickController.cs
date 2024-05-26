using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [SerializeField] WalkingModeController _WalkingModeController;

    [Header("MoveSound")]
    [SerializeField] GameObject[] SoundIncludeObjects;

    private void OnEnable()
    {
        InitJoystick();
    }

    private void InitJoystick()
    {
        foreach(var obj in SoundIncludeObjects)
        {
            obj.SetActive(false);
        }
        SoundIncludeObjects[_WalkingModeController.StoryIndex].SetActive(true);
    }
}
