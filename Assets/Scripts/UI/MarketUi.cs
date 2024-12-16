using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUi : MonoBehaviour
{
    public GameObject item_list;


    void Awake()
    {
        Shared.marketui = this;
    }

    // Update is called once per frame
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ResetContent()
    {
        MarketItem[] GoList = item_list.GetComponentsInChildren<MarketItem>();
        if (GoList != null)
        {
            for (int i = 0; i < GoList.Length; i++)
            {
                GoList[i].SelfDestroy();
            }
        }
    }
}
