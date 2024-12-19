using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OptionManager : MonoBehaviour
{
    public GameObject option_panel;
    private bool open_option = false;

    public GameObject Stacks;
    public TextMeshProUGUI[] SelectStacks;

    public GameObject Timer;
    public TextMeshProUGUI PlayTimerText;
    public float TotalPlayTime = 0f;
    public bool Playing = false;

    public int minutes;
    public int seconds;

    private void Awake()
    {
        if (Shared.optionMgr == null)
        {
            Shared.optionMgr = this;
            DontDestroyOnLoad(Shared.optionMgr);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Playing)
        {
            Timer.SetActive(true);
            TotalPlayTime += Time.deltaTime;
            UpdateTimerText();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Shared.loading != null)
                {
                    if (Shared.loading.DoLoading)
                    {
                        return; 
                    }
                }
                if (!open_option)
                {
                    if(!Shared.marketScript.MarketOpen)
                    {
                        option_panel.SetActive(true);
                        open_option = true;
                        Select_Stack_Setting();
                    }
                }
                else
                {
                    option_panel.SetActive(false);
                    open_option = false;
                }
            }
        }
        else
        {
            Timer.SetActive(false);
        }
    }

    void Select_Stack_Setting()
    {
        if (Shared.player != null)
        {
            Timer.SetActive(true);
            Stacks.SetActive(true);
            int[] getLevel = new int[9];
            getLevel = Shared.player.returnPlayerSelectLevel();
            for (int i = 0; i < getLevel.Length; i++)
            {
                if (getLevel[i] == 3)
                {
                    SelectStacks[i].text = "MAX";
                    SelectStacks[i].fontSize = 30;
                }
                else
                {
                    SelectStacks[i].text = getLevel[i].ToString();
                }
            }
        }
    }

    void UpdateTimerText()
    {
        minutes = Mathf.FloorToInt(TotalPlayTime / 60);
        seconds = Mathf.FloorToInt(TotalPlayTime % 60);

        PlayTimerText.text = "플레이 시간 : "+string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string returnTimerText()
    {
        string returnStr;
        int hours;
        int min;

        hours = Mathf.FloorToInt(TotalPlayTime / 60)/60;
        min = Mathf.FloorToInt(TotalPlayTime / 60) % 60;

        returnStr = string.Format("{0}시간 {1}분", hours, min);
        return returnStr;
    }

}
