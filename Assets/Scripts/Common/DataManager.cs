using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.Timeline;

[System.Serializable]
public class SaveData
{
    // 플레이어 데이터
    public int PlayerLevel = 1;
    public float PlayerGold = 0f;
    public float PlayerExp = 0f;
    public float PlayerCurrentHp = 100f;
    public Vector3 PlayerPos = new Vector3(0f, 0f, 0f);
    public int[] Stage = new int[2] { 0, 0 };

    // 숙련도 데이터
    public int proWeaponSellect = 0; //숙련도를 선택한 무기
    public int proLevel = 0; //숙련도 레벨
    public float proFill = 0f; //숙련도 진행 상황

    public float PlayTime = 0f;
    public int EnemyKill = 0;
    public float TotalGetGold = 0f;
    public float TotalDamaged = 0f;
    public int TotalItems = 0;

    public string[] LastMarketList;
    public string LastChestItem;

    public int Difficulty = 0;
    public bool FristMaterial;
    public bool SecondMaterial;
    public bool ThirdMaterial;
}

[System.Serializable]
public class OptionData
{
    // 게임 설정 데이터
    public float MasterVolume = 1f;
    public float BGMVolume = 1f;
    public float SFXVolume = 1f;

    public bool ViewMarketCnema = false;
    public bool ViewCnema1 = false;
    public bool ViewCnema2 = false;
    public bool ViewCnema3 = false;
    public bool ViewCnema4 = false;

    public bool NormalGameClear = false;
    public bool HardGameClear = false;
    public bool HellGameClear = false;

    public List<float> getVolume()
    {
        List<float> Volumes = new List<float>();
        Volumes.Add(MasterVolume);
        Volumes.Add(BGMVolume);
        Volumes.Add(SFXVolume);
        return Volumes;
    }
}

[System.Serializable]
public class Item
{
    public string[] itemEquip = new string[6];
    public string[] itemInven = new string[12];
}

[System.Serializable]
public class SelectLevel
{
    public string SelectName;
    public int Level;

    public SelectLevel(string selectName, int level)
    {
        SelectName = selectName;
        Level = level;
    }
}

[System.Serializable]
public class SelectList
{
    public SelectLevel[] Selects;
}

[System.Serializable]
public class DMUlcokItem
{
    public string ItemName;
    public bool isUnlock;

    public DMUlcokItem(string itemName, bool unlock)
    {
        ItemName = itemName;
        isUnlock = unlock;
    }
}

[System.Serializable]
public class ItemList
{
    public List<DMUlcokItem> items;
}

public class DataManager : MonoBehaviour
{
    public string PlayerPath;
    public string ItemPath;
    public string SelectPath;
    public string ItemUlockPath;
    public string OptionPath;
    public Proficiency_ui proData;
    public SoundManager soundData;
    public SoundSlider sliderData;
    public Player playerData;
    public Item ItemData = new Item();
    public SelectList SelectData = new SelectList();
    public UnlockList UnlockList;
    public SelectList SelectList;
    public SelectList selectData;

    public string PlayerloadJson;
    public string ItemloadJson;
    public string SelectloadJson;
    public string OptionLoadJson;

    public bool successCreateJson = true;

    
    public int[] CurrentStage;
    public itemStatus[] LastMarketList = new itemStatus[6];
    public string LastChestItem;

