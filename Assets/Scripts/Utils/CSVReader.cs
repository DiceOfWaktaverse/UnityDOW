using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public class FileReader
{
    public static string ReadOriginString(string file)
    {
        TextAsset data = Resources.Load(file) as TextAsset;
        if(data == null)
        {
            Debug.LogError("오류!");
        }
        return data.text;
    }

    public static string ReadString(string file)
    {
        try
        {
            return Decrypt(ReadOriginString(file));
        }
        catch
        {
            Debug.LogError($"정말 {file} 쓰고있습니까?");
            return ReadOriginString(file);
        }
    }

    // 암호화 필요하다면
    public static string Encrypt(string plain)
    {
        return plain;
    }

    // 복호화 필요하다면
    public static string Decrypt(string encrypt)
    {
        return encrypt;
    }
}
public class CSVReader : FileReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, string>> ReadWithUniqueKeyForFile(string file, char splitPivot = char.MinValue, string uniqueKey = "")
    {
        return ReadWithUniqueKey(ReadString(file), splitPivot, uniqueKey);
    }

    public static List<Dictionary<string, string>> ReadWithUniqueKey(string data, char splitPivot = char.MinValue, string uniqueKey = "")
    {
        var list = new List<Dictionary<string, string>>();
        var lines = Regex.Split(data, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        int startLine = 0;
        int keyColumn = 0;
        if (!string.IsNullOrEmpty(uniqueKey))
        {
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_RE);
                for (int j = 0; j < values.Length; j++)
                {
                    string key = values[j];
                    if (key.Equals(uniqueKey))
                    {
                        if (startLine != 0)
                        {
                            if (j > 0 || values[j - 1].Equals("data_start"))
                            {
                                //의도된 중복
                            }
                            else
                            {
                                Debug.LogWarning("중복된 uid 키 사용");
                            }
                        }

                        //last line의 uid 사용
                        startLine = i;
                        keyColumn = j;
                    }
                }
            }
        }

        string[] header;
        if (splitPivot == char.MinValue)
        {
            header = Regex.Split(lines[startLine], SPLIT_RE);
            for (var i = startLine + 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], SPLIT_RE);
                if (values.Length == 0
                    || values.Length <= keyColumn) continue;

                var entry = new Dictionary<string, string>();
                for (var j = 0; j < header.Length; j++)
                {
                    string value = "";
                    if (values.Length > j)
                    {
                        value = values[j];
                    }

                    value = value.Replace("\\n", "\n");
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                    entry[header[j]] = value;
                }
                list.Add(entry);
            }
        }
        else
        {
            header = lines[0].Split(splitPivot);
            for (var i = 1; i < lines.Length; i++)
            {

                var values = lines[i].Split(splitPivot);
                if (values.Length == 0 || values[0] == "") continue;

                var entry = new Dictionary<string, string>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = "";
                    if (values.Length > j)
                    {
                        value = values[j];
                    }

                    value = value.Replace("\\n", "\n");
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");

                    entry[header[j]] = value;
                }
                list.Add(entry);
            }
        }
        return list;
    }

    //원초적인 csv 파서 (단순 split 기능)
    public static List<List<string>> Read(string file, char splitPivot = char.MinValue)
    {
        var list = new List<List<string>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        for (var i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(splitPivot);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new List<string>();
            for (var j = 0; j < values.Length; j++)
            {
                string value = values[j];
                value = value.Replace("\\n", "\n");
                entry.Add(value);
            }
            list.Add(entry);
        }

        return list;
    }
}
