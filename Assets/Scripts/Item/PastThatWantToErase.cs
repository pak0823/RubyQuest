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
        data.itemName = "지우고 싶은 과거";
        data.itemNameEng = "PastThatWantToErase";
        data.itemPrice = 4823;
        data.color = Color.yellow;
        data.Rating = "신화";
        data.itemExplanation = "과거로 돌아갈 수 있다면 자신에게 어떤 조언을 하시겠습니까.";
        data.itemStat = "플레이어가 치명상을 입으면 체력을 되돌립니다.";
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
