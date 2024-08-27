using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

namespace JM
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager instance;

        [Header("Characters")]
        [SerializeField] List<AICharacterSpawner> aICharacterSpawners;
        [SerializeField] List<AICharacterManager> spawnedInCharacters;

        [Header("Bosses")]
        [SerializeField] List<AIBossCharacterManager> spawnedInBosses;

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

        public void AddCharacterToSpawnedCharactersList(AICharacterManager character)
        {
            if (spawnedInCharacters.Contains(character))
                return;

            spawnedInCharacters.Add(character);

            AIBossCharacterManager bossCharacter = character as AIBossCharacterManager;

            if (bossCharacter != null)
            {
                if (spawnedInBosses.Contains(bossCharacter))
                    return;

                spawnedInBosses.Add(bossCharacter);
            }
        }

        public AIBossCharacterManager GetBossCharacterByID(int ID)
        {
            return spawnedInBosses.FirstOrDefault(boss => boss.bossID == ID);
        }

        public void RestAllCharacters()
        {
            DespawnAllCharacters();

            foreach (var spawner in aICharacterSpawners)
            {
                spawner.AttemptToSpawnCharacter();
            }
        }

        private void DespawnAllCharacters()
        {
            foreach(var character in spawnedInCharacters)
            {
                character.GetComponent<NetworkObject>().Despawn();
            }

            spawnedInCharacters.Clear();
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