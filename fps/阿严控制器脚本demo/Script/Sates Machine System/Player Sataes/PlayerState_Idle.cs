using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Date/StateMachine/PlayerState/Idle",fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayState
{
    public override void Enter()
    {
        //this.animator.Play("Idle");
        base.Enter();
        this.player.SetVelocityX(0f);
    }

    /*public override void LogicUpdate()
    {
        if (Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_Run));
        }
    }*/

    public override void LogicUpdate()
    {
        if (this.playerInput.Move)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_Run));
        }
        if (this.playerInput.Jump)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }
        if (!this.player.isGrounded)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_JumpFall));
        }
    }
}
