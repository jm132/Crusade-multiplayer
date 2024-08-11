using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class ResetActionFlag : StateMachineBehaviour
    {
        CharaterManager charater;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (charater == null)
            {
                charater = animator.GetComponent<CharaterManager>();
            }

            // this is called when an action ends, and the state returns to "empty"
            charater.isPerfromingAction = false;
            charater.characterAnimatorManager.applyRootMotion = false;
            charater.charaterLocomotionManager.canRotate = true;
            charater.charaterLocomotionManager.canMove = true;
            charater.charaterLocomotionManager.isRolling = false;
            charater.characterCombatManager.DisableCanDoCombo();
            charater.characterCombatManager.DisableCanDoRollingAttack();
            charater.characterCombatManager.DisableCanDoBackStepAttack();

            if (charater.IsOwner)
            {
                charater.characterNetworkManager.isJumping.Value = false;
                charater.characterNetworkManager.isInvulnerable.Value = false;
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

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
}