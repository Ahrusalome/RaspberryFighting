using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState {get; private set;}
    public State mainStateType = new IdleCombatState();
    private State nextState;
    void Update()
    {
        //Set state to next state if there's one
        if (nextState != null) {
            SetState(nextState);
        }
        // Update the time so we can reset the combo if needed
        if (CurrentState != null) {
            CurrentState.OnUpdate();
        }
    }

    private void SetState(State _newState) {
        nextState = null;
        if (CurrentState != null) {
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        //Will set the animator and state duration of the new state
        CurrentState.OnEnter(this);
    }
    public void SetNextState(State _newState) {
        if (_newState != null) {
            nextState = _newState;
        }
    }
    //Set next state to the idle state (reset the combo)
    public void SetNextStateToMain() {
        nextState = mainStateType;
    }
     private void Awake()
    {
        SetNextStateToMain();
    }

}
