using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxController : MonoBehaviour
{
    public int BoxId;
    public string BoxValue;
    public List<CellController> CellList;
    public int Disablenumbers;
    public string Disable_Numbers;

    // Start is called before the first frame update
    public void OnEnable()
    {
        Disablenumbers = Random.Range(6,8);
        List<int> TempList = new List<int>();
        SudokhuController.Instance.AssignTempNumbers(TempList);
        Disable_Numbers = "";
        for (int i = 0; i < Disablenumbers; i++)
        {
            int num = Random.Range(0, TempList.Count);
            Disable_Numbers += num;
            TempList.RemoveAt(num);

        }

        for (int i = 0; i < CellList.Count; i++)
        {
            if (Disable_Numbers.Contains(i.ToString()))
            {
                CellList[i].Isclick = true;
            }
            else
            {
                CellList[i].Isclick = false;
            }
            CellList[i].Value = BoxValue.Substring(i, 1);
            CellList[i].index = i + 1;
            CellList[i].SetCell();
            
        }

        
    }

    
}
