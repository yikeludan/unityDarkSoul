using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    Animator animator;

    /*public PlayerState_Idle idleState;

    public PlayerState_Run RunState;*/

    [SerializeField] PlayState[] states;

    PlayerInput playerInput;

    PlayerController player;
    private void Awake()
    {

        this.animator = this.GetComponentInChildren<Animator>();
        this.playerInput = this.GetComponent<PlayerInput>();
        this.player = this.GetComponent<PlayerController>();
        this.stateTable = new Dictionary<Type, IState>(states.Length);
        foreach (PlayState state in states)
        {
            state.Initialize(this.animator,
                this.playerInput,
                this.player,this);
            this.stateTable.Add(state.GetType(),state);
        }
    }

    private void Start()
    {
        this.SwitchOn(this.stateTable[typeof(PlayerState_Idle)]);
    }
}
