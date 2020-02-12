using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[ExecuteInEditMode]


public class SudokhuController : MonoBehaviour
{
    public static SudokhuController Instance;
    public static GameObject SelectedCell;

    public GameObject parent;

    public List<string> RowValue;
    public List<string> ColumnValue;
    public List<string> BoxValue;
    // public List<string> rows;
    public List<BoxController> BoxList;

    public List<GameObject> Row1;
    public List<GameObject> Row2;
    public List<GameObject> Row3;
    public List<GameObject> Row4;
    public List<GameObject> Row5;
    public List<GameObject> Row6;
    public List<GameObject> Row7;
    public List<GameObject> Row8;
    public List<GameObject> Row9;
    public List<List<GameObject>> RowList;

    public List<GameObject> Column1;
    public List<GameObject> Column2;
    public List<GameObject> Column3;
    public List<GameObject> Column4;
    public List<GameObject> Column5;
    public List<GameObject> Column6;
    public List<GameObject> Column7;
    public List<GameObject> Column8;
    public List<GameObject> Column9;
    public List<List<GameObject>>ColumnList;




    public List<int> TempNumbers;

    public int row = 0, col = 0, i = 0;
    int index = 0;
    int box = 0;
    int num = 0;
    // public List<int> ColList;

    DateTime date1, date2;

    public void Start()
    {
        RectTransform parentRect = parent.GetComponent<RectTransform>();

        GridLayoutGroup gridLayout = parent.GetComponent<GridLayoutGroup>();
        float BoxSize = parentRect.rect.width / 3.2f;
        Debug.Log(BoxSize);

        float cellsize = BoxSize / 3f;

        gridLayout.cellSize = new Vector2(BoxSize, BoxSize);

        for (int i = 0; i < BoxList.Count; i++)
        {
            GridLayoutGroup gridLayout1 = BoxList[i].GetComponent<GridLayoutGroup>();

            gridLayout1.cellSize = new Vector2(cellsize, cellsize);
        }

    }

    public void OnEnable()
    {
        if (Instance == null)
            Instance = this;

        date1 = DateTime.Now;
        RowList = new List<List<GameObject>>();
        RowList.Add(Row1);
        RowList.Add(Row2);
        RowList.Add(Row3);
        RowList.Add(Row4);
        RowList.Add(Row5);
        RowList.Add(Row6);
        RowList.Add(Row7);
        RowList.Add(Row8);
        RowList.Add(Row9);

        ColumnList = new List<List<GameObject>>();
        ColumnList.Add(Column1);
        ColumnList.Add(Column2);
        ColumnList.Add(Column3);
        ColumnList.Add(Column4);
        ColumnList.Add(Column5);
        ColumnList.Add(Column6);
        ColumnList.Add(Column7);
        ColumnList.Add(Column8);
        ColumnList.Add(Column9);

        StartGame();
    }

    public void StartGame()
    {
        GenrateRows();
    }

