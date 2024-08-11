using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AIUndeadCombatManager : AiCharacterCombarManager
    {
        [Header("Damage Colliders")]
        [SerializeField] UndeadHandDamageCollider rightHandDamageCollider;
        [SerializeField] UndeadHandDamageCollider leftHandDamageCollider;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.0f;

        public void SetAttack01Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        }

        public void OpenRightHandDamageCollider()
        {
            aICharacter.characterSoundFXManager.PlayAttackGruntSoundFX();
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void DisableRightHandDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void OpenLeftHandDamageCollider()
        {
            aICharacter.characterSoundFXManager.PlayAttackGruntSoundFX();
            leftHandDamageCollider.DisableDamageCollider();
        }

        public void DisableLeftHandDamageCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
        }
    }
}