    private int count;
    public List<GameObject> finditemList;
    private void Awake()
    {
        if (Shared.dataMgr == null)
        {
            Shared.dataMgr = this;
            DontDestroyOnLoad(Shared.dataMgr);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayerPath = Path.Combine(Application.dataPath + "/Resources", "PlayerData.json");
        ItemPath = Path.Combine(Application.dataPath + "/Resources", "ItemData.json");
        SelectPath = Path.Combine(Application.dataPath + "/Resources", "UnlockSelectList.txt");
        ItemUlockPath = Path.Combine(Application.dataPath + "/Resources", "UnlockItemList.txt");
        OptionPath = Path.Combine(Application.dataPath + "/Resources", "OptionData.txt");
    }

    void Start()
    {
        PlayerPath = Path.Combine(Application.dataPath + "/Resources", "PlayerData.json");
        ItemPath = Path.Combine(Application.dataPath + "/Resources", "ItemData.json");
        SelectPath = Path.Combine(Application.dataPath + "/Resources", "UnlockSelectList.txt");
        ItemUlockPath = Path.Combine(Application.dataPath + "/Resources", "UnlockItemList.txt");
        OptionPath = Path.Combine(Application.dataPath + "/Resources", "OptionData.txt");
        JsonLoad("Default");
    }
    public void JsonLoad(string casedata)
    {
        SaveData saveData = new SaveData();
        OptionData optionData = new OptionData();
        if (!File.Exists(PlayerPath)) //초기값 생성
        {
            //Debug.Log("디버그 : 사용자 데이터 없음");
        }
        else
        {
            //Debug.Log("디버그 : 사용자 데이터 불러오는 중");
            PlayerloadJson = File.ReadAllText(PlayerPath);
            ItemloadJson = File.ReadAllText(ItemPath);
            SelectloadJson = File.ReadAllText(SelectPath);
            OptionLoadJson = File.ReadAllText(OptionPath);

            saveData = JsonUtility.FromJson<SaveData>(PlayerloadJson);
            ItemData = JsonUtility.FromJson<Item>(ItemloadJson);
            SelectData = JsonUtility.FromJson<SelectList>(SelectloadJson);
            optionData = JsonUtility.FromJson<OptionData>(OptionLoadJson);

            if (saveData != null)
            {
                switch (casedata)
                {
                    case "PlayerData":
                        if (Shared.player != null)
                        {
                            //Debug.Log("디버그 : 플레이어 데이터 불러오는 중");
                            Shared.player.level = saveData.PlayerLevel;
                            Shared.player.CurrentHp = saveData.PlayerCurrentHp;
                            Shared.player.gold = saveData.PlayerGold;
                            Shared.gameMgr.GetComponent<Ui_Controller>().ExpBar.value = saveData.PlayerExp;
                            Shared.player.transform.position = saveData.PlayerPos;
                            CurrentStage = saveData.Stage;
                            Shared.player.proSelectWeapon = saveData.proWeaponSellect;
                            Shared.player.proLevel = saveData.proLevel;
                            Shared.player.EnemyKillCount = saveData.EnemyKill;
                            Shared.player.TotalGetGold = saveData.TotalGetGold;
                            Shared.player.TotalDamaged = saveData.TotalDamaged;
                            Shared.player.FirstMaterial = saveData.FristMaterial;
                            Shared.player.SecondMaterial = saveData.SecondMaterial;
                            Shared.player.ThirdMaterial = saveData.ThirdMaterial;
                            OptionManager.instance.TotalPlayTime = saveData.PlayTime;
                            Proficiency_ui.instance.proWeaponIndex = saveData.proWeaponSellect;
                            Proficiency_ui.instance.proLevel = saveData.proLevel;
                            Proficiency_ui.instance.Profill.fillAmount = saveData.proFill;
                            //Debug.Log("디버그 : 플레이어 데이터 로드 완료");
                            //Debug.Log("디버그 : 선택지 데이터 불러오는 중");
                            for (int i = 0; i < SelectData.Selects.Length; i++)
                            {
                                switch (SelectData.Selects[i].SelectName)
                                {
                                    case "selectAtkLevel":
                                        Shared.player.selectAtkLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectATSLevel":
                                        Shared.player.selectATSLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectCCLevel":
                                        Shared.player.selectCCLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectLifeStillLevel":
                                        Shared.player.selectLifeStillLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectDefLevel":
                                        Shared.player.selectDefLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectHpLevel":
                                        Shared.player.selectHpLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectGoldLevel":
                                        Shared.player.selectGoldLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectExpLevel":
                                        Shared.player.selectExpLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectCoolTimeLevel":
                                        Shared.player.selectCoolTimeLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                }

                            }
                            //Debug.Log("디버그 : 선택지 데이터 로드 완료");
                            Shared.player.GetComponent<Player>().GetSelectValue("Start"); //시작시 선택지 능력치 얻음
                        }
                        break;
                    case "SliderData":
                        if (SoundSlider.instance != null)
                        {
                            //Debug.Log("디버그 : 사운드 데이터 불러오는 중");
                            SoundSlider.instance.master_slider.value = optionData.MasterVolume;
                            SoundSlider.instance.bgm_slider.value = optionData.BGMVolume;
                            SoundSlider.instance.sfx_slider.value = optionData.SFXVolume;
                            //Debug.Log("디버그 : 사운드 데이터 로드 완료");
                        }
                        break;
                    case "ItemData":
                        if (Shared.gameMgr != null)
                        {
                            //Debug.Log("디버그 : 아이템 데이터 불러오는 중");
                            for (int i = 0; i < ItemData.itemEquip.Length; i++)
                            {
                                if (ItemData.itemEquip[i] != "")
                                {
                                    GameObject prefab = Resources.Load<GameObject>("Prefab/item/" + ItemData.itemEquip[i]);
                                    GameObject temp = Instantiate(prefab, Shared.gameMgr.GetComponent<inven>().equip_slots[i].transform);
                                    temp.transform.SetParent(Shared.gameMgr.GetComponent<inven>().equip_slots[i].transform);
                                    Shared.gameMgr.GetComponent<inven>().equip_slots[i].GetComponentInChildren<Image>().color = Color.green;
                                }
                            }

                            for (int i = 0; i < ItemData.itemInven.Length; i++)
                            {
                                if (ItemData.itemInven[i] != "")
                                {
                                    GameObject prefab = Resources.Load<GameObject>("Prefab/item/" + ItemData.itemInven[i]);
                                    GameObject temp = Instantiate(prefab, Shared.gameMgr.GetComponent<inven>().inven_slots[i].transform);
                                    temp.transform.SetParent(Shared.gameMgr.GetComponent<inven>().inven_slots[i].transform);
                                }
                            }
                            //Debug.Log("디버그 : 아이템 데이터 로드 완료");
                        }
                        break;
                    case "ProData":
                        if (Proficiency_ui.instance != null)
                        {
                            //Debug.Log("프로 데이터 불러옴");
                            Proficiency_ui.instance.proWeaponIndex = saveData.proWeaponSellect;
                            Proficiency_ui.instance.proLevel = saveData.proLevel;
                            Proficiency_ui.instance.Profill.fillAmount = saveData.proFill;
                        }
                        break;

                }
            }
            //Debug.Log("디버그 : 사용자 데이터 성공적으로 불러옴");
        }
    }

    public void JsonSave(string casedata)
    {
        //Debug.Log("디버그 : 데이터 저장 하는중..");
        PlayerloadJson = File.ReadAllText(PlayerPath);
        SaveData jsonsave = JsonUtility.FromJson<SaveData>(PlayerloadJson);
        OptionLoadJson = File.ReadAllText(OptionPath);
        OptionData optionSave = JsonUtility.FromJson<OptionData>(OptionLoadJson);
        switch (casedata)
        {
            case "PlayerData":
                if (Shared.player != null)
                {
                    //Debug.Log("디버그 : 플레이어 데이터 저장 중");
                    jsonsave.PlayerLevel = Shared.player.level;
                    jsonsave.PlayerCurrentHp = Shared.player.CurrentHp;
                    jsonsave.PlayerGold = Shared.player.gold;
                    jsonsave.PlayerExp = Shared.gameMgr.GetComponent<Ui_Controller>().ExpBar.value;
                    jsonsave.PlayerPos = Shared.player.transform.position;
                    if (Shared.mapMgr != null)
                    {
                        jsonsave.Stage = Shared.mapMgr.CurrentStage;
                    }
                    jsonsave.proWeaponSellect = Proficiency_ui.instance.proWeaponIndex;
                    jsonsave.proLevel = Proficiency_ui.instance.proLevel;
                    jsonsave.proFill = Proficiency_ui.instance.Profill.fillAmount;
                    jsonsave.PlayTime = OptionManager.instance.TotalPlayTime;
                    jsonsave.EnemyKill = Shared.player.EnemyKillCount;
                    jsonsave.TotalGetGold = Shared.player.TotalGetGold;
                    jsonsave.TotalDamaged = Shared.player.TotalDamaged;
                    jsonsave.LastChestItem = LastChestItem;
                    jsonsave.FristMaterial = Shared.player.FirstMaterial;
                    jsonsave.SecondMaterial = Shared.player.SecondMaterial;
                    jsonsave.ThirdMaterial = Shared.player.ThirdMaterial;
                }
                //Debug.Log("디버그 : 플레이어 데이터 저장 완료");
                break;
            case "SliderData":
                //Debug.Log("디버그 : 사운드 데이터 저장 중");
                optionSave.MasterVolume = SoundSlider.instance.master_slider.value;
                optionSave.BGMVolume = SoundSlider.instance.bgm_slider.value;
                optionSave.SFXVolume = SoundSlider.instance.sfx_slider.value;
                //Debug.Log("디버그 : 사운드 데이터 저장 완료");
                break;
            case "ItemData":
                if (Shared.gameMgr != null)
                {
                    //Debug.Log("디버그 : 아이템 데이터 저장 중");
                    itemStatus[] equip_list = Shared.gameMgr.GetComponent<inven>().itemStatus_list_equip;
                    itemStatus[] inven_list = Shared.gameMgr.GetComponent<inven>().itemStatus_list_inven;
                    for (int i = 0; i < equip_list.Length; i++)
                    {
                        if (equip_list[i] != null)
                        {
                            ItemData.itemEquip[i] = equip_list[i].data.itemNameEng;
                            //Debug.Log(ItemData.itemEquip[i]);
                        }
                        else
                        {
                            ItemData.itemEquip[i] = null;
                        }
                    }
                    for (int i = 0; i < inven_list.Length; i++)
                    {
                        if (inven_list[i] != null)
                        {
                            ItemData.itemInven[i] = inven_list[i].data.itemNameEng;
                            //Debug.Log(ItemData.itemInven[i]);
                        }
                        else
                        {
                            ItemData.itemInven[i] = null;
                        }
                    }
                    //Debug.Log("디버그 : 아이템 데이터 저장 완료");
                }
                break;
            case "ProData":
                if (Proficiency_ui.instance != null)
                {
                    //Debug.Log("프로 데이터 저장");
                    jsonsave.proWeaponSellect = Proficiency_ui.instance.proWeaponIndex;
                    jsonsave.proLevel = Proficiency_ui.instance.proLevel;
                    jsonsave.proFill = Proficiency_ui.instance.Profill.fillAmount;
                }
                break;
            case "MarketData":
                for (int i = 0; i < LastMarketList.Length; i++)
                {
                    if (LastMarketList[i] == null)
                    {
                        jsonsave.LastMarketList[i] = "";
                    }
                    else
                    {
                        jsonsave.LastMarketList[i] = LastMarketList[i].data.itemNameEng;
                    }
                }
                break;
        }
        string Playerjson = JsonUtility.ToJson(jsonsave, true);
        string itemjson = JsonUtility.ToJson(ItemData, true);
        string optionJson = JsonUtility.ToJson(optionSave, true);
        File.WriteAllText(PlayerPath, Playerjson);
        File.WriteAllText(ItemPath, itemjson);
        File.WriteAllText(OptionPath, optionJson);
        //Debug.Log("디버그 : 모든 데이터를 성공적으로 저장하였습니다");
    }

    public void JsonSliderSave(float master, float bgm, float sfx)
    {
        OptionLoadJson = File.ReadAllText(OptionPath);
        OptionData optionSave = JsonUtility.FromJson<OptionData>(OptionLoadJson);
        optionSave.MasterVolume = master;
        optionSave.BGMVolume = bgm;
        optionSave.SFXVolume = sfx;
        string optionJson = JsonUtility.ToJson(optionSave, true);
        File.WriteAllText(OptionPath, optionJson);
    }

    public void JsonSliderLoad()
    {
        OptionData optionData = new OptionData();
        OptionLoadJson = File.ReadAllText(OptionPath);
        optionData = JsonUtility.FromJson<OptionData>(OptionLoadJson);
        if (SoundSlider.instance != null)
        {
            //Debug.Log("디버그 : 사운드 데이터 불러오는 중");
            SoundSlider.instance.master_slider.value = optionData.MasterVolume;
            SoundSlider.instance.bgm_slider.value = optionData.BGMVolume;
            SoundSlider.instance.sfx_slider.value = optionData.SFXVolume;
            //Debug.Log("디버그 : 사운드 데이터 로드 완료");
        }
    }

    public void CreateJson(int Difficulty)
    {
        //Debug.Log("디버그 : 데이터 생성 하는중..");
        CreatePlayerJson(Difficulty);
        CreateItemJson(Difficulty);
        CreateSelectJson();
        CreateUnlockItemJson();
        string filePath = Path.Combine(Application.dataPath + "/Resources", "OptionData.txt");
        if (File.Exists(filePath))
        {
            return;
        }
        else
        {
            CreateOptionJson();
        }
        //Debug.Log("디버그 : 데이터를 성공적으로 생성하였습니다");
        successCreateJson = true;
    }

    public void CreatePlayerJson(int Difficulty)
    {
        SaveData saveData = new SaveData();
        //Debug.Log("디버그 : 플레이어 데이터 생성 중");
        saveData.PlayerLevel = 1;
        saveData.PlayerGold = 0;
        saveData.PlayerExp = 0.0f;
        saveData.PlayerCurrentHp = 100.0f;
        saveData.PlayerPos = new Vector3(0f, 0f, 0f);
        int[] Stage = new int[2] { 0, 0 };
        saveData.Stage = Stage;
        saveData.PlayTime = 0f;
        saveData.EnemyKill = 0;
        saveData.TotalGetGold = 0f;
        saveData.TotalDamaged = 0f;
        //Debug.Log("디버그 : 플레이어 데이터 생성 완료");
        //Debug.Log("디버그 : 숙련도 데이터 생성 중");
        saveData.proWeaponSellect = 4;
        saveData.proLevel = 0;
        saveData.proFill = 0.0f;
        //Debug.Log("디버그 : 숙련도 데이터 생성 완료");
        saveData.LastMarketList = new string[6];
        saveData.LastChestItem = null;
        saveData.Difficulty = Difficulty;
        saveData.FristMaterial = false;
        saveData.SecondMaterial = false;
        saveData.ThirdMaterial = false;
        saveData.TotalItems = 0;
        string Playerjson = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(PlayerPath, Playerjson);
    }

    public void CreateItemJson(int Difficulty)
    {
        //Debug.Log("디버그 : 아이템 데이터 생성 중");
        Item itemData = new Item();
        itemData.itemEquip = new string[6];
        itemData.itemInven = new string[12];
        //Debug.Log("디버그 : 아이템 데이터 생성 완료");
        if (Difficulty < 2)
        {
            itemData.itemEquip[0] = "GrandmaNecklace";
        }
        string itemjson = JsonUtility.ToJson(itemData, true);
        File.WriteAllText(ItemPath, itemjson);
    }

    public void CreateOptionJson()
    {
        string filePath = Path.Combine(Application.dataPath + "/Resources", "OptionData.txt");
        if (File.Exists(filePath))
        {
            return;
        }
        else
        {
            OptionData optionData = new OptionData();

            //Debug.Log("디버그 : 사운드 데이터 생성 중");
            optionData.MasterVolume = 0.5f;
            optionData.BGMVolume = 0.5f;
            optionData.SFXVolume = 0.5f;
            //Debug.Log("디버그 : 사운드 데이터 생성 완료");

            optionData.ViewMarketCnema = false;
            optionData.ViewCnema1 = false;
            optionData.ViewCnema2 = false;
            optionData.ViewCnema3 = false;
            optionData.ViewCnema4 = false;

            optionData.NormalGameClear = false;
            optionData.HardGameClear = false;
            optionData.HellGameClear = false;
            string OptionJson = JsonUtility.ToJson(optionData, true);
            File.WriteAllText(OptionPath, OptionJson);
        }
    }

    public void CreateSelectJson()
    {
        //Debug.Log("디버그 : 선택지 레벨 리스트 파일 생성 중");

        // SelectLevel 데이터 생성
        List<SelectLevel> selectLevels = new List<SelectLevel>();
        selectLevels.Add(new SelectLevel("selectAtkLevel", 1));
        selectLevels.Add(new SelectLevel("selectATSLevel", 1));
        selectLevels.Add(new SelectLevel("selectCCLevel", 1));
        selectLevels.Add(new SelectLevel("selectLifeStillLevel", 1));
        selectLevels.Add(new SelectLevel("selectDefLevel", 1));
        selectLevels.Add(new SelectLevel("selectHpLevel", 1));
        selectLevels.Add(new SelectLevel("selectGoldLevel", 1));
        selectLevels.Add(new SelectLevel("selectExpLevel", 1));
        selectLevels.Add(new SelectLevel("selectCoolTimeLevel", 1));

        // 데이터를 JSON 파일로 저장
        SaveToJson(selectLevels);

        //Debug.Log("디버그 : 선택지 레벨 리스트 파일 생성 완료");
    }

    void SaveToJson(List<SelectLevel> data)
    {
        SelectList selectList = new SelectList();
        selectList.Selects = data.ToArray();

        string jsonData = JsonUtility.ToJson(selectList);
        File.WriteAllText(SelectPath, jsonData);
        //Debug.Log("JSON 파일이 생성되었습니다.");
    }

    void CreateUnlockItemJson()
    {
        // Item 데이터 생성
        List<DMUlcokItem> items = new List<DMUlcokItem>();
        items.Add(new DMUlcokItem("SkyWalker", false));
        items.Add(new DMUlcokItem("Club", false));
        items.Add(new DMUlcokItem("JadeEmblem", false));
        items.Add(new DMUlcokItem("ClownCloth", false));
        items.Add(new DMUlcokItem("ClownHat", false));
        items.Add(new DMUlcokItem("ClownGloves", false));
        items.Add(new DMUlcokItem("ClownPants", false));
        items.Add(new DMUlcokItem("ClownBoots", false));
        items.Add(new DMUlcokItem("SymbolRich", false));
        items.Add(new DMUlcokItem("LightningGloves", false));
        items.Add(new DMUlcokItem("BattleBookBeginner", false));
        items.Add(new DMUlcokItem("StrangeCandy", false));
        items.Add(new DMUlcokItem("VampireCup", false));
        items.Add(new DMUlcokItem("Cookie", false));
        items.Add(new DMUlcokItem("PoisonMushroom", false));
        items.Add(new DMUlcokItem("RootOfTree", false));
        items.Add(new DMUlcokItem("NightofCountingtheStars", false));
        items.Add(new DMUlcokItem("GlassSword", false));
        items.Add(new DMUlcokItem("GridsSword", false));
        items.Add(new DMUlcokItem("PastThatWantToErase", false));
        items.Add(new DMUlcokItem("ReproductionOfMassacre", false));
        items.Add(new DMUlcokItem("DivinePower", false));
        items.Add(new DMUlcokItem("BrokenWatch", false));
        items.Add(new DMUlcokItem("EyeOfBeast", false));
        items.Add(new DMUlcokItem("AttackClaw", false));
        items.Add(new DMUlcokItem("BlackCard", false));
        items.Add(new DMUlcokItem("RedCard", false));
        items.Add(new DMUlcokItem("AssassinDagger", false));
        items.Add(new DMUlcokItem("DoubleEdgedAxe", false));
        items.Add(new DMUlcokItem("ElfBow", false));
        items.Add(new DMUlcokItem("EscapeRope", false));
        items.Add(new DMUlcokItem("BundleOfGifts", false));
        items.Add(new DMUlcokItem("LoveLetter", false));
        items.Add(new DMUlcokItem("MiniStar", false));
        items.Add(new DMUlcokItem("HeroMask", false));
        items.Add(new DMUlcokItem("RepressionShield", false));
        items.Add(new DMUlcokItem("ThreePeas", false));
        items.Add(new DMUlcokItem("TransmitterHammer", false));
        items.Add(new DMUlcokItem("SuspiciousMirror", false));
        items.Add(new DMUlcokItem("VulcanArmor", false));
        items.Add(new DMUlcokItem("WorkGloves", false));
        items.Add(new DMUlcokItem("Ocarina", false));
        items.Add(new DMUlcokItem("PickpocketGloves", false));
        items.Add(new DMUlcokItem("RingOfLife", false));
        items.Add(new DMUlcokItem("ElunsHat", false));
        items.Add(new DMUlcokItem("ElunsRobe", false));
        items.Add(new DMUlcokItem("ElunsWand", false));
        items.Add(new DMUlcokItem("FairyFanFlute", false));
        items.Add(new DMUlcokItem("OrcHorn", false));
        items.Add(new DMUlcokItem("WoodenShield", false));
        items.Add(new DMUlcokItem("ShadowSupplyBook", false));
        // 데이터를 JSON 파일로 저장
        SaveToJson(items);
    }

    void SaveToJson(List<DMUlcokItem> data)
    {
        ItemList itemList = new ItemList();
        itemList.items = data;

        // Unity의 JsonUtility를 사용하여 JSON 파일 생성
        string jsonData = JsonUtility.ToJson(itemList, true);
        File.WriteAllText(ItemUlockPath, jsonData);
        //Debug.Log("JSON 파일이 생성되었습니다.");
    }

    public List<float> getVolume()
    {
        OptionData optionData = new OptionData();
        string loadJson = File.ReadAllText(OptionPath);
        optionData = JsonUtility.FromJson<OptionData>(loadJson);
        return optionData.getVolume();
    }

    public void UnlockListUpdate(int ItemNumber)
    {
        List<Unlock> newUnlockList = new List<Unlock>();

        string path = Application.dataPath + "/Resources";
        string fromJsonData = File.ReadAllText(path + "/UnlockItemList.txt");
        UnlockList = JsonUtility.FromJson<UnlockList>(fromJsonData);
        UnlockList.items[ItemNumber - 1].isUnlock = true;

        UnlockList Item = new UnlockList();
        Item.items = UnlockList.items;

        string jsonData = JsonUtility.ToJson(Item, true);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/UnlockItemList.txt", jsonData);
    }

    public void SelectListUpdate(string Name)
    {
        List<SelectLevel> newSelectList = new List<SelectLevel>();

        string path = Application.dataPath + "/Resources";
        string fromJsonData = File.ReadAllText(path + "/UnlockSelectList.txt");
        SelectList = JsonUtility.FromJson<SelectList>(fromJsonData);
        for (int i = 0; i < SelectList.Selects.Length; i++)
        {
            if (SelectList.Selects[i].SelectName == Name)
            {
                SelectList.Selects[i].Level++;
                switch (SelectList.Selects[i].SelectName)
                {
                    case "selectAtkLevel":
                        Shared.player.selectAtkLevel++;
                        Shared.player.GetSelectValue("selectAtkLevel");
                        break;
                    case "selectATSLevel":
                        Shared.player.selectATSLevel++;
                        Shared.player.GetSelectValue("selectATSLevel");
                        break;
                    case "selectCCLevel":
                        Shared.player.selectCCLevel++;
                        Shared.player.GetSelectValue("selectCCLevel");
                        break;
                    case "selectLifeStillLevel":
                        Shared.player.selectLifeStillLevel++;
                        Shared.player.GetSelectValue("selectLifeStillLevel");
                        break;
                    case "selectDefLevel":
                        Shared.player.selectDefLevel++;
                        Shared.player.GetSelectValue("selectDefLevel");
                        break;
                    case "selectHpLevel":
                        Shared.player.selectHpLevel++;
                        Shared.player.GetSelectValue("selectHpLevel");
                        Shared.gameMgr.GetComponent<Ui_Controller>().UiUpdate();
                        break;
                    case "selectGoldLevel":
                        Shared.player.selectGoldLevel++;
                        Shared.player.GetSelectValue("selectGoldLevel");
                        break;
                    case "selectExpLevel":
                        Shared.player.selectExpLevel++;
                        Shared.player.GetSelectValue("selectExpLevel");
                        break;
                    case "selectCoolTimeLevel":
                        Shared.player.selectCoolTimeLevel++;
                        Shared.player.GetSelectValue("selectCoolTimeLevel");
                        break;
                }
                break;
            }
        }
        SelectList Select = new SelectList();
        Select.Selects = SelectList.Selects;

        string jsonData = JsonUtility.ToJson(Select, true);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/UnlockSelectList.txt", jsonData);
    }

    public GameObject ChestItem()
    {
        PlayerloadJson = File.ReadAllText(PlayerPath);
        SaveData jsonsave = JsonUtility.FromJson<SaveData>(PlayerloadJson);
        if (jsonsave.LastChestItem != "")
        {
            GameObject randomItem = Resources.Load<GameObject>("Prefab/item/" + jsonsave.LastChestItem);
            LastChestItem = jsonsave.LastChestItem;
            JsonSave("PlayerData");
            return randomItem;
        }
        else
        {
            UnlockList Json;

            string path = Application.dataPath + "/Resources";
            string fromJsonData = File.ReadAllText(path + "/UnlockItemList.txt");
            Json = JsonUtility.FromJson<UnlockList>(fromJsonData);

            List<int> ItemList = new List<int> { };
            for (int i = 0; i < Json.items.Count; i++)
            {
                if (Json.items[i].isUnlock == false)
                {
                    ItemList.Add(i);
                    //Debug.Log(Json.items[i].ItemName);
                }
            }
            int randomNumber = Random.Range(0, ItemList.Count);
            if (ItemList.Count == 0) //남아 있는 아이템 없으면 종료
            {
                GameObject Potion = Resources.Load<GameObject>("Prefab/item/HpPotion");
                return Potion;
            }
            GameObject randomItem = Resources.Load<GameObject>("Prefab/item/" + Json.items[ItemList[randomNumber]].ItemName);
            LastChestItem = Json.items[ItemList[randomNumber]].ItemName;
            JsonSave("PlayerData");
            return randomItem;
        }
    }

    public void NextStage()
    {
        PlayerloadJson = File.ReadAllText(PlayerPath);
        SaveData jsonsave = JsonUtility.FromJson<SaveData>(PlayerloadJson);
        Vector3 vc = new Vector3(0, 0, 0);
        jsonsave.PlayerPos = vc;
        if (Shared.mapMgr != null)
        {
            jsonsave.Stage = Shared.mapMgr.CurrentStage;
        }
        jsonsave.PlayerGold = Shared.player.gold;
        jsonsave.LastChestItem = "";
        jsonsave.LastMarketList = new string[6];
        Shared.marketui.ResetContent();
        string Playerjson = JsonUtility.ToJson(jsonsave, true);
        File.WriteAllText(PlayerPath, Playerjson);
        JsonSave("ItemData");
    }

    public bool CanCenemaPlay()
    {
        OptionLoadJson = File.ReadAllText(OptionPath);
        OptionData optionSave = JsonUtility.FromJson<OptionData>(OptionLoadJson);
        if (optionSave.ViewMarketCnema == false)
        {
            optionSave.ViewMarketCnema = true;
            string optionJson = JsonUtility.ToJson(optionSave, true);
            File.WriteAllText(OptionPath, optionJson);
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<GameObject> finditem()
    {
        UnlockList Json;

        string path = Application.dataPath + "/Resources";
        string fromJsonData = File.ReadAllText(path + "/UnlockItemList.txt");
        Json = JsonUtility.FromJson<UnlockList>(fromJsonData);
        for (int i = 0; i < Json.items.Count; i++)
        {
            if (Json.items[i].isUnlock == true)
            {
                GameObject finditem = Resources.Load<GameObject>("Prefab/item/" + Json.items[i].ItemName);
                finditemList.Add(finditem);
            }
        }
        return finditemList;
    }

    public void DeleteJson()
    {
        string playPath = Path.Combine(Application.dataPath + "/Resources", "PlayerData.json");
        if (File.Exists(playPath))
        {
            File.Delete(playPath);
            //Debug.Log("JSON 파일이 성공적으로 삭제되었습니다.");
        }
        else
        {
            //Debug.Log("삭제할 JSON 파일이 존재하지 않습니다.");
        }
    }

    public bool findPlayerData()
    {
        string filePath = Path.Combine(Application.dataPath + "/Resources", "PlayerData.json");

        if (File.Exists(filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int returnDifficulty()
    {
        SaveData saveData = new SaveData();
        PlayerloadJson = File.ReadAllText(PlayerPath);
        saveData = JsonUtility.FromJson<SaveData>(PlayerloadJson);
        return saveData.Difficulty;
    }

    public List<bool> returnClear()
    {
        OptionData optionData = new OptionData();
        OptionLoadJson = File.ReadAllText(OptionPath);
        optionData = JsonUtility.FromJson<OptionData>(OptionLoadJson);
        List<bool> returnList = new List<bool> { };
        returnList.Add(optionData.NormalGameClear);
        returnList.Add(optionData.HardGameClear);
        returnList.Add(optionData.HellGameClear);
        return returnList;
    }

    public void GameClear(int Level)
    {
        OptionLoadJson = File.ReadAllText(OptionPath);
        OptionData optionSave = JsonUtility.FromJson<OptionData>(OptionLoadJson);
        switch (Level)
        {
            case 0:
                optionSave.NormalGameClear = true;
                break;
            case 1:
                optionSave.HardGameClear = true;
                break;
            case 2:
                optionSave.HellGameClear = true;
                break;
        }
        string optionJson = JsonUtility.ToJson(optionSave, true);
        File.WriteAllText(OptionPath, optionJson);
    }
}
