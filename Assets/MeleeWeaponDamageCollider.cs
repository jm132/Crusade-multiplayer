using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharaterManager characterCausingDamage; // (when calculating damage this is used to check for attackers damage modifiers, effects ect)

    }
}