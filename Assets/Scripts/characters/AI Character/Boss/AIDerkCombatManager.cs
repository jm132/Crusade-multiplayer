using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AIDerkCombatManager : AiCharacterCombarManager
    {
        [Header("Damage Collider")]
        [SerializeField] BossMeleeDamageCollider clubDamageCollider;
        [SerializeField] Transform durksStompingFoot;
        [SerializeField] float stompAttackAOERadius = 1.5f;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.0f;
        [SerializeField] float attack03DamageModifier = 1.0f;
        [SerializeField] float stompDamage = 25;

        public void SetAttack01Damage()
        {
            clubDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            clubDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        }

        public void SetAttack03Damage()
        {
            clubDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
        }

        public void OpenClubDamageCollider()
        {
            aICharacter.characterSoundFXManager.PlayAttackGrunt();
            clubDamageCollider.EnableDamageCollider();

        }

        public void DisableClubDamageCollider()
        {
            clubDamageCollider.DisableDamageCollider();
        }

        public void ActivateDurkStomp()
        {
            Collider[] colliders = Physics.OverlapSphere(durksStompingFoot.position, stompAttackAOERadius, WorldUtilityManager.instance.GetCharacterLayer());
            List<CharaterManager> charactersDamaged = new List<CharaterManager>();

            foreach (var collider in colliders)
            {
                CharaterManager charater = collider.GetComponentInParent<CharaterManager>();

                if(charater != null)
                {
                    if (charactersDamaged.Contains(charater))
                        continue;

                    charactersDamaged.Add(charater);

                    // only process damage if the character "isowner" so that the player get damaged if the collider connects on the client
                    // meaning if the player is hit on the hosts screen but not on player screen, player will not be hit
                    if (charater.IsOwner)
                    {
                        // chech for block
                        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
                        damageEffect.physicalDamage = stompDamage;
                        damageEffect.poiseDamage = stompDamage;

                        charater.characterEffectsManager.ProcessInstantEffect(damageEffect);
                    }
                }
            }
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
