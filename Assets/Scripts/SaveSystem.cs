using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static bool isFileInUse = false;

    public static void SavePersistentData(PersistentData data)
    {
        if (!isFileInUse)
        {
            isFileInUse = true;

            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/persist.rsf";
            FileStream stream = new FileStream(path, FileMode.Create);

            SerializablePersistentData sData = new SerializablePersistentData(data);

            formatter.Serialize(stream, sData);
            stream.Close();
            Debug.Log(path);
            isFileInUse = false;
        }
    }

    public static SerializablePersistentData LoadPersistentData()
    {
        if (!isFileInUse)
        {
            isFileInUse = true;

            string path = Application.persistentDataPath + "/persist.rsf";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                SerializablePersistentData sData = formatter.Deserialize(stream) as SerializablePersistentData;
                stream.Close();

                isFileInUse = false;
                return sData;
            }
            else
            {
                Debug.Log("Save file not found at " + path);
                isFileInUse = false;
                return null;
            }
        }
        return null;
    }
}
