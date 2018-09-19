using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class CheckSaveFileController : SceneController {

    [Header("System Data")]
    [SerializeField]
    private string SYSTEM_DATA_FILE_NAME;
    [SerializeField]
    private string SYSDATA_EXT;
    [Space]
    [Header("Save Data")]
    [SerializeField]
    private string SAVE_DATA_FILE_NAME;
    [SerializeField]
    private string SAVE_EXT;
    [Space]
    [Header("Display Message")]
    [SerializeField]
    [TextArea(1, 3)]
    private string SYSTEM_DATA_NONE;
    [SerializeField]
    [TextArea(1, 3)]
    private string HAS_SYSTEM_DATA;
    [Header("Display Effect")]
    [SerializeField]
    private TypeWriter typeWriter;

    private bool checkingFinished;

    // for initialization
	void Start () {
        tempDelay = delay; 
        checkingFinished = false;

        // check system data
        Debug.Log("Checking system data..."); // logs
        CheckSystemData();
	}
	
	void Update () {
        // checks if animation is fin
        if (typeWriter.typeFinished)
        {
            if (!checkingFinished) return; // if checking is not finished
                
            if (tempDelay > 0)
            {
                tempDelay--;
                return;
            }

            // if delay is finished, load next scene
            fadeAnimator.SetTrigger("FadeOut"); // trigger fade out animation
            // instead of loading scene here, use animation event
        }
	}

    private void CheckSystemData()
    {
        // check if system data exists
        if(File.Exists(Application.persistentDataPath + "/" +
            SYSTEM_DATA_FILE_NAME + SYSDATA_EXT)) 
        {
            // display message
            typeWriter.RestartTyping(HAS_SYSTEM_DATA);
            Debug.Log(HAS_SYSTEM_DATA); // logs
            Debug.Log("System Data Path: " + Application.persistentDataPath + "/" +
                SYSTEM_DATA_FILE_NAME + SYSDATA_EXT); // logs
        }
        else
        {
            // display message
            typeWriter.RestartTyping(SYSTEM_DATA_NONE);
            Debug.Log(SYSTEM_DATA_NONE); // logs
            Debug.Log("Persisten Data Path: " + Application.persistentDataPath); // logs

            // create a system data
            CreateSystemData(); // one system data
            CreateSaveData(); // creates 50 save files
        }
    }

    // create system data (contains player settings)
    private void CreateSystemData()
    {
        SystemData systemData = new SystemData(); // create an instance of system data
        
        BinaryFormatter binaryFormatter = new BinaryFormatter(); // convert to binary
        FileStream fileStream = File.Create(Application.persistentDataPath + "/" +
            SYSTEM_DATA_FILE_NAME + SYSDATA_EXT);
        binaryFormatter.Serialize(fileStream, systemData);
        fileStream.Close();

        Debug.Log("System Data created."); // logs
    }

    // create save data (contains player progress)
    private void CreateSaveData()
    {
        // read system data
        if (File.Exists(Application.persistentDataPath + "/" +
            SYSTEM_DATA_FILE_NAME + SYSDATA_EXT))
        {
            // display message
            typeWriter.RestartTyping(HAS_SYSTEM_DATA);
            Debug.Log(HAS_SYSTEM_DATA); // logs

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/" +
                SYSTEM_DATA_FILE_NAME + SYSDATA_EXT, FileMode.Open);
            SystemData systemData = (SystemData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            Debug.Log("Reading system data."); // logs
            
            // create save data
            Debug.Log("Creating " + systemData.maxSaveFiles + " save data files..."); // logs
            for (int i = 1; i <= systemData.maxSaveFiles; i++)
            {
                SaveData save = new SaveData();
                binaryFormatter = new BinaryFormatter(); // convert to binary
                fileStream = File.Create(Application.persistentDataPath + "/" +
                    SAVE_DATA_FILE_NAME + i + SAVE_EXT);
                binaryFormatter.Serialize(fileStream, save);
                fileStream.Close();
            }
            Debug.Log("Save data files created."); // logs
        }
    }

    // by sh0
}
