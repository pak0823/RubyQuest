using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridsSword : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "±×¸®µåÀÇ °Ë";
        data.itemNameEng = "GridsSword";
        data.itemPrice = 4500;
        data.color = Color.yellow;
        data.Rating = "½ÅÈ­";
        data.itemExplanation = "\"µ·Àº °ð Èû!\" \n-±×¸®µå-";
        data.itemStat = "°ñµå È¹µæ·® +77% \nº¸À¯ÇÑ °ñµå 777 ´ç, °ø°Ý·ÂÀÌ 0.77 Áõ°¡ÇÕ´Ï´Ù.";
        data.itemNumber = 19;
        data.GoldGet = 0.77f;
    }

    public override void SpecialPower()
    {
        Ui_Controller ui = Shared.gameMgr.GetComponent<Ui_Controller>();
        Player player = Shared.player;
        if (player == null)
            return;

        if (!data.SpecialPower)
        {
            player.UseGridSword = false;
            player.GridPower = 0f;
            ui.UiUpdate();
        }
        if (data.SpecialPower)
        {
            player.GridsSword();
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
