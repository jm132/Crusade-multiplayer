using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JM
{
    public class AICharacterManager : CharaterManager
    {
        [HideInInspector] public AICharacterNetworkManager aICharacterNetworkManager;
        [HideInInspector] public AiCharacterCombarManager aiCharacterCombarManager;
        [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;

        [Header("Navmesh Agent")]
        public NavMeshAgent navMeshAgent;

        [Header("Current State")]
        [SerializeField] AIState currentState;

        [Header("States")]
        public IdleState idle;
        public PursueTargetState pursueTarget;
        // combat stance
        // attack

        protected override void Awake()
        {
            base.Awake();

            aICharacterNetworkManager = GetComponent<AICharacterNetworkManager>();
            aiCharacterCombarManager = GetComponent<AiCharacterCombarManager>();
            aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

            // use a copy of scriptable objects, so the originals are not modified
            idle = Instantiate(idle);
            pursueTarget = Instantiate(pursueTarget);

            currentState = idle;
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

            // the position/rotation should be reset only after the state machine has processed it's tick
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;

            if (navMeshAgent.enabled)
            {
                Vector3 agentDestination = navMeshAgent.destination;
                float remainingDistance = Vector3.Distance(agentDestination, transform.position);

                if (remainingDistance > navMeshAgent.stoppingDistance)
                {
                    aICharacterNetworkManager.isMoving.Value = true;
                }
                else
                {
                    aICharacterNetworkManager.isMoving.Value= false;
                }
            }
            else
            {
                aICharacterNetworkManager.isMoving.Value = false;
            }
        }
    }
}
