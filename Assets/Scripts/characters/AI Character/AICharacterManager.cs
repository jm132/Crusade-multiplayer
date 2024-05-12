using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AICharacterManager : CharaterManager
    {
        public AiCharacterCombarManager aiCharacterCombarManager;

        [Header("Current State")]
        [SerializeField] AIState currentState;

        protected override void Awake()
        {
            base.Awake();

            aiCharacterCombarManager = GetComponent<AiCharacterCombarManager>();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            ProcessStateMachine();
        }

        private void ProcessStateMachine()
        {
             AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }
}