        void GenrateRows()
    {

    Label4:
        int[,] IntArray = new int[9, 9];

        box = 0;
        index = 0;

        for (int i = 0; i < RowValue.Count; i++)
        {
            RowValue[i] = ""; 
        }
        for (int i = 0; i < ColumnValue.Count; i++)
        {
            ColumnValue[i] = "";
        }
        for (int i = 0; i < BoxValue.Count; i++)
        {
            BoxValue[i] = "";
        }


        for (int i = 0; i < IntArray.GetLength(0); i++)
        {
            TempNumbers.Clear();

            

            for (int j = 0; j < IntArray.GetLength(1); j++)
            {
                AssignTempNumbers(TempNumbers);

                Sufflenumber(TempNumbers);

                if (i > 0)
                {
                Label1:
                    for (int x = 0; x < TempNumbers.Count; x++)
                    {
                        if (BoxValue[box].Contains(TempNumbers[x].ToString()))
                        {
                            TempNumbers.RemoveAt(x);
                            goto Label1;
                        }
                    }

                }

                if (RowValue.Count > i)
                {
                Label2:
                    for (int x = 0; x < TempNumbers.Count; x++)
                    {
                        if (RowValue[i].Contains(TempNumbers[x].ToString()))
                        {
                            TempNumbers.RemoveAt(x);
                            goto Label2;
                        }
                    }
                }

                if (ColumnValue.Count > j)
                {
                Label3:
                    for (int x = 0; x < TempNumbers.Count; x++)
                    {
                        if (ColumnValue[j].Contains(TempNumbers[x].ToString()))
                        {
                            TempNumbers.RemoveAt(x);
                            goto Label3;
                        }
                    }
                }

                if (TempNumbers.Count != 0)
                {
                    num = TempNumbers[UnityEngine.Random.Range(0, TempNumbers.Count)];
                }
                else
                {
                    goto Label4;
                }

                IntArray[i, j] = num;

                TempNumbers.Remove(num);

                //Value Assign in InputBox
               // AllBoxs[box][index].text = IntArray[i, j].ToString();

                RowValue[i] += IntArray[i, j].ToString();

                ColumnValue[j] += IntArray[i, j].ToString();

                BoxValue[box] += IntArray[i, j].ToString();

                BoxList[box].CellList[index].Row_index = i;
                BoxList[box].CellList[index].Cul_index = j;


                if (j % 3 == 2)
                {
                    if (i % 3 == 2 && j == index)
                    {
                        box++;
                        index = 0;
                    }
                    else
                    {
                        if (box % 3 == 2)
                        {
                            box -= 2;
                            index++;
                        }
                        else
                        {
                            box++;
                            if (index > 0)
                            {
                                index -= 2;
                            }
                            else
                            {
                                index = 0;
                            }

                        }
                    }
                }
                else
                {
                    index++;
                }
            }

            

        }

        for (int i = 0; i < BoxList.Count; i++)
        {
            BoxList[i].BoxValue = BoxValue[i];
            BoxList[i].BoxId = i + 1;
            BoxList[i].gameObject.SetActive(false);
            BoxList[i].gameObject.SetActive(true);
        }

        date2 = DateTime.Now;
        TimeSpan travelTime = date2 - date1;
        Debug.Log("travelTime Minutes : " + travelTime.Minutes + " Second : " + travelTime.Seconds + " mili Sec : " + travelTime.Milliseconds );


        // set numbers in inputfield
        /* for (int x = 0; x < IntArray.GetLength(0); x++)
         {
             for (int j = 0; j < IntArray.GetLength(1); j++)
             {
                 AllBoxs[box][index].text = IntArray[x, j].ToString();
                 if (j==0)
                 {
                     RowValue.Add(IntArray[x, j].ToString());
                 }
                 else
                 {
                     RowValue[x] += IntArray[x, j].ToString();
                 }
                 if (ColumnValue.Count != IntArray.GetLength(1))
                 {
                     ColumnValue.Add(IntArray[x, j].ToString());
                 } else
                 {
                     ColumnValue[j] += IntArray[x, j].ToString();
                 }

                 if (j % 3 == 2)
                 {
                     if (x % 3 == 2 && j == index)
                     {
                         box++;
                         index = 0;
                     }
                     else
                     {
                         if (box % 3 == 2)
                         {
                             box -= 2;
                             index++;
                         }
                         else
                         {
                             box++;
                             if (index > 0)
                             {
                                 index -= 2;
                             }
                             else
                             {
                                 index = 0;
                             }

                         }
                     }
                 }
                 else
                 {
                     index++;
                 }
             }
         }*/




        /* Label3:

            RowValue.Clear();
            TempNumbers.Clear();
            int i = 0;

            while (i < 9)
            {

                string strrow="";
                if (RowValue.Count == 0)
                {
                    //strrow = "123456789";

                    AssignTempNumbers(TempNumbers);

                    Sufflenumber(TempNumbers);

                    for (int x = 0; x < TempNumbers.Count; x++)
                    {
                        strrow += TempNumbers[x].ToString();
                    }
                }
                else if (RowValue.Count == 1)
                {
                    // strrow = "574819632";

                    int rm;


                    List<string> str = RowValue[i - 1].Select(c => c.ToString()).ToList();



                    //1;
                    // Debug.Log("str " + string.Join("",str));
                    rm = UnityEngine.Random.Range(3, 5);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // // Debug.Log("1 " + strrow);

                    //2
                    // // Debug.Log("str " + string.Join("", str));
                    rm = UnityEngine.Random.Range(5, 7);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // // Debug.Log("2 " + strrow);

                    //3
                    // Debug.Log("str " + string.Join("", str));
                    rm = UnityEngine.Random.Range(3, 4);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // Debug.Log("3 " + strrow);

                    //4
                    // Debug.Log("str " + string.Join("", str));
                    rm = UnityEngine.Random.Range(4, 5);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // Debug.Log("4 " + strrow);

                    //5
                    // Debug.Log("str " + string.Join("", str));
                    rm = UnityEngine.Random.Range(0, 3);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // Debug.Log("5 " + strrow);

                    //6
                    // Debug.Log("str " + string.Join("", str));
                    rm = str.Count - 1;
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // Debug.Log("6 " + strrow);

                    //7
                    // Debug.Log("str " + string.Join("", str));
                    rm = UnityEngine.Random.Range(0, str.Count);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // Debug.Log("7 " + strrow);

                    //8
                    // Debug.Log("str " + string.Join("", str));
                    rm = UnityEngine.Random.Range(0, str.Count);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // Debug.Log("8 " + strrow);

                    //9
                    // Debug.Log("str " + string.Join("", str));
                    rm = UnityEngine.Random.Range(0, str.Count);
                    strrow += str[rm];
                    str.RemoveAt(rm);
                    // Debug.Log("9 " + strrow);
                }
                else if (RowValue.Count == 2)
                {
                    //strrow = "689237145";

                    string Temp, str = "";
                    Temp = RowValue[i-2].Substring(0, 3);
                    Temp += RowValue[i-1].Substring(0, 3);
                    // Debug.Log("Temp " + Temp);
                    for (int x = 1; x <= 9; x++)
                    {
                        if (!Temp.Contains(x.ToString()))
                        {
                            str += x.ToString();
                        }
                    }
                    strrow = str;

                    // Debug.Log(str);
                    str = "";
                    Temp = RowValue[i - 2].Substring(3, 3);
                    Temp += RowValue[i - 1].Substring(3, 3);
                    // Debug.Log("Temp " + Temp);
                    for (int x = 1; x <= 9; x++)
                    {
                        if (!Temp.Contains(x.ToString()))
                        {
                            str += x.ToString();
                        }
                    }
                    strrow += str;
                    // Debug.Log(str);

                    str = "";
                    Temp = RowValue[i - 2].Substring(6, 3);
                    Temp += RowValue[i - 1].Substring(6, 3);
                    // Debug.Log("Temp " + Temp);
                    for (int x = 1; x <= 9; x++)
                    {
                        if (!Temp.Contains(x.ToString()))
                        {
                            str += x.ToString();
                        }
                    }
                    strrow += str;
                    // Debug.Log(str);


                }
                else if (RowValue.Count == 3)
                {
                    // strrow = "861392574";
                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    for (int j = 0; j < rows.Count; j++)
                    {
                        TempNumbers.Clear();
                        AssignTempNumbers(TempNumbers);
                    Label1:
                        for (int k = 0; k < TempNumbers.Count; k++)
                        {
                            if (rows[j].Contains(TempNumbers[k].ToString()) || strrow.Contains(TempNumbers[k].ToString()))
                            {
                                TempNumbers.RemoveAt(k);
                                goto Label1;

                            }
                        }
                        if (TempNumbers.Count != 0)
                        {
                            int num = UnityEngine.Random.Range(0, TempNumbers.Count);
                            strrow += TempNumbers[num].ToString();
                        }
                    }

                    if (strrow.Length != 9)
                    {
                        TempNumbers.Clear();
                        AssignTempNumbers(TempNumbers);
                        for (int x = 0; x < TempNumbers.Count; x++)
                        {
                            if (!strrow.Contains(TempNumbers[x].ToString()))
                            {
                                strrow += TempNumbers[x].ToString();
                            }
                        }
                    }

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    for (int x = 0; x < rows.Count; x++)
                    {
                        if (rows[x].Contains(strrow.Substring(x, 1)))
                        {
                            Debug.Log("Exist 3");
                            goto Label3;
                        }
                    }
                }
                else if (RowValue.Count == 4)
                {
                    //strrow = "861392574";

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    List<string> Temp1 = new List<string>();

                    Temp1.Add(RowValue[i - 1].Substring(0, 3));
                    Temp1.Add(RowValue[i - 1].Substring(3, 3));
                    Temp1.Add(RowValue[i - 1].Substring(6, 3));

                    int tempIndex = 0;

                    for (int x = 0; x < rows.Count; x++)
                    {

                        for (int j = 0; j < Temp1[tempIndex].Length; j++)
                        {
                            if (!rows[x].Contains(Temp1[tempIndex].Substring(j, 1)))
                            {
                                rows[x] += Temp1[tempIndex].Substring(j, 1);
                            }
                        }
                        if (x % 3 == 2 && tempIndex != 2)
                        {
                            tempIndex++;
                        }
                    }

                    for (int j = 0; j < rows.Count; j++)
                    {
                        TempNumbers.Clear();
                        List<int> templist = new List<int>();
                        AssignTempNumbers(TempNumbers);
                        templist = TempNumbers;

                    Label2:
                        for (int k = 0; k < TempNumbers.Count; k++)
                        {
                            if (rows[j].Contains(TempNumbers[k].ToString()) || strrow.Contains(TempNumbers[k].ToString()))
                            {
                                templist.Remove(TempNumbers[k]);
                                goto Label2;
                            }

                        }
                        if (templist.Count != 0)
                        {
                            int num = UnityEngine.Random.Range(0, templist.Count);
                            strrow += templist[num].ToString();
                        }
                    }

                    if (strrow.Length != 9)
                    {
                        TempNumbers.Clear();
                        AssignTempNumbers(TempNumbers);
                        for (int x = 0; x < TempNumbers.Count; x++)
                        {
                            if (!strrow.Contains(TempNumbers[x].ToString()))
                            {
                                strrow += TempNumbers[x].ToString();
                            }
                        }
                    }

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    for (int x = 0; x < rows.Count; x++)
                    {
                        if (rows[x].Contains(strrow.Substring(x, 1)))
                        {
                            Debug.Log("Exist 4");
                            goto Label3;
                        }
                    }


                    if (Temp1[0].Contains(strrow.Substring(0, 1)) ||
                         Temp1[0].Contains(strrow.Substring(1, 1)) ||
                         Temp1[0].Contains(strrow.Substring(2, 1)) ||
                         Temp1[1].Contains(strrow.Substring(3, 1)) ||
                         Temp1[1].Contains(strrow.Substring(4, 1)) ||
                         Temp1[1].Contains(strrow.Substring(5, 1)) ||
                         Temp1[2].Contains(strrow.Substring(6, 1)) ||
                         Temp1[2].Contains(strrow.Substring(7, 1)) ||
                         Temp1[2].Contains(strrow.Substring(8, 1)))
                    {
                        Debug.Log("Exist 4");
                        goto Label3;
                    }

                }
                else if (RowValue.Count == 5)
                {
                    string Temp, str = "";
                    Temp = RowValue[i - 2].Substring(0, 3);
                    Temp += RowValue[i - 1].Substring(0, 3);
                    // Debug.Log("Temp " + Temp);
                    for (int x = 1; x <= 9; x++)
                    {
                        if (!Temp.Contains(x.ToString()))
                        {
                            str += x.ToString();
                        }
                    }
                    strrow = str;

                    // Debug.Log(str);
                    str = "";
                    Temp = RowValue[i - 2].Substring(3, 3);
                    Temp += RowValue[i - 1].Substring(3, 3);
                    // Debug.Log("Temp " + Temp);
                    for (int x = 1; x <= 9; x++)
                    {
                        if (!Temp.Contains(x.ToString()))
                        {
                            str += x.ToString();
                        }
                    }
                    strrow += str;
                    // Debug.Log(str);

                    str = "";
                    Temp = RowValue[i - 2].Substring(6, 3);
                    Temp += RowValue[i - 1].Substring(6, 3);
                    // Debug.Log("Temp " + Temp);
                    for (int x = 1; x <= 9; x++)
                    {
                        if (!Temp.Contains(x.ToString()))
                        {
                            str += x.ToString();
                        }
                    }
                    strrow += str;

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    for (int x = 0; x < rows.Count; x++)
                    {
                        if (rows[x].Contains(strrow.Substring(x, 1)))
                        {
                            Debug.Log("Exist 5");
                            goto Label3;
                        }
                    }
                }
                else if (RowValue.Count == 6)
                {
                    // strrow = "861392574";
                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    for (int j = 0; j < rows.Count; j++)
                    {
                        TempNumbers.Clear();
                        AssignTempNumbers(TempNumbers);
                    Label1:
                        for (int k = 0; k < TempNumbers.Count; k++)
                        {
                            if (rows[j].Contains(TempNumbers[k].ToString()) || strrow.Contains(TempNumbers[k].ToString()))
                            {
                                TempNumbers.RemoveAt(k);
                                goto Label1;

                            }
                        }
                        if (TempNumbers.Count != 0)
                        {
                            int num = UnityEngine.Random.Range(0, TempNumbers.Count);
                            strrow += TempNumbers[num].ToString();
                        }
                    }

                    if (strrow.Length != 9)
                    {
                        TempNumbers.Clear();
                        AssignTempNumbers(TempNumbers);
                        for (int x = 0; x < TempNumbers.Count; x++)
                        {
                            if (!strrow.Contains(TempNumbers[x].ToString()))
                            {
                                strrow += TempNumbers[x].ToString();
                            }
                        }
                    }

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    for (int x = 0; x < rows.Count; x++)
                    {
                        if (rows[x].Contains(strrow.Substring(x, 1)))
                        {
                            Debug.Log("Exist 6");
                            goto Label3;
                        }
                    }
                }
                else if (RowValue.Count == 7)
                {
                    //strrow = "861392574";

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    List<string> Temp1 = new List<string>();

                    Temp1.Add(RowValue[i - 1].Substring(0, 3));
                    Temp1.Add(RowValue[i - 1].Substring(3, 3));
                    Temp1.Add(RowValue[i - 1].Substring(6, 3));

                    int tempIndex = 0;

                    for (int x = 0; x < rows.Count; x++)
                    {

                        for (int j = 0; j < Temp1[tempIndex].Length; j++)
                        {
                            if (!rows[x].Contains(Temp1[tempIndex].Substring(j, 1)))
                            {
                                rows[x] += Temp1[tempIndex].Substring(j, 1);
                            }
                        }
                        if (x % 3 == 2 && tempIndex != 2)
                        {
                            tempIndex++;
                        }
                    }

                    for (int j = 0; j < rows.Count; j++)
                    {
                        TempNumbers.Clear();
                        List<int> templist = new List<int>();
                        AssignTempNumbers(TempNumbers);
                        templist = TempNumbers;

                    Label5:
                        for (int k = 0; k < TempNumbers.Count; k++)
                        {
                            if (rows[j].Contains(TempNumbers[k].ToString()) || strrow.Contains(TempNumbers[k].ToString()))
                            {
                                templist.Remove(TempNumbers[k]);
                                goto Label5;
                            }

                        }
                        if (templist.Count != 0)
                        {
                            int num = UnityEngine.Random.Range(0, templist.Count);
                            strrow += templist[num].ToString();
                        }
                    }

                    if (strrow.Length != 9)
                    {
                        TempNumbers.Clear();
                        AssignTempNumbers(TempNumbers);
                        for (int x = 0; x < TempNumbers.Count; x++)
                        {
                            if (!strrow.Contains(TempNumbers[x].ToString()))
                            {
                                strrow += TempNumbers[x].ToString();
                            }
                        }
                    }

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    for (int x = 0; x < rows.Count; x++)
                    {
                        if (rows[x].Contains(strrow.Substring(x, 1)))
                        {
                            Debug.Log("Exist 7");
                            goto Label3;
                        }
                    }


                    if (Temp1[0].Contains(strrow.Substring(0, 1)) ||
                         Temp1[0].Contains(strrow.Substring(1, 1)) ||
                         Temp1[0].Contains(strrow.Substring(2, 1)) ||
                         Temp1[1].Contains(strrow.Substring(3, 1)) ||
                         Temp1[1].Contains(strrow.Substring(4, 1)) ||
                         Temp1[1].Contains(strrow.Substring(5, 1)) ||
                         Temp1[2].Contains(strrow.Substring(6, 1)) ||
                         Temp1[2].Contains(strrow.Substring(7, 1)) ||
                         Temp1[2].Contains(strrow.Substring(8, 1)))
                    {
                        Debug.Log("Exist 7");
                        goto Label3;
                    }

                }
                else if (RowValue.Count == 8)
                {
                    List<string> Temp1 = new List<string>();

                    Temp1.Add(RowValue[i - 1].Substring(0, 3));
                    Temp1.Add(RowValue[i - 1].Substring(3, 3));
                    Temp1.Add(RowValue[i - 1].Substring(6, 3));
                    Temp1[0] += (RowValue[i - 2].Substring(0, 3));
                    Temp1[1] += (RowValue[i - 2].Substring(3, 3));
                    Temp1[2] += (RowValue[i - 2].Substring(6, 3));

                    rows.Clear();

                    for (int j = 0; j < RowValue.Count; j++)
                    {
                        for (int k = 0; k < RowValue[j].Length; k++)
                        {
                            if (rows.Count != 9)
                            {
                                rows.Add(RowValue[j].Substring(k, 1));
                            }
                            else
                            {
                                rows[k] += RowValue[j].Substring(k, 1);
                            }
                        }

                    }

                    List<int> TempList = new List<int>();
                    strrow = "";
                    for (int x = 0; x < rows.Count; x++)
                    {
                        TempList.Clear();
                        AssignTempNumbers(TempList);
                        for (int y= 0; y < TempList.Count; y++)
                        {

                            if (!rows[x].Contains(TempList[y].ToString()))
                            {
                                strrow += TempList[y];
                                //Debug.Log(rows[x] +  " " + TempList[y]);
                            }

                        }

                    }

                    if (Temp1[0].Contains(strrow.Substring(0, 1)) ||
                         Temp1[0].Contains(strrow.Substring(1, 1)) ||
                         Temp1[0].Contains(strrow.Substring(2, 1)) ||
                         Temp1[1].Contains(strrow.Substring(3, 1)) ||
                         Temp1[1].Contains(strrow.Substring(4, 1)) ||
                         Temp1[1].Contains(strrow.Substring(5, 1)) ||
                         Temp1[2].Contains(strrow.Substring(6, 1)) ||
                         Temp1[2].Contains(strrow.Substring(7, 1)) ||
                         Temp1[2].Contains(strrow.Substring(8, 1)))
                    {
                        Debug.Log("Exist 8");
                        goto Label3;
                    }


                }


                RowValue.Add(strrow);
                    i++;
            }

            int index = 0;
            int box = 0;
            // set numbers in inputfield
            for (int x = 0; x < RowValue.Count; x++)
            {

                List<string> strlist = RowValue[x].Select(c => c.ToString()).ToList();

                for (int j = 0; j < strlist.Count; j++)
                {
                    AllBoxs[box][index].text = strlist[j];

                    if (j % 3 == 2)
                    {
                        if (x % 3 == 2 && j== strlist.Count-1)
                        { 
                            box++;
                            index = 0;
                        }
                        else
                        {
                            if (box % 3 == 2)
                            {
                                box -= 2;
                                index++;
                            }
                            else
                            {
                                box++;
                                if (index > 0)
                                {
                                    index -= 2;
                                }
                                else
                                {
                                    index = 0;
                                }

                            }
                        }
                    }
                    else
                    {
                        index++;
                    }
                }



            }*/
    }

    void Sufflenumber(List<int> Numlist)
    {
        Numlist.Shuffle();
    }


    public void AssignTempNumbers(List<int> Numbers)
    {
        for (int i = 1; i < 10; i++)
        {
            Numbers.Add(i);
        }
    }
}

public static class randomlist
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}