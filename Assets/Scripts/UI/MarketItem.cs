using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketItem : MonoBehaviour
{
    public GameObject icon;
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI price;
    public itemStatus randomitem;
    public MarketScript marketScript;
    DataManager dataMgr;
    // Start is called before the first frame update
    void Start()
    {
        randomitem = icon.GetComponentInChildren<itemStatus>();
        randomitem.InitSetting();
        icon.GetComponent<Image>().color = randomitem.data.color;
        itemname.text = randomitem.data.itemName;
        itemname.color = randomitem.data.color;
        price.text = randomitem.data.itemPrice.ToString();
        dataMgr = Shared.dataMgr;
        for (int i = 0; i < dataMgr.LastMarketList.Length; i++)
        {
            if (dataMgr.LastMarketList[i] == null)
            {
                dataMgr.LastMarketList[i] = randomitem;
                break;
            }
        }
    }

    public void buy_item()
    {
        Ui_Controller ui = Shared.gameMgr.GetComponent<Ui_Controller>();
        inven iv = Shared.gameMgr.GetComponent<inven>();
        bool EmptySloatSearch = false;
        if (ui.UseGold(int.Parse(price.text)) == true)
        {
            ui.MarketTextBox.text = "\"∞Ì∏ø¥Ÿ ƒ£±∏!\"";
            Shared.gameMgr.GetComponent<Ui_Controller>().UiUpdate();
            if (randomitem.GetComponentInChildren<itemStatus>().data.itemNameEng == "HpPotion")
            {
                Destroy(Shared.gameMgr.GetComponent<Ui_Controller>().DescriptionBox);
                Shared.gameMgr.GetComponent<Ui_Controller>().Heal(Shared.player.GetComponent<Player>().MaxHp / 2);
                Destroy(this.gameObject);
                marketScript.BuySoundPlay();
            }
            else
            {
                for (int i = 0; i < iv.inven_slots.Length; i++)
                {
                    if (iv.inven_slots[i].GetComponentInChildren<itemStatus>() == null)
                    {
                        int itemNumber = randomitem.GetComponentInChildren<itemStatus>().data.itemNumber;
                        Debug.Log(randomitem.GetComponentInChildren<itemStatus>().data.itemName);
                        EmptySloatSearch = true;
                        dataMgr.UnlockListUpdate(itemNumber);
                        Instantiate(randomitem, iv.inven_slots[i].transform);
                        Shared.gameMgr.GetComponent<inven>().updateUi();
                        Destroy(Shared.gameMgr.GetComponent<Ui_Controller>().DescriptionBox);
                        Destroy(this.gameObject);
                        break;
                    }
                }
                if (!EmptySloatSearch)
                {
                    ui.MarketTextBox.text = "\"∞°πÊ¿Ã ∞°µÊ √°¥Ÿ!\"";
                    ui.GetGold(int.Parse(price.text) / Shared.player.GoldGet); //»Ø∫“
                    return;
                }
                marketScript.BuySoundPlay();
            }
    }
    else
    {
        ui.MarketTextBox.text = "\"∞ÒµÂ∞° ∫Œ¡∑«œ¿›æ∆!\"";
    }
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
