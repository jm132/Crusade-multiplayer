using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] MeleeWeaponDamageCollider meleeDamageCollider;

        private void Awake()
        {
            meleeDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        }

        public void SetWeaponDamage(CharaterManager charaterWieldingWeapon, WeaponItem weapon)
        {
            meleeDamageCollider.characterCausingDamage = charaterWieldingWeapon;
            meleeDamageCollider.physicalDamage = weapon.phyicalDamage;
            meleeDamageCollider.magicDamage = weapon.magicDamage;
            meleeDamageCollider.fireDamage = weapon.fireDamage;
            meleeDamageCollider.lightningDamage = weapon.lightningDamage;
            meleeDamageCollider.holyDamage = weapon.holydamage;
        }
    }
}