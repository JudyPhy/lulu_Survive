using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class ReadCsv {

    private string[][] Array;
    private int Row_;

    public ReadCsv(string csvName) {
        string filePath = ResourcesManager.GetCsvConfigFilePath(csvName);
        MyLog.Log("Read filePath:" + filePath);
        string[] lineOfArray = File.ReadAllLines(filePath);
        this.Array = new string[lineOfArray.Length][];
        for (int i = 0; i < lineOfArray.Length; i++)
        {
            string line = lineOfArray[i];
            Regex regex = new Regex(",\"");
            string[] str_y1 = regex.Split(line);
            List<string> result = new List<string>();
            for (int n_y1 = 0; n_y1 < str_y1.Length; n_y1++)
            {
                regex = new Regex("\",");
                string[] str_y2 = regex.Split(str_y1[n_y1]);
                for (int n_y2 = 0; n_y2 < str_y2.Length; n_y2++)
                {
                    string[] str_d = str_y2[n_y2].Split(',');
                    for (int n_d = 0; n_d < str_d.Length; n_d++)
                    {
                        //Debug.LogError("str_d:" + str_d[n_d]);
                        result.Add(str_d[n_d]);
                    }
                }
            }
            this.Array[i] = new string[result.Count];
            for (int n = 0; n < result.Count; n++)
            {
                this.Array[i][n] = result[n];
            }
            //MyLog.LogError("result.Count: " + result.Count);
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
