using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        if (Shared.gameMgr != null && Shared.gameMgr != this) //�ν��Ͻ��� �̹� �����ϴ��� Ȯ��, �ڱ� �ڽ����� Ȯ��
        {
            Destroy(this.gameObject); //�����Ϸ��� �ν��Ͻ� �ı�
        }
        else
        {
            Shared.gameMgr = this; //�ƴϸ� �ν��Ͻ��� ���� GameManager ��ü�� ����
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // ���ӸŴ��� ��� ����
    public void gameover()
    {
        StartCoroutine(DataSave());
        Application.Quit();
    }

    IEnumerator DataSave()
    {
        Shared.dataMgr.JsonSave(default);
        yield return null;
    }
}
