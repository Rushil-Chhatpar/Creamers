using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";
    private bool _useEncryption = false;
    private readonly string _encryptionKey = "STALKER";


    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);

        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // deserialize the data from JSON back to C# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception exception)
            {
                Debug.LogError("Error occured when trying to load data from file!!! file: " + fullPath + "\n" + exception);
            }
        }

        return loadedData;
    }

    public void Save(GameData gameData)
    {
        // using Path.Combine to accound for difference in OS saving types
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            // create directory the file will be written to if it doesn't exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into JSON
            string dataToStore = JsonUtility.ToJson(gameData, true);

            if (_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // write the serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception exception)
        {
            Debug.LogError("Error occured when trying to save data to file!!! file: " + fullPath + "\n" + exception);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string encryptedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            encryptedData += (char)(data[i] ^ _encryptionKey[i % _encryptionKey.Length]);
        }

        return encryptedData;
    }
}
