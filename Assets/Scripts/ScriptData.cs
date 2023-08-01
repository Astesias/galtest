using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �籾����
[System.Serializable]
public class ScriptData
{
    public int loadType; // ������Դ���� 1.bg 2.char 
    public string spriteName;

    public string[] characterNames = { "null", "null", "null" };

    public string speaker = "";
    public string dialogContent;

    public int soundType; // 1.cv 2.bgm 3.effct
    public string soundPath;

    public string[] favorabilityChange = { };
}

// ȫ������
[System.Serializable]
public class DramaData
{
    public string name;
    public string version;
    public string author;
    public string lastUpdateTime;
    public ScriptData[] data;
}
