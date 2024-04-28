using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class WeaponItem : Item
    {
        // animator controller override (change attack animations based on weapon you are currently using)

        [Header("Weapon Model")]
        public GameObject weaponModel;

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
        public float light_Attack_01_Modifier = 1.1f;
        public float heavy_Attack_01_Modifier = 1.4f;
        public float charge_Attack_01_Modifier = 2.0f;

        [Header("Stamina Costs")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaCostMultipler = 0.9f;
        // running attack stamina cost modifier
        // heavy attack stamina cost modifier

        [Header("Actions")]
        public WeaponItemAction oh_RB_Action; // one handed bumper action
        public WeaponItemAction oh_RT_Action; // one handed trigger action


        // ash of war

        // blocking sounds
    }
}
