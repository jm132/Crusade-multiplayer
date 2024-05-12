using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AiCharacterCombarManager : CharaterCombatManager
    {
        [Header("Detaction")]
        [SerializeField] float detectionRadius = 15;
        [SerializeField] float minimumDetectionAngle = -35;
        [SerializeField] float maximumDetectionAngle = 35;

        public void FindATargetViaLineOfSight(AICharacterManager aiCharacter)
        {
            if (currentTarget != null)
                return;

            Collider[] colliders = Physics.OverlapSphere(aiCharacter.transform.position, detectionRadius, WorldUtilityManager.instance.GetCharacterLayer());

            for (int i = 0; i < colliders.Length; i++)
            {
                CharaterManager targetCharacter = colliders[i].transform.GetComponent<CharaterManager>();

                if (targetCharacter == null)
                    continue;

                if (targetCharacter == aiCharacter) 
                    continue;

                if (targetCharacter.isDead.Value)
                    continue;

                // can attack this character, if so, make them target
                if (WorldUtilityManager.instance.CanIDamageThisTarget(aiCharacter.characterGroup, targetCharacter.characterGroup))
                {
                    // if a potential target is found, it has to be infornt
                    Vector3 targetDirection = targetCharacter.transform.position - aiCharacter.transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, aiCharacter.transform.forward);

                    if (viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                    {
                        // lasty, check for enviroment blocks
                        if (Physics.Linecast(aiCharacter.characterCombatManager.lockOnTransform.position,
                            targetCharacter.characterCombatManager.lockOnTransform.position,
                            WorldUtilityManager.instance.GetEnviroLayer()))
                        {
                            Debug.DrawLine(aiCharacter.characterCombatManager.lockOnTransform.position, targetCharacter.characterCombatManager.lockOnTransform.position);
                            Debug.Log("blocked");
                        }
                        else
                        {
                            aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                        }
                    }
                }
            }
        }
    }
}
