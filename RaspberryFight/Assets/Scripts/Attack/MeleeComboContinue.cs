using UnityEngine;

//Second melee combo attack
public class MeleeComboContinue : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        attackIndex = 2;
        duration = 1f;
        animator.SetTrigger("SDAttack" + attackIndex);
        lastHitTime = Time.time + duration;
    }
}
