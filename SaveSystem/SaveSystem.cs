using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer(PlayerAbiltiesHandler abilities, Health playerHealth, Transform position, int level) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(abilities, playerHealth, position, level);

        formatter.Serialize(stream, data);
        stream.Close();


    }

    public static PlayerData LoadPlayer(PlayerAbiltiesHandler abilities, Health playerHealth, Transform position) 
    {
        string path = Application.persistentDataPath + "/player.save";
        if(File.Exists(path)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
    

