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
        data.itemName = "�׸����� ��";
        data.itemNameEng = "GridsSword";
        data.itemPrice = 4500;
        data.color = Color.yellow;
        data.Rating = "��ȭ";
        data.itemExplanation = "\"���� �� ��!\" \n-�׸���-";
        data.itemStat = "��� ȹ�淮 +77% \n������ ��� 777 ��, ���ݷ��� 0.77 �����մϴ�.";
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
