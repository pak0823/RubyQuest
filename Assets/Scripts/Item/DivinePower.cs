using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DivinePower : itemStatus
{
    public override void InitSetting()
    {
        data.itemimg = this.GetComponent<Image>();
        data.itemName = "�ż� ����";
        data.itemNameEng = "DivinePower";
        data.itemPrice = 4007;
        data.color = Color.yellow;
        data.Rating = "��ȭ";
        data.itemExplanation = "���� ���̿�, ���� �̲����!";
        data.itemStat = "�뽬 ��Ÿ�� ���� 50%\n�뽬 ���� �ż� ȭ���� �߻��մϴ�.";
        data.itemNumber = 22;
    }

    public override void SpecialPower()
    {
        Player player = Shared.player;
        if (player == null) return;

        if (!data.SpecialPower)
        {
            player.DivinePower = false;
            player.SlidingCool += 1f;
        }
        if (data.SpecialPower)
        {
            player.DivinePower = true;
            player.SlidingCool -= 1f;
        }
    }

    public override void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText, TextMeshProUGUI PriceText, TextMeshProUGUI RatingText)
    {
        base.TextImageSettings(img, NameText, ExplanationText, StatText, PriceText, RatingText);
    }
}
