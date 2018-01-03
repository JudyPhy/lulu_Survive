using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class ReadCsv {

    private string[][] Array;
    private int Row_;

    public ReadCsv(string[] array)
    {
        string[] lineOfArray = array;
        this.Array = new string[lineOfArray.Length][];
        for (int i = 0; i < lineOfArray.Length; i++)
        {
            string line = lineOfArray[i];
            //先按照逗号分割
            string[] str_d = line.Split(',');
            bool needConbine = false;
            List<string> result = new List<string>();
            string curGrid = "";
            for (int n_d = 0; n_d < str_d.Length; n_d++)
            {
                //将含有引号的部分拼接                
                if (needConbine)
                {
                    curGrid += "," + str_d[n_d];
                    if (str_d[n_d].Contains("\""))
                    {
                        int index = curGrid.IndexOf('\"');
                        curGrid = curGrid.Remove(index, 1);
                        result.Add(curGrid);
                        curGrid = "";
                        needConbine = false;
                    }
                }
                else
                {
                    if (str_d[n_d].Contains("\""))
                    {
                        curGrid = str_d[n_d];
                        int index = curGrid.IndexOf('\"');
                        curGrid = curGrid.Remove(index, 1);
                        needConbine = true;
                    }
                    else
                    {
                        result.Add(str_d[n_d]);
                    }
                }
            }
            this.Array[i] = new string[result.Count];
            for (int n = 0; n < result.Count; n++)
            {
                //if (csvName == "Equipment")
                //{
                //    Debug.LogError("result:" + result[n]);
                //}
                this.Array[i][n] = result[n];
            }
        }
        this.Row_ = this.Array.Length;
    }

    public int GetRow() {
        return this.Row_;
    }

    public string GetDataByRowAndName(int nRow, string strName) {
        if (this.Array.Length <= 0 || nRow >= this.Array.Length)
            return "";
        for (int i = 0; i < this.Array[2].Length; i++) {
            if (this.Array[2][i] == strName) {
                if (this.Array[1][i] == "int32") {
                    return string.IsNullOrEmpty(this.Array[nRow][i]) ? "0" : this.Array[nRow][i];
                } else {
                    return string.IsNullOrEmpty(this.Array[nRow][i]) ? "" : this.Array[nRow][i];
                }
            }
        }
        return "";
    }

    public string GetDataByRowAndCol(int nRow, int nCol) {
        if (this.Array.Length <= 0 || nRow >= this.Array.Length)
            return "";
        if (nCol >= this.Array[0].Length)
            return "";

        return this.Array[nRow][nCol];
    }

    public string GetDataByIdAndName(int nId, string strName) {
        if (Array.Length <= 0)
            return "";

        int nRow = Array.Length;
        int nCol = Array[0].Length;
        for (int i = 1; i < nRow; ++i) {
            string strId = string.Format("\n{0}", nId);
            if (Array[i][0] == strId) {
                for (int j = 0; j < nCol; ++j) {
                    if (Array[0][j] == strName) {
                        return Array[i][j];
                    }
                }
            }
        }
        return "";
    }



}
