using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        // process instant effects (take Damage, heal)

        // process timed effects (poison, build ups)

        // process static effects (adding/removing buffs from talismans ect)

        CharaterManager charater;

        protected virtual void Awake()
        {
            charater = GetComponent<CharaterManager>();
        }

        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProcessEffect(charater);
        }
    }
}
