using UnityEngine;
using FreeDraw;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.piss";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.piss";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }

        {
    
            return null;
        }

    }

        public static void SaveStamp(DrawingSettings drawSet)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path2 = Application.persistentDataPath + "/stamps.piss";
            FileStream stream = new FileStream(path2, FileMode.Create);
            StampData data = new StampData(drawSet);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static StampData LoadStamp()
        {
            string path1 = Application.persistentDataPath + "/stamps.piss";
            if (File.Exists(path1))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path1, FileMode.Open);

                StampData data = formatter.Deserialize(stream) as StampData;
                stream.Close();
                return data;

            }

            {
                Debug.LogError("save file not found at " + path1);
                return null;
            }
        }
    public static void SaveTreasure(TreasureManager treasureManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/treasure.piss";
        FileStream stream = new FileStream(path, FileMode.Create);

        TreasureData data = new TreasureData(treasureManager);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static TreasureData LoadTreasure()
    {

        string path = Application.persistentDataPath + "/treasure.piss";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            TreasureData data = formatter.Deserialize(stream) as TreasureData;
            stream.Close();
            return data;
        }

        {
            Debug.LogError("save file not found at " + path);
            return null;
        }

    }

}
