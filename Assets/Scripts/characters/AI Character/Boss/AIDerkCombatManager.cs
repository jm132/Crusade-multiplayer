using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AIDerkCombatManager : AiCharacterCombarManager
    {
        AIDerkCharacterManager durkManager;

        [Header("Damage Collider")]
        [SerializeField] BossMeleeDamageCollider clubDamageCollider;
        [SerializeField] DurksStompCollider stompCollider;
        public float stompAttackAOERadius = 1.5f;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.0f;
        [SerializeField] float attack03DamageModifier = 1.0f;
        public float stompDamage = 25;

        [Header("VFX")]
        public GameObject durkInpactVFX;

        protected override void Awake()
        {
            base.Awake();

            durkManager = GetComponent<AIDerkCharacterManager>();
        }

        public void SetAttack01Damage()
        {
            aICharacter.characterSoundFXManager.PlayAttackGruntSoundFX();
            clubDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            aICharacter.characterSoundFXManager.PlayAttackGruntSoundFX();
            clubDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        }

        public void SetAttack03Damage()
        {
            aICharacter.characterSoundFXManager.PlayAttackGruntSoundFX();
            clubDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
        }

        public void OpenClubDamageCollider()
        {
            clubDamageCollider.EnableDamageCollider();
            durkManager.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.Instance.ChooseRandomSFXFromArray(durkManager.durkSoundFXManager.weaponWhooshes));

        }

        public void DisableClubDamageCollider()
        {
            clubDamageCollider.DisableDamageCollider();
        }

        public void ActivateDurkStomp()
        {
            stompCollider.StompAttack();
        }

        public override void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            // play a pivot animation depending on viweable Angle of target
            if (aiCharacter.isPerfromingAction)
                return;

            if (viewableAngle >= 61 && viewableAngle <= 110)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_90", true);
            }
            else if (viewableAngle <= -61 && viewableAngle >= -110)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_90", true);
            }
            else if (viewableAngle >= 146 && viewableAngle <= 180)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);
            }
            else if (viewableAngle <= -146 && viewableAngle >= -180)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
            }
        }
    }
}
