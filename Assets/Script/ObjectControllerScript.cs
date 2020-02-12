using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectControllerScript : MonoBehaviour
{
    public static ObjectControllerScript Instance;
    public GameObject HomeScreen;
    public GameObject GamePlayScreen;
    public GameObject SettingScreen;
    public GameObject PauseScreen;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void PlayButtonPress()
    {
        HomeScreen.SetActive(false);
        GamePlayScreen.SetActive(true);
    }

    public void SettingButtonPress()
    {
        GamePlayScreen.GetComponent<CanvasGroup>().alpha = 0f;
        SettingScreen.SetActive(true);
    }

    public void SettingBackbuttonPress()
    {
        GamePlayScreen.GetComponent<CanvasGroup>().alpha = 1f;
        SettingScreen.SetActive(false);
    }

    public void RefreshButtonPress()
    {
        for (int i = 0; i < SudokhuController.Instance.RowList.Count; i++)
        {
            for (int j = 0; j < SudokhuController.Instance.RowList[i].Count; j++)
            {
                GameObject cell = SudokhuController.Instance.RowList[i][j];
                cell.GetComponent<Image>().color = cell.GetComponent<CellController>().DefaultColor;
                if (cell.GetComponent<CellController>().Isclick)
                {
                    cell.GetComponent<CellController>().inputfield.text = "";
                }
                
            }
        }
        SudokhuController.SelectedCell = null;
        PauseScreen.SetActive(false);
        GamePlayScreen.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void StartNewGame()
    {

        PauseScreen.SetActive(false);
        SudokhuController.Instance.StartGame();
        GamePlayScreen.GetComponent<CanvasGroup>().alpha = 1f;
    }

    public void GamePauseScreen()
    {
        GamePlayScreen.GetComponent<CanvasGroup>().alpha = 0f;
        PauseScreen.SetActive(true);

    }

    public void GamePauseScreenClose()
    {
        GamePlayScreen.GetComponent<CanvasGroup>().alpha = 1f;
        PauseScreen.SetActive(false);

    }

}
