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
    [Header("Panels")]
    [SerializeField]
    private GameObject[] panels;
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

    protected override void Start()
    {
        base.Start();

        // check current save
        CheckCurrentSave();

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

    public void CheckCurrentSave()
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

            // get current save
            if (systemData.currentSave == null)
            {
                infos[1].text = "Current Save : None";
            }
            else
            {
                Debug.Log("Function to get file name not yet done.");
            }
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
                    NewGame();
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
    public void NewGame()
    {
        Debug.Log("New Game button clicked.");

        // show panel
        panels[0].SetActive(true);
    }

    public void Continue()
    {
        Debug.Log("Continue button clicked.");
    }

    public void Load()
    {
        Debug.Log("Load Save button clicked.");
    }
}
