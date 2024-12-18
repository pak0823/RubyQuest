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
        data.itemName = "신성 권한";
        data.itemNameEng = "DivinePower";
        data.itemPrice = 4007;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemExplanation = "빛의 힘이여, 나를 이끌어라!";
        data.itemStat = "대쉬 쿨타임 감소 50%\n대쉬 사용시 신성 화살을 발사합니다.";
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
