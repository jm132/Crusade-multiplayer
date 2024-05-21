using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JM
{
    [CreateAssetMenu(menuName = "A.I/States/Pursue Target")]
    public class PursueTargetState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            // check if performing an action (if so do nothing until action is complete)
            if (aiCharacter.isPerfromingAction)
                return this;

            // check if target is null, if no target, return to idle state
            if (aiCharacter.aiCharacterCombarManager.currentTarget == null)
                return SwitchState(aiCharacter, aiCharacter.idle);

            // make sure navmesh agent is active, if its not enable it
            if (!aiCharacter.navMeshAgent.enabled)
                aiCharacter.navMeshAgent.enabled = true;

            // if the target gose outdise of the characters f.o.v, pivot to face them
            if (aiCharacter.aiCharacterCombarManager.viewableAngle < aiCharacter.aiCharacterCombarManager.minimumFOV
                || aiCharacter.aiCharacterCombarManager.viewableAngle > aiCharacter.aiCharacterCombarManager.maximumFOV)
                aiCharacter.aiCharacterCombarManager.PivotTowardsTarget(aiCharacter);

            aiCharacter.aiCharacterLocomotionManager.RotateTowardsAgent(aiCharacter);

            if (aiCharacter.aiCharacterCombarManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
                return SwitchState(aiCharacter, aiCharacter.combatStance);

            // if the target is not reachable, and far away, return home 

            // purse the target
            NavMeshPath path = new NavMeshPath();
            aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombarManager.currentTarget.transform.position, path);
            aiCharacter.navMeshAgent.SetPath(path);

            return this;
        }

    }
}