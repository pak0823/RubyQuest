using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    public string name;
    [TextArea]
    public string dialogue;
    public Image poImage;
}

public class OwnerTalk : MonoBehaviour
{
    public MarketScript marketScript;                                       
    public GameObject allObject;   // ��� ������Ʈ
    public GameObject portrait; // �ʻ�ȭ icon
    public TextMeshProUGUI txt_name;    // �ʻ�ȭ �̸�
    public TextMeshProUGUI txt_dialogue;    // ��ȭ ����
    public TextMeshProUGUI txt_Enter;     // Enter �ȳ� ����

    public bool isDialogue; 
    public bool isShow;     // ������Ʈ �Ѵ� ����
    private int count = 0;
    
    public Dialogue[] dialogue;

    public DataManager dataMgr;
    public bool firstShow; //ó�� ������ Ȯ���ϴ� bool ��

    public void ShowDialogue()  // UI
    {
        isDialogue = false;
        allObject.SetActive(true);  // ��� ������Ʈ ON
        count = 0;
        NextDialogue();
        Time.timeScale = 0;
    }

    private void HideDialogue()
    {
        allObject.SetActive(false); // ��� ������Ʈ OFF
        Time.timeScale = 1;
    }

    private void NextDialogue() // ���� ��ȭ 
    {
        txt_name.text = dialogue[count].name;
        txt_dialogue.text = dialogue[count].dialogue;
        portrait.GetComponent<Image>().sprite = dialogue[count].poImage.sprite;
        count++;
    }

    void Start()
    {
        dataMgr = Shared.dataMgr;
        firstShow = dataMgr.CanCenemaPlay();
        marketScript = Shared.marketScript;
        isDialogue = true;
    }

    void Update()
    {
        isShow = marketScript.PlayerVisit;    // �÷��̾ ������ ��Ҵ��� Ȯ���ϴ� ����
        if (isShow && firstShow) // ó�� ������ Ȯ���ϴ� ���ǹ� �߰�
        {
            if (isDialogue)
            {
                ShowDialogue(); // ��ȭâ ����
            }
            if (Input.GetKeyUp(KeyCode.Return))     // ���� Ű�� ������ ��
            {
                if(count < dialogue.Length) 
                {
                    NextDialogue();     // ���� ��ȭ
                }
                else
                {
                    HideDialogue();     // ��ȭ â �����
                }
            }
        }
    }
}
