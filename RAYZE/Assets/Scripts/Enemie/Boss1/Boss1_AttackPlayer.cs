using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Source https://www.youtube.com/watch?v=lHLZxd0O6XY ChronoAbi
public class Boss1_AttackPlayer : StateMachineBehaviour
{
    [SerializeField] Boss1_SrctySystem Boss1;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boss1 = GameObject.FindGameObjectWithTag("OldSrctySystem").GetComponent<Boss1_SrctySystem>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boss1.AttackPlayerState();
    }
}
