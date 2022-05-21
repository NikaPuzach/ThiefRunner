using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance { get; private set; }

    private UserData data;

    const string fileName = "/user.data";
    public Action<int> onMoneyChange;

    public int Money
    {
        get => data.money;
        set
        {
            if (value >= 0)
                data.money = value;
            else
                data.money = 0;
            onMoneyChange?.Invoke(data.money);
            Save();
        }
    }

    void Awake()
    {
        Load();

        Instance = this;
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + fileName;
        Debug.Log($"{path}");

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = stream.Length == 0? new UserData(): formatter.Deserialize(stream) as UserData;
            stream.Close();
        }
        else
        {
            data = new UserData();
        }
    }
}
