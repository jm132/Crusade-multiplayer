using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq.Expressions;

namespace JM
{
    public class SaveFileDataWriter
    {
        public string saveDataDirctoryPath = "";
        public string saveFileName = "";

        // before create a new save file, check ti see if one of this character slot already exists
        public bool CheckToSeeIfFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirctoryPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // used to delete character save files
        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirctoryPath,saveFileName));
        }

        // used to create a save file upon starting a new game
        public void CreateNewCharacterSaveFile(CharaterSaveData charaterDate)
        {
            // make a path to save the file on the device 
            string savePath = Path.Combine(saveDataDirctoryPath,saveFileName);

            try
            {
                // Creat the directory for the file to be written to, if it does not already exist
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("CREATING SAVE FILE, AT SAVE PATH: " + savePath);

                // serialize the C# game data object into json
                string dataToStore = JsonUtility.ToJson(charaterDate, true);

                // Write the file to the system
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("ERROR WHILST TRYING TO SAVE CHARACTER DATA, GAME NOT SAVED" + savePath + "\n" + ex);
            }
        }

        // used to load a save files upon loading a previous game
        public CharaterSaveData LoadSaveFile()
        {
            CharaterSaveData charaterData = null;

            // make a path to load the file on the device 
            string loadPath = Path.Combine(saveDataDirctoryPath, saveFileName);

            if (File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    // deserialze the data from jason back to unity C#
                    charaterData = JsonUtility.FromJson<CharaterSaveData>(dataToLoad);
                }
                catch (Exception ex)
                {
                    Debug.Log("FILE IS BLANK");
                }
            }

            return charaterData;
        }
    }
}
