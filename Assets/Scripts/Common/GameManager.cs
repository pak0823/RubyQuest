using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        if (Shared.gameMgr != null && Shared.gameMgr != this) //인스턴스가 이미 존재하는지 확인, 자기 자신인지 확인
        {
            Destroy(this.gameObject); //생성하려는 인스턴스 파괴
        }
        else
        {
            Shared.gameMgr = this; //아니면 인스턴스를 현재 GameManager 객체로 설정
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // 게임매니저 기능 구현
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
