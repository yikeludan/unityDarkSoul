using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    IState currentState;

    protected Dictionary<System.Type, IState> stateTable;

    void FixedUpdate()
    {
        this.currentState.PhysicUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        this.currentState.LogicUpdate();
    }

    public void SwitchOn(IState newState)
    {
        this.currentState = newState;
        this.currentState.Enter();
    }

    public void SwitchState(IState newState)
    {
        this.currentState.Exit();
        this.SwitchOn(newState);
    }

    public void SwitchState(System.Type newStateType)
    {
        this.SwitchState(this.stateTable[newStateType]);
    }
}
