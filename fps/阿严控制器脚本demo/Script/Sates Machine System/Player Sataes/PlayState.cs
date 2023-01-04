using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState :ScriptableObject,IState
{ 
    [SerializeField]string stateName;
    [SerializeField,Range(0,1)]float transtadection = 0.1f;
    private int stateHash;
    protected Animator animator;
    private float aniStateStartTime;

    protected PlayerStateMachine playStateMachine;

    protected PlayerInput playerInput;

    protected PlayerController player;

    protected float aniStateDuration => Time.time - aniStateStartTime;

    protected bool isAnimationFinish => aniStateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;

    public void Initialize(Animator animator,PlayerInput playerInput,PlayerController player,PlayerStateMachine playStateMachine)
    {
        this.animator = animator;
        this.player = player;
        this.playerInput = playerInput;
        this.playStateMachine = playStateMachine;
    }

    private void OnEnable()
    {
        this.stateHash = Animator.StringToHash(this.stateName);
    }

    public virtual void Enter()
    {
        this.animator.CrossFade(this.stateHash,this.transtadection);
        this.aniStateStartTime = Time.time;
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicUpdate()
    {
    }
}
