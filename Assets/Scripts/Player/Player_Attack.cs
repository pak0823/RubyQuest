using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Attack;
using static System.Net.WebRequestMethods;

public class Attack : Player
{
    public abstract class PlayerAttackRoutine
    {
        protected Player player;

        protected PlayerAttackRoutine(Player _player)
        {
            player = _player;
        }

        public abstract void NormalAttack();
        public abstract void NormalSkill();
        public abstract void PassiveSkill();
        public abstract void MasterSkill();
    }
}
public class SwordAttack : PlayerAttackRoutine
{
    public SwordAttack(Player player) : base(player) { }    

    public override void NormalAttack()
    {
        player.Dmg = (player.ATP + player.AtkPower + player.GridPower + player.VulcanPower) * player.WeaponsDmg[0];
        player.box.size = new Vector2(2.5f, 2.5f);
        player.box.offset = new Vector2(1.5f, 0);
        player.animator.SetFloat("Sword", player.SwdCnt); //Blend를 이용해 일반공격과 스킬 애니메이션 구분 실행
        player.animator.SetTrigger("sword_atk"); //공격 대미지 함수 실행은 애니메이션 부분에 들어있음
    }
    public override void NormalSkill()
    {
        if(player.Sword_SkTime <= 0)
        {
            player.isSkill = true;
            player.SwdCnt = 2;
            player.animator.SetTrigger("sword_atk");
            player.animator.SetFloat("Sword", player.SwdCnt); // 애니메이션에 스킬 실행함수를 넣어뒀음
            player.Sword_SkTime = player.DeCoolTimeCarcul(player.SkillTime[0]); //스킬쿨 수정함
        }
    }
    public override void PassiveSkill()
    {

    }
    public override void MasterSkill()
    {

    }
}
public class AxeAttack : PlayerAttackRoutine
{
    public AxeAttack(Player player) : base(player) { }

    public override void NormalAttack()
    {
        player.Dmg = (player.ATP + player.AtkPower + player.GridPower + player.VulcanPower) * player.WeaponsDmg[1];
        player.box.size = new Vector2(2.5f, 2.5f);
        if (player.slideDir == 1)   //공격 방향별 box.offset값을 다르게 적용
            player.box.offset = new Vector2(2, 0);
        else
            player.box.offset = new Vector2(1, 0);

        player.animator.SetTrigger("axe_atk");
        player.animator.SetFloat("Axe", 1); //Blend를 이용해 연속공격의 애니메이션 순차적 실행

        //사운드는 애니메이션에서 이벤트 실행
    }
    public override void NormalSkill()
    {
        if (player.Axe_SkTime <= 0)
        {
            player.isSkill = true;
            player.StartCoroutine(player.Skill());
            player.Axe_SkTime = player.DeCoolTimeCarcul(player.SkillTime[1]); //스킬쿨 수정함
        }
    }
    public override void PassiveSkill()
    {

    }
    public override void MasterSkill()
    {

    }
}

public class BowAttack : PlayerAttackRoutine
{
    public BowAttack(Player player) : base(player) { }

    public override void NormalAttack()
    {
        player.animator.SetTrigger("arrow_atk");
    }
    public override void NormalSkill()
    {
        if (player.Bow_SkTime <= 0)
        {
            player.isSkill = true;
            player.StartCoroutine(player.Skill());
            player.Bow_SkTime = player.DeCoolTimeCarcul(player.SkillTime[2]); //스킬쿨 수정함
        }
    }
    public override void PassiveSkill()
    {

    }
    public override void MasterSkill()
    {

    }
}




