using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    //The state script linked to the GameObject that will permit to go from a state to another
    private StateMachine meleeStateMachine;
    //Base state from which the combo can start
    private IdleCombatState idle = new IdleCombatState();
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        meleeStateMachine = GetComponent<StateMachine>();
    }
    void OnSDAttack() {
        // When the melee attack input has been pressed, if the player isn't being attacked, set the different combo states
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("IsAttacked")){
            if (meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState)) {
                meleeStateMachine.SetNextState(new MeleeComboStart());
            }
            if (meleeStateMachine.CurrentState.GetType() == typeof(MeleeComboStart)) {
                meleeStateMachine.SetNextState(new MeleeComboContinue());
            }
            if (meleeStateMachine.CurrentState.GetType() == typeof(MeleeComboContinue)) {
                meleeStateMachine.SetNextState(new MeleeComboFinal());
            }
        }
    }
}
