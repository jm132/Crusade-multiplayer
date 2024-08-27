using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

namespace JM
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        public PlayerManager player;

        [Header("SAVE/LOAD")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("World Scene Index")]
        [SerializeField] int WorldSceneIndex = 1;

        [Header("save Data Writer")]
        private SaveFileDataWriter saveFileDateWriter;

        [Header("Current Character Data")]
        public CharacterSlot currentCharacterSlotBeingUsed;
        public CharaterSaveData currentCharacterData;
        private string saveFileName;

        [Header("Character Slots")]
        public CharaterSaveData characterSlot01;
        public CharaterSaveData characterSlot02;
        public CharaterSaveData characterSlot03;
        public CharaterSaveData characterSlot04;
        public CharaterSaveData characterSlot05;
        public CharaterSaveData characterSlot06;
        public CharaterSaveData characterSlot07;
        public CharaterSaveData characterSlot08;
        public CharaterSaveData characterSlot09;
        public CharaterSaveData characterSlot10;

        private void Awake()
        {
            // there can only be one instance of this script at one time. if another exists, destroy it.
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
           
        } 

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadAllCharacterProfiles();
        }

        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }
            if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }

        public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
        {
            string fileName = "";

            switch (characterSlot)
            {
                case CharacterSlot.CharacterSlot_01:
                    fileName = "characterSlot_01";
                    break;
                case CharacterSlot.CharacterSlot_02:
                    fileName = "characterSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    fileName = "characterSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    fileName = "characterSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    fileName = "characterSlot_05";
                    break;
                case CharacterSlot.CharacterSlot_06:
                    fileName = "characterSlot_06";
                    break;
                case CharacterSlot.CharacterSlot_07:
                    fileName = "characterSlot_07";
                    break;
                case CharacterSlot.CharacterSlot_08:
                    fileName = "characterSlot_08";
                    break;
                case CharacterSlot.CharacterSlot_09:
                    fileName = "characterSlot_09";
                    break;
                case CharacterSlot.CharacterSlot_10:
                    fileName = "characterSlot_10";
                    break;
                default:
                    break;
            }
            return fileName;
        }

        public void AttemptCreateNewGame()
        {

            saveFileDateWriter = new SaveFileDataWriter();
            saveFileDateWriter.saveDataDirctoryPath = Application.persistentDataPath;

            // generally work on multiple mechina types (Application.persistentDataPath)
            saveFileDateWriter.saveFileName = saveFileName;
            // check to see if we can create a new save file
            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_01;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_02;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_03;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_04;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_05;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_06;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_07;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_08;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_09;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);

            if (!saveFileDateWriter.CheckToSeeIfFileExists())
            {
                // if this profile slot is not taken, make a new one using this slot
                currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_10;
                currentCharacterData = new CharaterSaveData();
                NewGame();
                return;
            }

            TitleScreenManager.Instance.DisplayNoFreeCharacterSlotsPopUp();
        }

        private void NewGame()
        {
            // saves the newly created character stats, and items (when creation screen is added)
            player.playerNetworkManager.vitality.Value = 15;
            player.playerNetworkManager.endurance.Value = 10;
            
            SaveGame();
            LoadWorldScene(WorldSceneIndex);
        }

        public void LoadGame()
        {
            // load a previos file, with a file name depending on which slot is in use
           saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveFileDateWriter = new SaveFileDataWriter();
            // generally work on multiple mechina types (Application.persistentDataPath)
            saveFileDateWriter.saveDataDirctoryPath = Application.persistentDataPath;
            saveFileDateWriter.saveFileName = saveFileName;
            currentCharacterData = saveFileDateWriter.LoadSaveFile();

            LoadWorldScene(WorldSceneIndex);
        }

        public void SaveGame()
        {
            // save the current file under a file name depending on which slot is in used
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveFileDateWriter = new SaveFileDataWriter();
            // generally work on multiple mechina types (Application.persistentDataPath)
            saveFileDateWriter.saveDataDirctoryPath = Application.persistentDataPath;
            saveFileDateWriter.saveFileName = saveFileName;

            //pass the players info, from game, to their save file
            player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

            // write that info onto a json file, saved to this mechine
            saveFileDateWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        public void DeleteGame(CharacterSlot characterSlot)
        {
            // choose file based on name
            saveFileDateWriter = new SaveFileDataWriter();
            saveFileDateWriter.saveDataDirctoryPath = Application.persistentDataPath;
            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
            
            saveFileDateWriter.DeleteSaveFile();
        }

        // load all character profiles on device when stating game
        private void LoadAllCharacterProfiles()
        {
            saveFileDateWriter = new SaveFileDataWriter();
            saveFileDateWriter.saveDataDirctoryPath = Application.persistentDataPath;

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
            characterSlot01 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
            characterSlot02 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
            characterSlot03 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
            characterSlot04 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);
            characterSlot05 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);
            characterSlot06 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);
            characterSlot07 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);
            characterSlot08 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);
            characterSlot09 = saveFileDateWriter.LoadSaveFile();

            saveFileDateWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);
            characterSlot10 = saveFileDateWriter.LoadSaveFile();

        }

        public void LoadWorldScene(int buidIndex)
        {
            string worldScene = SceneUtility.GetScenePathByBuildIndex(buidIndex);
            NetworkManager.Singleton.SceneManager.LoadScene(worldScene, LoadSceneMode.Single);

            player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);
        }

        public int GetWorldSceneIndex()
        {
            return WorldSceneIndex;
        }
    }
}
