using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class WeaponItem : Item
    {
        [Header("Animations")]
        public AnimatorOverrideController weaponAnimator;

        [Header("model Intantiation")]
        public WeaponModelType weaponModelType;

        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapon Class")]
        public WeaponClass weaponClass;

        [Header("Weapon Requirements")]
        public int strengthREQ = 0;
        public int dexREQ = 0;
        public int intREQ = 0;
        public int faithREQ = 0;

        // weapon guard absorption (blocking Power)

        [Header("Weapom Base Damage")]
        public int phyicalDamage = 0;
        public int magicDamage = 0;
        public int fireDamage = 0;
        public int holydamage = 0;
        public int lightningDamage = 0;

        [Header("Weapon Poise")]
        public float poiseDamage = 10;
        // offensive poise bonus when attacking

        [Header("Attack Modifiers")]
        public float light_Attack_01_Modifier = 1.0f;
        public float light_Attack_02_Modifier = 1.2f;
        public float heavy_Attack_01_Modifier = 1.4f;
        public float heavy_Attack_02_Modifier = 1.6f;
        public float charge_Attack_01_Modifier = 2.0f;
        public float charge_Attack_02_Modifier = 2.2f;
        public float running_Attack_01_Modifier = 1.1f;
        public float rolling_Attack_01_Modifier = 1.1f;
        public float backstep_Attack_01_Modifier = 1.1f;

        [Header("Stamina Costs")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaCostMultipler = 0.9f;
        public float heavyAttackStaminaCostMultipler = 1.3f;
        public float chargedAttackStaminaCostMultipler = 1.5f;
        public float runningAttackStaminaCostMultipler = 1.1f;
        public float rollingAttackStaminaCostMultipler = 1.1f;
        public float backstepAttackStaminaCostMultipler = 1.1f;

        [Header("Weapon Blocking Absorption")]
        public float physicalBaseDamegeAbsorption = 50;
        public float magicBaseDamageAbsorption = 50;
        public float fireBaseDamageAbsorption = 50;
        public float holyBaseDamageAbsorption = 50;
        public float lightningBaseDamageAbsorption = 50;
        public float stability = 50; // reduces stamina lost from block

        [Header("Actions")]
        public WeaponItemAction oh_RB_Action; // one handed right bumper action
        public WeaponItemAction oh_RT_Action; // one handed right trigger action
        public WeaponItemAction oh_LB_Action; // one handed Left bumper action
        // ash of war

        // blocking sounds
        [Header("SFX")]
        public AudioClip[] whooshes;
        public AudioClip[] blocking;
    }
}
