using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Date/StateMachine/PlayerState/Run",fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayState
{
    [SerializeField] float speed = 5f;
    public override void Enter()
    {
       // this.animator.Play("Run");
       base.Enter();
    }

    /*public override void LogicUpdate()
    {
        if (!(Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed))
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }*/

    public override void LogicUpdate()
    {
        if (!this.playerInput.Move)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_Idle));
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

    public override void PhysicUpdate()
    {
        this.player.Move(speed);
    }
}
