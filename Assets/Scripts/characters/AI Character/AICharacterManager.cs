using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JM
{
    public class AICharacterManager : CharaterManager
    {
        [HideInInspector] public AICharacterNetworkManager aiCharacterNetworkManager;
        [HideInInspector] public AiCharacterCombarManager aiCharacterCombarManager;
        [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;

        [Header("Navmesh Agent")]
        public NavMeshAgent navMeshAgent;

        [Header("Current State")]
        [SerializeField] AIState currentState;

        [Header("States")]
        public IdleState idle;
        public PursueTargetState pursueTarget;
        public CombatStanceState combatStance;
        public AttackState attacks;

        protected override void Awake()
        {
            base.Awake();

            aiCharacterNetworkManager = GetComponent<AICharacterNetworkManager>();
            aiCharacterCombarManager = GetComponent<AiCharacterCombarManager>();
            aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();

            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

            // use a copy of scriptable objects, so the originals are not modified
            idle = Instantiate(idle);
            pursueTarget = Instantiate(pursueTarget);

            currentState = idle;
        }

        protected override void Update()
        {
            base.Update();

            aiCharacterCombarManager.HandleActionRecovery(this);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (IsOwner)
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

            if (aiCharacterCombarManager.currentTarget != null)
            {
                aiCharacterCombarManager.targetsDirection = aiCharacterCombarManager.currentTarget.transform.position - transform.position;
                aiCharacterCombarManager.viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, aiCharacterCombarManager.targetsDirection);
                aiCharacterCombarManager.distanceFromTarget = Vector3.Distance(transform.position, aiCharacterCombarManager.currentTarget.transform.position);
            }
            
            if (navMeshAgent.enabled)
            {
                Vector3 agentDestination = navMeshAgent.destination;
                float remainingDistance = Vector3.Distance(agentDestination, transform.position);

                if (remainingDistance > navMeshAgent.stoppingDistance)
                {
                    aiCharacterNetworkManager.isMoving.Value = true;
                }
                else
                {
                    aiCharacterNetworkManager.isMoving.Value= false;
                }
            }
            else
            {
                aiCharacterNetworkManager.isMoving.Value = false;
            }
        }
    }
}
