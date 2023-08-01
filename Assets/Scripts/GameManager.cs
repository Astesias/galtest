using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextAsset dramaFile;
    private List<ScriptData> sdata;
    private int scriptIndex;

    public int eneryVaule;
    public Dictionary<string, int> favorabilityDict;

    private bool showAllDialog = false;
    private string allDialog = "";

    private void Start()
    {
        Screen.SetResolution(1024, 768, false);
    }
    private void Awake()
    {
        
        Instance = this;
        scriptIndex = 0;

        eneryVaule = 50;
        favorabilityDict = new Dictionary<string, int>() { {"薇儿", 100 } , {"海涅",100 } , { "安黛尔", 100 } };

        sdata = new List<ScriptData>();
        string dramaText = dramaFile.text;
        //Debug.Log("text: " + dramaText);
        DramaData dramaData = JsonUtility.FromJson<DramaData>(dramaText);

        Debug.Log("Name: " + dramaData.name);
        Debug.Log("Version: " + dramaData.version);
        Debug.Log("Author: " + dramaData.author);
        Debug.Log("Last Update Time: " + dramaData.lastUpdateTime);

        foreach (ScriptData entry in dramaData.data)
        {
            //Debug.Log(entry.favorabilityChange == null);
            //Debug.Log(entry.favorabilityChange.Length);
            sdata.Add(entry);
        }

        HandleData();
    }


    private void HandleData()
    {
        if (scriptIndex >= sdata.Count)
        {
            Debug.Log("Game Finish");
            return;
        }

        if (sdata[scriptIndex].soundType != 0)
        {
            PlaySound(sdata[scriptIndex].soundType);
        }

        if (sdata[scriptIndex].loadType == 1)
        {
            // bg 设置背景 加载下一条数据
            SetBGImageSprite(sdata[scriptIndex].spriteName);
            LoadNextScript();
        }
        else if (sdata[scriptIndex].loadType == 2)
        {
            HandleCharacter();
        }

    }

    public void HandleCharacter()
    {
        //显示人物
        for (int characterIndex = 0; characterIndex < sdata[scriptIndex].characterNames.Length; characterIndex++)
        {
            string name = sdata[scriptIndex].characterNames[characterIndex];
            bool isFilp = false;
            if (name.StartsWith("-")) { isFilp = true; name = name[1..]; }
            //Debug.Log($"{name}, {characterIndex}, {isFilp}");
            ShowCharacter(name, characterIndex, isFilp);
            if (sdata[scriptIndex].speaker.Length <= 1 && name != "null")
            {
                UpdateSpeakerName(name);
                allDialog += $"{name}: {sdata[scriptIndex].dialogContent}\n"; //更新聊天记录
            }
        }
        //更新发言者
        if (sdata[scriptIndex].speaker.Length >= 1) UpdateSpeakerName(sdata[scriptIndex].speaker); 
        //更新对话框
        UpdateTalkLineText(sdata[scriptIndex].dialogContent);
        //更新聊天记录
        if (sdata[scriptIndex].speaker.Length >= 1)
            allDialog += $"{sdata[scriptIndex].speaker}: {sdata[scriptIndex].dialogContent}\n";
        //更新活力值
        //ChangeEnergyVaule();
        //更新好感度
        if (sdata[scriptIndex].favorabilityChange != null)
            ChangeFavorability(sdata[scriptIndex].favorabilityChange);
        //else
        //    Debug.Log("sdata[scriptIndex].favorabilityChange == null");
    }

    public void PlaySound(int soundType)
    {
        switch (soundType)// 1cv 2bgm 3effct
        {
            case 1:
                AudioManager.Instance.PlayCv(sdata[scriptIndex].soundPath);
                break;
            case 2:
                AudioManager.Instance.PlayBgm(sdata[scriptIndex].soundPath);
                break;
            case 3:
                AudioManager.Instance.PlayEffect(sdata[scriptIndex].soundPath);
                break;
            default:
                break;
        }
    }

    private void SetBGImageSprite(string spriteName)
    {
        UIManager.Instance.SetBGImageSprite(spriteName);
    }

    public void LoadNextScript()
    {
        //Debug.Log("加载下一句");
        scriptIndex++;
        HandleData();
    }

    private void ShowCharacter(string name,int characterIndex ,bool isFilp)
    {
        UIManager.Instance.ShowCharacter(name, characterIndex, isFilp);
    }

    private void UpdateTalkLineText(string dialogContent)
    {
        UIManager.Instance.UpdateDialogText(dialogContent);
    }

    private void UpdateSpeakerName(string name)
    {
        UIManager.Instance.UpdateSpeakerName(name);
    }

    public void ChangeEnergyVaule(int changeVaule)
    {
        //if (newVaule > eneryVaule) ;
        //elif (newVaule < eneryVaule) ;

        eneryVaule += changeVaule;
        eneryVaule = (eneryVaule > 100) ? 100 : eneryVaule;
        eneryVaule = (eneryVaule < 0) ? 0 : eneryVaule;

        UpdateEnergyVaule(eneryVaule);
    }

    public void UpdateEnergyVaule(int eneryVaule)
    {
        UIManager.Instance.UpdateEnergyVaule(eneryVaule);
    }

    public void ChangeFavorability(string[] listObject)
    {
        for (int listIndex = 0; listIndex < listObject.Length; listIndex += 2)
        {
            int changeValue = Int32.Parse(listObject[listIndex + 1]);
            string name = listObject[listIndex];
            //Debug.Log(changeValue); Debug.Log(name);
            favorabilityDict[name] += changeValue;
            favorabilityDict[name] = (favorabilityDict[name] > 200) ? 200 : favorabilityDict[name];
            favorabilityDict[name] = (favorabilityDict[name] < 0) ? 0 : favorabilityDict[name];
            
        }
        //Debug.Log("c1" + favorabilityDict["c1"]); Debug.Log("c2" + favorabilityDict["c2"]);
        UpdateFavorability(favorabilityDict);
    }

    public void UpdateFavorability(Dictionary<string, int> favorabilityDict)
    {
        UIManager.Instance.UpdateFavorability(favorabilityDict);
    }

    public void ShowAllDialog(string allDialog)
    {
        UIManager.Instance.ShowAllDialog(allDialog);
    }

    public void CloseAllDialog()
    {
        UIManager.Instance.CloseAllDialog();
    }

    public void ChangeIfShowAllDialog()
    {
        showAllDialog = !showAllDialog;
        if (showAllDialog) ShowAllDialog(allDialog);
        else CloseAllDialog();
    }

}


