using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameControl : MonoBehaviour
{

    public static GameControl gameControl;

    public Settings settings;  
    public MatchSettings matchSettings;

    private String settingsFilePath;

    // Start is called before the first frame update
    void Awake()
    {        
        if(gameControl == null)
        {
            DontDestroyOnLoad(gameObject);
            gameControl = this;
            settingsFilePath = Application.persistentDataPath+"/"+"settings.dat";
            gameControl.Load();
        }
        else if(gameControl != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(settingsFilePath, FileMode.Create);
        binaryFormatter.Serialize(file, settings);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(settingsFilePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(settingsFilePath, FileMode.Open);
            settings = (Settings) binaryFormatter.Deserialize(file);
            file.Close();
        }
    }

}

[Serializable]
public class Settings
{
    public float musicVol;
    public float soundVol;
}

[Serializable]
public class MatchSettings
{
    public int courtSize;
    public int ballIndex;
    public int matchType; // 0 = Time, 1 = Goals
    public int duration;   
    public int goalsToWin; 
    public bool nukeStuckBall;
    public bool powerupsEnabled;
    public int powerupFrequeny;
    public bool powerupTurboEnabled; 
    public bool powerupShieldEnabled;
    public bool powerupMineEnabled;
    public bool powerupMissileEnabled;
    public bool powerupEMPEnabled;
    
}