using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AICharacterLocomotionManager : CharaterLocomotionManager
    {
        public void RotateTowardsAgent(AICharacterManager aICharacter)
        {
            if (aICharacter.aiCharacterNetworkManager.isMoving.Value)
            {
                aICharacter.transform.rotation = aICharacter.navMeshAgent.transform.rotation;
            }
        }
    }
}