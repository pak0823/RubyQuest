using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Controller : MonoBehaviour
{

    public Enemy monster;
    public Transform target;


    void Start()
    {
        target = Shared.player.gameObject.transform;
        monster.InitSetting(Shared.mapMgr.Difficulty);
        monster.orcbossOnetime();
    }


    void Update()
    {
        monster.OrcBoss(target);
    }
}
