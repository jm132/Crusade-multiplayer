using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")]
        [SerializeField] protected Collider damageCollider;

        [Header("Damage")]
        public float physicalDamage = 0;
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Poise")]
        public float poiseDamage = 0;

        [Header("Contact Point")]
        protected Vector3 contactPoint;

        [Header("Characters Damaged")]
        protected List<CharaterManager> charactersDamaged = new List<CharaterManager>();

        [Header("Block")]
        protected Vector3 directionFromAttackToDamageTarget;
        protected float dotValueFromAttackToDamageTarget;

        protected virtual void Awake ()
        {

        }

        protected virtual void OnTriggerEnter(Collider other)
        {

            CharaterManager damageTarget = other.GetComponentInParent<CharaterManager>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // check if we can damage this traget based on friendly fire

                // check if target is blocking
                CheckForBlock(damageTarget);

                DamageTarget(damageTarget);
            }
        }

        protected virtual void CheckForBlock(CharaterManager damageTarget)
        {
            // if this character has already baan damaged, do not proceed
            if(charactersDamaged.Contains(damageTarget))
                return;

            GetBlockingDotValues(damageTarget);

            if (damageTarget.characterNetworkManager.isBlocking.Value && dotValueFromAttackToDamageTarget > 0.3f)
            {
                charactersDamaged.Add(damageTarget);

                TakeBlockDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeBlockDamageEffect);

                damageEffect.physicalDamage = physicalDamage;
                damageEffect.magicDamage = magicDamage;
                damageEffect.fireDamage = fireDamage;
                damageEffect.holyDamage = holyDamage;
                damageEffect.poiseDamage = poiseDamage;
                damageEffect.StaminaDamage = poiseDamage;
                damageEffect.contactPoint = contactPoint;

                //apply block character damage to target
                damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
            }
        }

        protected virtual void GetBlockingDotValues(CharaterManager damageTarget)
        {
            directionFromAttackToDamageTarget = transform.position - damageTarget.transform.position;
            dotValueFromAttackToDamageTarget = Vector3.Dot(directionFromAttackToDamageTarget, damageTarget.transform.forward);
        }

        protected virtual void DamageTarget(CharaterManager damageTarget)
        {
            // don't want to damage the same target more than once in a single attack
            // add them to a list then checks before applying damage
            if(charactersDamaged.Contains(damageTarget))
                return;

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.contactPoint = contactPoint;
            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            charactersDamaged.Clear();  // reset the chatacters that have been hit when reset the collider, so they may be hit again. 
        }
    }
}
