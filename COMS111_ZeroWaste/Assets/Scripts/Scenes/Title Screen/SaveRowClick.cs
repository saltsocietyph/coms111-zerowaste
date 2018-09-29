using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveRowClick : MonoBehaviour {

    private GameObject confirmation;
    private string fileName;
    private SaveData saveFile;


    public void ButtonInfo()
    {
        Debug.Log(fileName);
        Debug.Log(saveFile.isSaveEmpty);

        AddFunctions();
    }

    public void ShowConfirmation()
    {
        confirmation.SetActive(true);
    }

    public void HideConfirmation()
    {
        confirmation.SetActive(false);
    }

    public void AddFunctions()
    {
        Button[] buttons = confirmation.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.name.Equals("BtnCancel"))
                button.onClick.AddListener(HideConfirmation);
        }
    }

    // getter and setters
    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }

    public void SetSaveData(SaveData saveFile)
    {
        this.saveFile = saveFile;
    }

    public void SetConfirmationPanel(GameObject confirmation)
    {
        this.confirmation = confirmation;
    }
}
