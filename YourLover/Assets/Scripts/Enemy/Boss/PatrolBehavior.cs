using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : StateMachineBehaviour
{
    GameObject[] patrolPoints;
    int randomPoint;

    [SerializeField] float moveSpeed = 5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        randomPoint = Random.Range(0, patrolPoints.Length);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPoint].transform.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
            randomPoint = Random.Range(0, patrolPoints.Length);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
