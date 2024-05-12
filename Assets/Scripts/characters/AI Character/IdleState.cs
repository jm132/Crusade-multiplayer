using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "A.I/States/Idle")]
    public class IdleState : AIState
    {
        public override AIState Tick(AICharacterManager aICharacter)
        {
            if (aICharacter.characterCombatManager.currentTarget != null)
            {
                // retuen the pursue target state (change the state to the pursue target state)
                Debug.Log("have a target");

                return this;
            }
            else
            {
                // return this state, to continually search for a taget (keep the state here, until a target is found)
                aICharacter.aiCharacterCombarManager.FindATargetViaLineOfSight(aICharacter);
                Debug.Log("dont have a target");
                return this;
            }
        }
    }
}
