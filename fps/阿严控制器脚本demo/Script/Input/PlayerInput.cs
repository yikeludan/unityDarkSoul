using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
     PlayerInputAction playerInputAction;

     Vector2 axes => this.playerInputAction.GamePlay.Axes.ReadValue<Vector2>();

     public bool Jump => this.playerInputAction.GamePlay.Jump.WasPressedThisFrame();

     public bool StopJump => this.playerInputAction.GamePlay.Jump.WasReleasedThisFrame();

     public bool Move => AxisX != 0;
     public float AxisX => axes.x;


     private void Awake()
    {
        this.playerInputAction = new PlayerInputAction();
    }

    public void EnableGamePlayInputs()
    {
        this.playerInputAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
