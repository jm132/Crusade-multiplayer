using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class WorldUtilityManager : MonoBehaviour
    {
        public static WorldUtilityManager instance;

        [Header("Layer")]
        [SerializeField] LayerMask characterLayers;
        [SerializeField] LayerMask enviroLayer;

        private void Awake()
        {
            if (instance == null )
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public LayerMask GetCharacterLayer()
        {
            return characterLayers;
        }

        public LayerMask GetEnviroLayer()
        {
            return enviroLayer;
        }
    }
}
