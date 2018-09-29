using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveRowPopulate : MonoBehaviour {

    [Header("Scene Controller")]
    [SerializeField]
    private TitleScreenController sceneController;
    [Space]
    [Header("Cell Prefab")]
    [SerializeField]
    private GameObject cell;
    [SerializeField]
    private Sprite notEmptyIcon;
    [SerializeField]
    private Sprite emptyIcon;
    [Space]
    [Header("Confirmation Panel")]
    [SerializeField]
    private GameObject confirmation;

    private int maxNoOfCells;
    private FileInfo[] fileDirectory;
    private SaveData[] saveFiles;

    void Start()
    {
        // initialize
        maxNoOfCells = sceneController.GetSystemData().maxSaveFiles;
        Debug.Log(maxNoOfCells);

        fileDirectory = sceneController.GetSaveFileNames();
        saveFiles = sceneController.GetSaveData();
        
        PopulateGrid();
    }

    void PopulateGrid()
    {
        GameObject newCell = new GameObject();
        TextMeshProUGUI[] texts;
        Image saveIcon;

        for (int i = 0; i < maxNoOfCells; i++)
        {
            // create a new instance of cell
            newCell = (GameObject)Instantiate(cell, transform);

            SetSaveData(newCell, i);

            texts = newCell.GetComponentsInChildren<TextMeshProUGUI>();
            SaveFileInfo(texts, i);

            saveIcon = newCell.GetComponentInChildren<Image>();
            SetSaveIcon(saveIcon, i);
        }
    }

    void SaveFileInfo(TextMeshProUGUI[] texts, int i)
    {
        String fileName = Path.GetFileNameWithoutExtension(fileDirectory[i].ToString());
        foreach (TextMeshProUGUI text in texts)
        {
            if (text.name.Equals("FileName"))
                text.text = fileName;

            Debug.Log(text.name);

            if (text.name.Equals("LastSaveDate"))
                text.text = saveFiles[i].lastSaveDate;

            if (text.name.Equals("PlayerName"))
                text.text = saveFiles[i].firstName + " " + saveFiles[i].lastName;

            if (text.name.Equals("Progress"))
                text.text = "Progress: " + saveFiles[i].gameCompletion.ToString() + "% Complete";
        }
    }

    void SetSaveIcon(Image saveIcon, int i)
    {
        if (!saveFiles[i].isSaveEmpty)
            saveIcon.sprite = notEmptyIcon;
    }

    void SetSaveData(GameObject cell, int i)
    {
        String fileName = Path.GetFileNameWithoutExtension(fileDirectory[i].ToString());
        cell.GetComponentInChildren<SaveRowClick>().SetFileName(fileName);
        cell.GetComponentInChildren<SaveRowClick>().SetSaveData(saveFiles[i]);
        cell.GetComponentInChildren<SaveRowClick>().SetConfirmationPanel(confirmation);
    }
}
