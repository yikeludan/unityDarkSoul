using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Date/StateMachine/PlayerState/JumpUp",fileName = "PlayerState_JumpUp")]
public class PlayerState_JumpUp : PlayState
{
    [SerializeField]float jumpForce = 7f;
    [SerializeField]float moveSpeed = 5f;

    public ParticleSystem jumpVFX;
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("跳起来");
        this.player.SetVelocityY(jumpForce);
        Instantiate(jumpVFX, this.player.transform.position, Quaternion.identity);
    }

    public override void LogicUpdate()
    {
        //Debug.Log("this.player.isFall = "  + this.player.isFall);
        if (this.playerInput.StopJump || this.player.isFall)
        {
           // Debug.Log("下落了");
            this.playStateMachine.SwitchState(typeof(PlayerState_JumpFall));
        }
    }

    public override void PhysicUpdate()
    {
        this.player.Move(moveSpeed);
    }
}
