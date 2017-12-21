using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ReadCsv {

    private string[][] Array;
    private int Row_;

    public ReadCsv(string csvName) {
        string filePath = ResourcesManager.GetCsvConfigFilePath(csvName);
        Debug.Log("Read filePath:" + filePath);
        string[] lineOfArray = File.ReadAllLines(filePath);
        this.Array = new string[lineOfArray.Length][];
        for (int i = 0; i < lineOfArray.Length; i++)
        {
            string line = lineOfArray[i];
            List<string> result = new List<string>();
            int curStartPos = 0;
            bool flag = false;
            int index = 0;
            for (int j = 0; j < line.Length; j++)
            {
                if (!flag && line[j] == '"')
                {
                    flag = true;
                    continue;
                }
                if (flag && line[j] == '"')
                {
                    flag = false;
                    string str = line.Substring(curStartPos + 1, j - curStartPos - 1);
                    //Debug.Log("111111:" + str);
                    result.Add(str);
                    curStartPos = j + 2;
                    index++;
                    j++;
                    continue;
                }
                if (!flag && line[j] == ',')
                {
                    string str = line.Substring(curStartPos, j - curStartPos);
                    //Debug.Log("2222:" + str);
                    result.Add(str);
                    curStartPos = j + 1;
                    index++;
                }
            }
            string str2 = line.Substring(curStartPos);
            //Debug.Log("last: " + str2);
            result.Add(str2);
            this.Array[i] = new string[result.Count];
            for (int n = 0; n < result.Count; n++)
            {
                this.Array[i][n] = result[n];
            }
            //Debug.LogError("result.Count: " + result.Count);
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
