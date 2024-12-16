using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Boss : Enemy
{
    public override void InitSetting(int Difficulty)  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Stage = 3;
        Enemy_Name = "��� ������ ������"; //������ �߰���
        AmIBoss = true; //������ �߰���
        BossHpLine = 5; //������ �߰���
        Enemy_Mod = 2;  // ����
        Enemy_Power = 12f; //���� ���ݷ�
        Bump_Power = 10f;    // �浹 ���ݷ�
        Enemy_HP = 8000f * stats[Difficulty];  // ���� ü��
        Enemy_Speed = 1f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 4.2f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 10f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 5f;  // �÷��̾� ���� Y��
        Enemy_Range_X = 2f; //���� X�� ���� ��Ÿ�
        Enemy_Range_Y = 1f; //���� Y�� ���� ��Ÿ�
        atkDelay = 1f; // ���� ������
        atkTime = 0.6f; // ���� ��� �ð�
        endTime = 0.8f; // ����ü ������� �ð�
        bleedLevel = 0; // ���� ������
        turning = true; // ���� ����
        Attacking = false;
        Shared.gameMgr.GetComponent<BossHpController>().BossSpawn(this);
    }

    public override void Boss(Transform target)
    {
        base.Boss(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}