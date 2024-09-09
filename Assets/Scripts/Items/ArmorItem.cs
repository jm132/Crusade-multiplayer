using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class ArmorItem : EquipmentItem
    {
        [Header("Equipment Absorption bouns")]
        public float physicalDamageAbsorption;
        public float magicDamageAbsorption;
        public float fireDamageAbsorption;
        public float lightningDamageAbsorption;
        public float holyDamageAbsorption;

        [Header("Equipment Resistance Bonus")]
        public float immunity;     // risistance to poison
        public float robustness;   // risistance to bleed and frost
        public float focus;        // risistance to madness and sleep
        public float vitality;     // risistance to death curse

        [Header("Poise")]
        public float poise;

        public EquipmentModel[] equipmentModels;
    }
}
