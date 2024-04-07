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

        // wepon modifiers
        // light attack modifier
        // heavy attack modifier
        // critical damage modifier ect

        [Header("Stamina Costs")]
        public int baseStaminaCost = 20;
        // running attack stamina cost modifier
        // light attack stamina cost modifier
        // heavy attack stamina cost modifier

        [Header("Actions")]
        public WeaponItemAction oh_RB_Action; // one handed action


        // ash of war

        // blocking sounds
    }
}
