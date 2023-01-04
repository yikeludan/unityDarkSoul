using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Date/StateMachine/PlayerState/JumpFall",fileName = "PlayerState_JumpFall")]
public class PlayerState_JumpFall : PlayState
{
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField]float moveSpeed = 5f;
    public override void LogicUpdate()
    {

        if (this.player.isGrounded)
        {
            //Debug.Log("到底了吗");
            this.playStateMachine.SwitchState(typeof(PlayerState_Land));
        }
        if (this.playerInput.Jump)
        {
            Debug.Log("this.player.CanAirJump = "+this.player.CanAirJump);
            if (this.player.CanAirJump)
            {
                this.playStateMachine.SwitchState(typeof(PlayerState_AirJump));
            }

        }
    }

    public override void PhysicUpdate()
    {
        this.player.Move(moveSpeed);
        this.player.SetVelocityY(speedCurve.Evaluate(this.aniStateDuration));
    }
}
