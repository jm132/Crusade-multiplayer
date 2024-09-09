using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class StaticCharacterEffect : ScriptableObject
    {
        [Header("Effect I.D")]
        public int staticEffectID;

        public virtual void ProcessStaticEffect(CharaterManager charater)
        {

        }

        public virtual void RemoveStaticEffect(CharaterManager charater)
        {

        }
    }
}
