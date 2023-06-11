using UnityEngine;

public abstract class State
{
    protected float time {get; set;}
    //The amount of time you shouldn't exceed if you want to combo
    protected float ResetTime = 0.8f;
    protected float lastHitTime;

    public StateMachine stateMachine;
    //Retrieve the state machine script from which all the states are given so we can access to the game object it's linked to
    public virtual void OnEnter(StateMachine _stateMachine) {
        stateMachine = _stateMachine;
    }
    public virtual void OnUpdate() {
        time += Time.deltaTime;
    }
    public virtual void OnExit() {

    }
    //Since it's not a MonoBehaviour class you have to redeclare that method if you want to use it
    protected T GetComponent<T>() where T : Component {
         return stateMachine.GetComponent<T>(); 
        }
}
