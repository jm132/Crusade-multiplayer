using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class AIDerkCharacterManager : AIBossCharacterManager
    {
        // why give the bosses there own character manager ?
        // the character manager act as a hub to where it can be refernce all componers for a character

        [HideInInspector]public AIBossSoundFXManager durkSoundFXManager;
        [HideInInspector] public AIDerkCombatManager durkCombatManager;

        protected override void Awake()
        {
            base.Awake();

            durkSoundFXManager = GetComponent<AIBossSoundFXManager>();
            durkCombatManager = GetComponent<AIDerkCombatManager>();
        }
    }
}
