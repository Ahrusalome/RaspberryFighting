using UnityEngine;

//First Melee combo attack
public class MeleeComboStart : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        attackIndex = 1;
        duration = 0.5f;
        animator.SetTrigger("SDAttack" + attackIndex);
        lastHitTime = Time.time + duration;
    }
}
