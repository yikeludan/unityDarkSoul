using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Date/StateMachine/PlayerState/Land",fileName = "PlayerState_Land")]
public class PlayerState_Land : PlayState
{
    private float stiffTime = 0.2f;
    public override void Enter()
    {
       // Debug.Log("到地");
        base.Enter();
        this.player.SetVelocity(Vector3.zero);
    }

    public override void LogicUpdate()
    {
        if (this.playerInput.Jump)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_JumpUp));
        }

        if (this.aniStateDuration < this.stiffTime)
        {
            return;
        }


        if (this.playerInput.Move)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_Run));
        }
       // Debug.Log("this.isAnimationFinish = "+this.isAnimationFinish);
        if (this.isAnimationFinish)
        {
            this.playStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }
}
