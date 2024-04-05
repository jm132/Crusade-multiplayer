using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")]
        protected Collider damageCollider;

        [Header("Damage")]
        public float physicalDamage = 0;
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Contact Point")]
        protected Vector3 contactPoint;

        [Header("Characters Damaged")]
        protected List<CharaterManager> charactersDamaged = new List<CharaterManager>();

        private void OnTriggerEnter(Collider other)
        {

            CharaterManager damageTarget = other.GetComponentInParent<CharaterManager>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // check if we can damage this traget based on friendly fire

                // check if target is blocking

                // check if target is invulnerable

                DamageTarget(damageTarget);
            }
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
