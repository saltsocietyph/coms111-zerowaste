using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using TMPro;

public class TitleScreenController : BaseSceneController {

    [Space]
    [Header("Button Functions")]
    [SerializeField]
    private Button[] buttons;
    [Space]
    [Header("Overlay Panels")]
    [SerializeField]
    private GameObject[] panels;
    [SerializeField]
    private Animator[] animators;
    [Space]
    [Header("Information")]
    [SerializeField]
    private TextMeshProUGUI[] infos;
    [Space]
    [Header("System Data")]
    [SerializeField]
    private string SYSTEM_DATA_FILE_NAME;
    [SerializeField]
    private string SYSDATA_EXT;

    private Color active, inactive;
    private SystemData systemData;
    private SaveData currentSave;
    private SaveData[] savefiles;

    protected override void Start()
    {
        base.Start();

        // check current save
        CheckCurrentSave();
        savefiles = new SaveData[systemData.maxSaveFiles];

        // get colors
        active = new Color(buttons[0].image.color.r, buttons[0].image.color.g,
            buttons[0].image.color.b, buttons[0].image.color.a);
        inactive = new Color(buttons[0].image.color.r, buttons[0].image.color.g,
            buttons[0].image.color.b, 0);
    }

    protected override void Update()
    {
        base.Update();
    }

    public FileInfo[] GetSaveFileNames()
    {
        // get all save files
        String persistentDataPath = Application.persistentDataPath + "/";
        DirectoryInfo directory = new DirectoryInfo(persistentDataPath);
        FileInfo[] fileNames = directory.GetFiles("*.save");

        Debug.Log("Save Files: ");
        foreach (FileInfo fileName in fileNames)
            Debug.Log(fileName.ToString()); // logs

        return fileNames;
    }

    private void CheckCurrentSave()
    {
        // read system data
        if (File.Exists(Application.persistentDataPath + "/" +
            SYSTEM_DATA_FILE_NAME + SYSDATA_EXT))
        {
            Debug.Log("Reading system data..."); // logs

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/" +
                SYSTEM_DATA_FILE_NAME + SYSDATA_EXT, FileMode.Open);

            // get system data
            systemData = (SystemData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            // check if current save is null or not
            if (systemData.currentSave == null)
                infos[1].text = "Current Save : None";
            else
                infos[1].text = "Current Save : " + systemData.currentSave;
        }
    }

    public void ButtonClick(int btnNo)
    {
        // loop through buttons and change alphas
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i.Equals(btnNo))
                buttons[btnNo].image.color = active; // active
            else
                buttons[i].image.color = inactive; // inactive
        }

        switch (btnNo)
        {
            case 0:
                {
                    NewGame(btnNo);
                    break;
                }
            case 1:
                {
                    Continue();
                    break;
                }
            case 2:
                {
                    Load();
                    break;
                }
            default:
                {
                    // do nothing
                    break;
                }
        }
    }

    // function called for creating new save
    public void NewGame(int btnNo)
    {
        Debug.Log("New Game button clicked."); // logs

        // get save file names
        FileInfo[] fileNames = GetSaveFileNames();

        if (File.Exists(Application.persistentDataPath + "/" +
            SYSTEM_DATA_FILE_NAME + SYSDATA_EXT))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            int index = 0;

            Debug.Log(Application.persistentDataPath);
            foreach (FileInfo fileName in fileNames)
            {
                String name = Path.GetFileName(fileName.ToString());
                FileStream fileStream = File.Open(Application.persistentDataPath +
                    "/" + name, FileMode.Open);
                savefiles[index] = (SaveData)binaryFormatter.Deserialize(fileStream);

                index++;
                Debug.Log(fileStream.Name);
                fileStream.Close();
            }
        }

        // show panel
        panels[btnNo].SetActive(true);
        if (enableAnimation[btnNo]) // if enabled, show animation
            animators[btnNo].SetTrigger(animationEvents[btnNo].animationName);
    }

    public void Continue()
    {
        Debug.Log("Continue button clicked.");
    }

    public void Load()
    {
        Debug.Log("Load Save button clicked.");
    }

    public void ClosePanel(int btnNo)
    {
        panels[btnNo].SetActive(false);
    }

    // getters and setters
    public SystemData GetSystemData()
    {
        return systemData;
    }

    public SaveData[] GetSaveData()
    {
        return savefiles;
    }
}
