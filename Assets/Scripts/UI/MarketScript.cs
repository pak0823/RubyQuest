using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class UnlockItem
{
    public bool Unlock;
}

[System.Serializable]
public class UnlockItemList
{
    public Dictionary<string, UnlockItem> Items;
}

[System.Serializable]
public class Unlock
{
    public string ItemName;
    public bool isUnlock;
}

[System.Serializable]
public class UnlockList
{
    public List<Unlock> items;
}

public class MarketScript : MonoBehaviour
{
    public GameObject marketListObject;
    public GameObject marketItemObject;
    public GameObject marketUiObject;
    public GameObject KeyUiObject;
    public bool MarketOpen = false;
    public bool PlayerVisit = false;
    public bool Remain;
    public List<int> RandomList;

    public UnlockList ItemFromJson;

    public AudioClip SellSound;
    public AudioClip BuySound;
    public AudioSource sfx;

    public DataManager dataMgr;
    void Awake()
    {
        Shared.marketScript = this; 
        marketUiObject = Shared.marketui.gameObject;
        marketListObject = Shared.marketui.item_list;
    }


    void Start()
    {
        dataMgr = Shared.dataMgr;
        string PlayerPath = Application.dataPath + "/Resources";
        SaveData saveData = new SaveData();
        //FromJson �κ�
        string playerJson = File.ReadAllText(PlayerPath + "/PlayerData.json");
        saveData = JsonUtility.FromJson<SaveData>(playerJson);
        for (int i = 0; i < saveData.LastMarketList.Length; i++)
        {
            if (saveData.LastMarketList[i] != "")
            {
                Remain = true;
                //���� ǰ��
                GameObject list = Instantiate(marketItemObject) as GameObject;
                list.transform.SetParent(marketListObject.transform, false);
                list.GetComponent<MarketItem>().marketScript = this;
                //������
                GameObject randomItem = Resources.Load<GameObject>("Prefab/item/" + saveData.LastMarketList[i]);

                Image img = randomItem.GetComponent<Image>();
                img.SetNativeSize();
                RectTransform rectTransform = randomItem.GetComponent<RectTransform>();
                // �̹����� �ʺ�� ���� ���� �����ɴϴ�.
                float width = rectTransform.sizeDelta.x;
                float height = rectTransform.sizeDelta.y;
                rectTransform.sizeDelta = new Vector2(width * 3f, height * 3f);

                Instantiate(randomItem, list.GetComponent<MarketItem>().icon.transform);
            }
        }
        if (!Remain)
        {
            string path = Application.dataPath + "/Resources";

            //FromJson �κ�
            string fromJsonData = File.ReadAllText(path + "/UnlockItemList.txt");
            ItemFromJson = JsonUtility.FromJson<UnlockList>(fromJsonData);

            FindRemainItem();
            FillItemList(5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerVisit == true)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (MarketOpen == true)
                {
                    MarketIsClosed();
                }
                else
                {
                    MarketIsOpen();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (MarketOpen == true)
                {
                    MarketIsClosed();
                }
            }
        }
    }

    void MarketIsOpen()
    {
        marketUiObject.SetActive(true);
        MarketOpen = true;
        KeyUiObject.SetActive(false);
        Ui_Controller ui = Shared.gameMgr.GetComponent<Ui_Controller>();
        ui.inven_ui.SetActive(true);
        ui.openMarket = true;
        Time.timeScale = 0f;
        Invoke("SaveLastList", 1f);
    }

    void MarketIsClosed()
    {
        marketUiObject.SetActive(false);
        MarketOpen = false;
        KeyUiObject.SetActive(true);
        Ui_Controller ui = Shared.gameMgr.GetComponent<Ui_Controller>();
        ui.inven_ui.SetActive(false);
        ui.openMarket = false;
        Destroy(ui.DescriptionBox);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            PlayerVisit = true;
            KeyUiObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            PlayerVisit = false;
            marketUiObject.SetActive(false);
            KeyUiObject.SetActive(false);
            MarketOpen = false;
        }
    }



    void FindRemainItem() //��� �ȵ� �����۸� �ҷ���
    {
        LoadPotion();
        for (int i = 0; i < ItemFromJson.items.Count; i++)
        {
            if (ItemFromJson.items[i].isUnlock == false)
            {
                RandomList.Add(i);
            }
        }
    }

    void FillItemList(int number) //���� ����Ʈ ä��
    {
        for (int i = 0; i < number; i++)
        {
            int randomNumber = Random.Range(0, RandomList.Count);
            if (RandomList.Count == 0) //���� �ִ� ������ ������ ����
            {
                break;
            }
            LoadPrefab(RandomList[randomNumber]);
        }
    }
    
    void LoadPrefab(int randomNumber) //������ �ҷ���
    {
        if (ItemFromJson.items[randomNumber].isUnlock == false)
        {
            //���� ǰ��
            GameObject list = Instantiate(marketItemObject) as GameObject;
            list.transform.SetParent(marketListObject.transform, false);
            list.GetComponent<MarketItem>().marketScript = this;
            //������
            GameObject randomItem = Resources.Load<GameObject>("Prefab/item/" + ItemFromJson.items[randomNumber].ItemName);

            Image img = randomItem.GetComponent<Image>();
            img.SetNativeSize();
            RectTransform rectTransform = randomItem.GetComponent<RectTransform>();
            // �̹����� �ʺ�� ���� ���� �����ɴϴ�.
            float width = rectTransform.sizeDelta.x;
            float height = rectTransform.sizeDelta.y;
            rectTransform.sizeDelta = new Vector2(width * 3f, height * 3f);

            Instantiate(randomItem, list.GetComponent<MarketItem>().icon.transform);
            RandomList.Remove(randomNumber);
        }
    }

    void LoadPotion()
    {
        //���� ǰ��
        GameObject list = Instantiate(marketItemObject) as GameObject;
        list.transform.SetParent(marketListObject.transform, false);
        list.GetComponent<MarketItem>().marketScript = this;

        //������
        GameObject randomItem = Resources.Load<GameObject>("Prefab/item/HpPotion");
        Instantiate(randomItem, list.GetComponent<MarketItem>().icon.transform);
    }

    public void BuySoundPlay()
    {
        sfx.clip = BuySound;
        sfx.Play();
        Invoke("DataSave", 1f);
    }

    public void SellSoundPlay()
    {
        sfx.clip = SellSound;
        sfx.Play();
        Invoke("DataSave", 1f);
    }

    public void SaveLastList()
    {
        dataMgr.JsonSave("MarketData");
    }

    void DataSave()
    {
        dataMgr.JsonSave("PlayerData");
        dataMgr.JsonSave("ItemData");
        dataMgr.JsonSave("MarketData");
    }
}
