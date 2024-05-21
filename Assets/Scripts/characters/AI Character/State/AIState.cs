using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AIState : ScriptableObject
    {
        public virtual AIState Tick(AICharacterManager aICharacter)
        {
            return this;
        }

        protected virtual AIState SwitchState(AICharacterManager aICharacter, AIState newState)
        {
            ResetStateFlags(aICharacter);
            return newState;
        }

        protected virtual void ResetStateFlags(AICharacterManager aiCharacter)
        {
            // reset any state flags here so when return to the state, it blank once again
        }
    }
}
