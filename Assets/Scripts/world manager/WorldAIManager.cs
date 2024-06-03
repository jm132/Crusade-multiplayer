using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

namespace JM
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager instance;

        [Header("Characters")]
        [SerializeField] List<AICharacterSpawner> aICharacterSpawners;
        [SerializeField] List<GameObject> spawnedInCharacters;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SpawnCharacter(AICharacterSpawner aICharacterSpawner)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                aICharacterSpawners.Add(aICharacterSpawner);
                aICharacterSpawner.AttemptToSpawnCharacter();
            }
        }

        private void DespawnAllCharacters()
        {
            foreach(var character in spawnedInCharacters)
            {
                character.GetComponent<NetworkObject>().Despawn();
            }
        }

        private void DisableAllCharacter()
        {
            // to do suable character gameobjects, sync disabled status on network
            // disable gameobbjects for client upon connecting, if disabled status is true
            // can be used to disable characters that are for from players to save memory
            // characters can be split into ares (area_00_, area_01, area_02) ect
        }
    }
}