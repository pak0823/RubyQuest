using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PastThatWantToErase : itemStatus
{
    public override void InitSetting()
    {
        Player player = Shared.player;
        if(player == null )
            return;

        if (player.UsePastErase)
        {
            player.PastErase = this.gameObject;
        }
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "����� ���� ����";
        data.itemNameEng = "PastThatWantToErase";
        data.itemPrice = 4823;
        data.color = Color.yellow;
        data.Rating = "��ȭ";
        data.itemExplanation = "���ŷ� ���ư� �� �ִٸ� �ڽſ��� � ������ �Ͻðڽ��ϱ�.";
        data.itemStat = "�÷��̾ ġ����� ������ ü���� �ǵ����ϴ�.";
        data.itemNumber = 20;
    }

    public override void SpecialPower()
    {
        Player player = Shared.player;
        if (player == null)
            return;

        if (!data.SpecialPower)
        {
            player.UsePastErase = false;
            player.PastErase = null;
        }
        else if (data.SpecialPower)
        {
            player.UsePastErase = true;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
