using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [CreateAssetMenu(menuName = "A.I/Actions/Attack")]
    public class AICharacterAttackAtion : ScriptableObject
    {
        [Header("Attack")]
        [SerializeField] private string attackAnimation;

        [Header("Combo Action")]
        public AICharacterAttackAtion comboAction; // the combo action of this attack action

        [Header("Action Values")]
        [SerializeField] AttackType attackType;
        public int attackWeight = 50;
        // attack can be repeted
        public float actionRecoveryTime = 1.5f; // the time befor the character can make another attack after performing this one
        public float minimumAttackAngle = -35;
        public float maximumAttackAngle = 35;
        public float minimumAttackDistance = 0;
        public float maximumAttackDistance = 2;
        public void AttemptToPerformAction(AICharacterManager aICharacter)
        {
            aICharacter.characterAnimatorManager.PlayTargetActionAnimation( attackAnimation, true);
        }
    }
}
