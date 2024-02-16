using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorIcon : MonoBehaviour
{
    public int Doortype;
    public int IconIdx;
    private int _selectDoorIdx;
    private Button button;

    private void Start()
    {
        _selectDoorIdx = Data.GameData.InGameData.SelectDoorIdx;
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickIcon);
    }

    public void SetColorAndShape()
    {
    }

    public void SelectUI()
    {
        if(_selectDoorIdx == IconIdx)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }

    void ClickIcon()
    {
        Data.GameData.InGameData.SelectDoorIdx = IconIdx;
        _selectDoorIdx = Data.GameData.InGameData.SelectDoorIdx;
        SelectUI();
        DoorUIHandler.DoorFocusUIH.ChangeSelectDoor();
    }

    void Update()
    {
        if (_selectDoorIdx != Data.GameData.InGameData.SelectDoorIdx)
            SelectUI();
    }
}
