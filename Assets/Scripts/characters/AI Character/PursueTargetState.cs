using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JM
{
    [CreateAssetMenu(menuName = "A.I/States/Pursue Target")]
    public class PursueTargetState : AIState
    {
        public override AIState Tick(AICharacterManager aICharacter)
        {
            // check if performing an action (if so do nothing until action is complete)
            if (aICharacter.isPerfromingAction)
                return this;

            // check if target is null, if no target, return to idle state
            if (aICharacter.aiCharacterCombarManager.currentTarget == null)
                return SwitchState(aICharacter, aICharacter.idle);

            // make sure navmesh agent is active, if its not enable it
            if (!aICharacter.navMeshAgent.enabled)
                aICharacter.navMeshAgent.enabled = true;

            aICharacter.aiCharacterLocomotionManager.RotateTowardsAgent(aICharacter);

            // if within combat range of a target, switch state to combat stance state

            // if the target is not reachable, and far away, return home 

            // purse the target
            NavMeshPath path = new NavMeshPath();
            aICharacter.navMeshAgent.CalculatePath(aICharacter.aiCharacterCombarManager.currentTarget.transform.position, path);
            aICharacter.navMeshAgent.SetPath(path);

            return this;
        }

    }
}