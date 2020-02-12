using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    GameObject Parent;
    public Color DefaultColor;
    Color Valid_Color;

    List<List<GameObject>> RowList;
    List<List<GameObject>> ColumnList;
    BoxController box_controller;
    
    public Button button;
    public Text inputfield;


    public Color Selecton_Color;
    public Color Selecton_Cell_Color;
    public int index;
    public int Row_index;
    public int Cul_index;
    public bool Isclick = false;
    public bool IsSelect = false;
    public string Value;

    string pattern = "^[1-9]";


    public void SetCell()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener (()=> OnCellSelect());

        inputfield = this.GetComponentInChildren<Text>();

        DefaultColor = this.GetComponent<Image>().color;
        Valid_Color = inputfield.color;

        Parent = this.transform.parent.gameObject;
        box_controller = Parent.GetComponent<BoxController>();


        RowList = SudokhuController.Instance.RowList;
        ColumnList = SudokhuController.Instance.ColumnList;

        button.enabled = Isclick;
        button.enabled = Isclick;
        if (!Isclick)
        {
            inputfield.text = Value;
        }
        else
        {
            inputfield.text = "";
        }
    }

    public void OnValueChange()
    {
        //inputfield.text = "";
        bool IsValid = true;
        if (Isclick)
        {

            for (int i = 0; i < box_controller.CellList.Count; i++)
            {
                GameObject cell = box_controller.CellList[i].gameObject;
                if (cell != this.gameObject)
                {
                    if (cell.GetComponent<CellController>().inputfield.text == inputfield.text)
                    {
                        IsValid = false;
                    }
                }
            }

            if (IsValid)
            {
                for (int i = 0; i < RowList[Row_index].Count; i++)
                {
                    if (RowList[Row_index][i] != this.gameObject)
                    {
                        if (RowList[Row_index][i].GetComponent<CellController>().inputfield.text == inputfield.text)
                        {
                            IsValid = false;
                        }
                    }

                }
            }

            if (IsValid)
            {
                for (int i = 0; i < ColumnList[Cul_index].Count; i++)
                {
                    if (ColumnList[Cul_index][i] != this.gameObject)
                    {
                        if (ColumnList[Cul_index][i].GetComponent<CellController>().inputfield.text == inputfield.text)
                        {
                            IsValid = false;
                        }
                    }
                }
            }

            if (!IsValid)
            {
                inputfield.color = Color.red;
            }
            else
            {
                inputfield.color = Valid_Color;
;
            }
        }
    }

    public void OnCellSelect()
    {
        if (Isclick)
        {
            for (int i = 0; i < RowList.Count; i++)
            {
                for (int j = 0; j < RowList[i].Count; j++)
                {
                    RowList[i][j].GetComponent<Image>().color = RowList[i][j].GetComponent <CellController>().DefaultColor;

                }
            }

                SudokhuController.SelectedCell = this.gameObject;

            for (int i = 0; i < box_controller.CellList.Count; i++)
                {
                    GameObject cell = box_controller.CellList[i].gameObject;
                    cell.GetComponent<Image>().color = Selecton_Cell_Color;
                }
            
            for (int i = 0; i < RowList[Row_index].Count; i++)
            {
                RowList[Row_index][i].GetComponent<Image>().color = Selecton_Cell_Color;
            }

            for (int i = 0; i < ColumnList[Cul_index].Count; i++)
            {
                ColumnList[Cul_index][i].GetComponent<Image>().color = Selecton_Cell_Color;
            }

            this.GetComponent<Image>().color = Selecton_Color;
            this.IsSelect = true;


        }
    }
}