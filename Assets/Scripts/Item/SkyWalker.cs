using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkyWalker : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "��ī�� ��Ŀ";
        data.itemNameEng = "SkyWalker";
        data.itemPrice = 700;
        data.color = Color.green;
        data.Rating = "����";
        data.itemExplanation = "�������� ��ü�� ����������";
        data.itemStat = "�̵��ӵ� +5% \n���ݼӵ� +5% \n���� Ƚ�� +1";
        data.itemNumber = 1;
        data.Speed = 0.25f;
        data.AtkSpeed = 0.05f;
    }

    public override void SpecialPower()
    {
        if (!data.SpecialPower)
        {
            Shared.player.JumpCount--;
            Shared.player.JumpCnt--;
        }
        if(data.SpecialPower)
        {
            Shared.player.JumpCount++;
            Shared.player.JumpCnt++;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}