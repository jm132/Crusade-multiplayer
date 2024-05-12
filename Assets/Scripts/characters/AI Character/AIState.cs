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
    }
}
