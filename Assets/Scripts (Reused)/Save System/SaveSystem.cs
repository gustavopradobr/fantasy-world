using System;
using UnityEngine;
using System.Text;

public static class SaveSystem
{
    private const string prefSaveKey = "savedata";
    public static void SaveGameData (GameData gameData)
    {
        GameDataSerialize data = new GameDataSerialize(gameData);     
        PrefsSave(data);
    }

    public static GameDataSerialize LoadGameData(GameData gameData)
    {
        if (PlayerPrefsSaveExist())
        {
            return PrefsLoad<GameDataSerialize>();
        }
        else
        {
            GameDataSerialize data = new GameDataSerialize(gameData);
            SaveGameData(gameData);
            return data;
        }
    }

    private static bool PlayerPrefsSaveExist()
    {
        return PlayerPrefs.HasKey(prefSaveKey);
    }
    private static T PrefsLoad<T>()
    {
        return JsonUtility.FromJson<T>(Encoding.UTF8.GetString(Convert.FromBase64String(PlayerPrefs.GetString(prefSaveKey))));        
    }
    private static void PrefsSave(object data)
    {
        PlayerPrefs.SetString(prefSaveKey, Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonUtility.ToJson(data))));
        PlayerPrefs.Save();
    }
}
