using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberPedController : MonoBehaviour
{

    public void OnNumberPress(Text num)
    {
        if (SudokhuController.SelectedCell != null)
        {
            Debug.Log(num.text);
            if (num.text == "X")
            {
                SudokhuController.SelectedCell.GetComponent<CellController>().inputfield.text = "";
            }
            else
            {
                SudokhuController.SelectedCell.GetComponent<CellController>().inputfield.text = num.text;
                SudokhuController.SelectedCell.GetComponent<CellController>().OnValueChange();
            }
        }

    }
}
