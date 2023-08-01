using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Image imgBG;
    public Image[] imgCharacterList;

    public Text speakerName;
    public Text textTalkLine;
    public Text textFavorTab;
    public Text textAllDialog;

    public GameObject allDialogFather;
    public GameObject talkLineFather;
    public GameObject favorTabather;
    public GameObject characterListFather;

    public void Awake()
    {
        Instance = this;
    }

    public void SetBGImageSprite(string spriteName)
    {
        imgBG.sprite = Resources.Load<Sprite>("Sprites/BG/" + spriteName);
    }

    public void ShowCharacter(string name, int characterIndex, bool isFilp)
    {

        if (name != "null")
        {
            imgCharacterList[characterIndex].sprite = Resources.Load<Sprite>("Sprites/Characters/" + name);
            imgCharacterList[characterIndex].gameObject.SetActive(true);
            imgCharacterList[characterIndex].transform.eulerAngles = new Vector3(0, isFilp ? 180 : 0, 0);
        }
        else
            imgCharacterList[characterIndex].gameObject.SetActive(false);
    }

    public void UpdateDialogText(string dialogContent)
    {
        talkLineFather.SetActive(true);
        textTalkLine.text = dialogContent;
    }

    public void UpdateSpeakerName(string name)
    {
        speakerName.text = name;
    }

    public void UpdateEnergyVaule(int eneryVaule)
    {

    }

    public void UpdateFavorability(Dictionary<string, int> favorabilityDict)
    {
        favorTabather.SetActive(true);
        string infoText = "";
        foreach (KeyValuePair<string, int> map in favorabilityDict)
        {
            string name = map.Key;
            int favorability = map.Value;
            //Debug.Log("key" + name);
            infoText += $"{name} {favorability} ";
        }
        infoText.Trim('\n');
        textFavorTab.text = infoText;
    }

    public void ShowAllDialog(string allDialog)
    {
        allDialogFather.SetActive(true);
        talkLineFather.SetActive(false);
        favorTabather.SetActive(false);
        characterListFather.SetActive(false);
        textAllDialog.text = allDialog;
    }

    public void CloseAllDialog()
    {
        talkLineFather.SetActive(true);
        allDialogFather.SetActive(false);
        favorTabather.SetActive(true);
        characterListFather.SetActive(true);
    }
}