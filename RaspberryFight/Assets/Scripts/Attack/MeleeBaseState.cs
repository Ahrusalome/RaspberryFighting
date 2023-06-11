using UnityEngine;

public class MeleeBaseState : State
{
    //Total duration of the attack
    public float duration;
    protected int attackIndex;
    protected Animator animator;
    public override void OnEnter(StateMachine _stateMachine) {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        //Reset the combo past the set delay time (=ResetTime)
        if (Time.time - lastHitTime >= ResetTime) {
            stateMachine.SetNextStateToMain();
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
