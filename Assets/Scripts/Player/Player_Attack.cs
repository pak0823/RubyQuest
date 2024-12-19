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
        player.animator.SetFloat("Sword", player.SwdCnt); //Blend�� �̿��� �Ϲݰ��ݰ� ��ų �ִϸ��̼� ���� ����
        player.animator.SetTrigger("sword_atk"); //���� ����� �Լ� ������ �ִϸ��̼� �κп� �������
    }
    public override void NormalSkill()
    {
        if(player.Sword_SkTime <= 0)
        {
            player.isSkill = true;
            player.SwdCnt = 2;
            player.animator.SetTrigger("sword_atk");
            player.animator.SetFloat("Sword", player.SwdCnt); // �ִϸ��̼ǿ� ��ų �����Լ��� �־����
            player.Sword_SkTime = player.DeCoolTimeCarcul(player.SkillTime[0]); //��ų�� ������
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
        if (player.slideDir == 1)   //���� ���⺰ box.offset���� �ٸ��� ����
            player.box.offset = new Vector2(2, 0);
        else
            player.box.offset = new Vector2(1, 0);

        player.animator.SetTrigger("axe_atk");
        player.animator.SetFloat("Axe", 1); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����

        //����� �ִϸ��̼ǿ��� �̺�Ʈ ����
    }
    public override void NormalSkill()
    {
        if (player.Axe_SkTime <= 0)
        {
            player.isSkill = true;
            player.StartCoroutine(player.Skill());
            player.Axe_SkTime = player.DeCoolTimeCarcul(player.SkillTime[1]); //��ų�� ������
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
            player.Bow_SkTime = player.DeCoolTimeCarcul(player.SkillTime[2]); //��ų�� ������
        }
    }
    public override void PassiveSkill()
    {

    }
    public override void MasterSkill()
    {

    }
}




