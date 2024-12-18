using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip bloodEff;

    public AudioClip beeAtk;
    public AudioClip Buzzing;

    public AudioClip boarHit;
    public AudioClip boarDie;
    public AudioClip boarRun;

    public AudioClip lgHit;
    public AudioClip lgDie;
    public AudioClip lgAtk;

    public AudioClip mrHit;
    public AudioClip mrDie;
    public AudioClip mrAtk;

    public AudioClip necjump;
    public AudioClip necTeleport;
    public AudioClip necBoom;
    public AudioClip necHit;
    public AudioClip necDie;

    public AudioClip slimeHit;
    public AudioClip slimeDie;
    public AudioClip slimeJump;

    public AudioClip torchHit;
    public AudioClip torchDie;

    public AudioClip wolfAtk;
    public AudioClip wolfHit;
    public AudioClip wolfDie;

    public AudioClip ratDie;
    public AudioClip ratHit;

    public AudioClip batHit;
    public AudioClip batDie;

    public AudioClip orcHit;
    public AudioClip orcDie;
    public AudioClip orcAtk;
    public AudioClip orcWalkL;
    public AudioClip orcWalkR;

    public AudioClip snailHit;
    public AudioClip snailDie;

    public AudioClip golemLaser;
    public AudioClip golemBreakdown;
    public AudioClip golemHit;
    public AudioClip golemDie;

    private void Awake()
    {
        audioSource = this.gameObject.GetComponentInParent<AudioSource>();
    }
    public void BloodEff()
    {
        Sounds("bloodEff");
    }
    public void BeeAtk()
    {
        Sounds("beeAtk");
    }
    public void buzzing()
    {
        Sounds("Buzzing");
    }
    public void BoarHit()
    {
        Sounds("boarHit");
    }
    public void BoarDie()
    {
        Sounds("boarDie");
    }
    public void BoarRun()
    {
        Sounds("boarRun");
    }
    public void LgHit()
    {
        Sounds("lgHit");
    }
    public void LgDie()
    {
        Sounds("lgDie");
    }
    public void LgAtk()
    {
        Sounds("lgAtk");
    }
    public void MrHit()
    {
        Sounds("mrHit");
    }
    public void MrDie()
    {
        Sounds("mrDie");
    }
    public void MrAtk()
    {
        Sounds("mrAtk");
    }
    public void Necjump()
    {
        Sounds("necjump");
    }
    public void NecTeleport()
    {
        Sounds("necTeleport");
    }
    public void NecBoom()
    {
        Sounds("necBoom");
    }
    public void NecHit()
    {
        Sounds("necHit");
    }
    public void NecDie()
    {
        Sounds("necDie");
        BossClearPos.instance.function(2);
        Shared.player.SecondMaterial = true;
        Shared.mapMgr.BossDie();
    }
    public void SlimeHit()
    {
        Sounds("slimeHit");
    }
    public void SlimeDie()
    {
        Sounds("slimeDie");
    }
    public void SlimeJump()
    {
        Sounds("slimeJump");
    }
    public void TorchHit()
    {
        Sounds("torchHit");
    }
    public void TorchDie()
    {
        Sounds("torchDie");
    }
    public void WolfAtk()
    {
        Sounds("wolfAtk");
    }
    public void WolfHit()
    {
        Sounds("wolfHit");
    }
    public void WolfDie()
    {
        Sounds("wolfDie");
    }
    public void RatHit()
    {
        Sounds("ratHit");
    }
    public void RatDie()
    {
        Sounds("ratDie");
    }
    public void BatHit()
    {
        Sounds("batHit");
    }
    public void BatDie()
    {
        Sounds("batDie");
    }
    public void OrcHit()
    {
        Sounds("orcHit");
    }
    public void OrcDie()
    {
        Sounds("orcDie");
    }
    public void OrcAtk()
    {
        Sounds("orcAtk");
    }
    public void OrcWalkL()
    {
        Sounds("orcWalkL");
    }
    public void OrcWalkR()
    {
        Sounds("orcWalkR");
    }
    public void SnailHit()
    {
        Sounds("snailHit");
    }
    public void SnailDie()
    {
        Sounds("snailDie");
    }

    public void GolemLaser()
    {
        Sounds("golemLaser");
    }

    public void GolemBreakdown()
    {
        Sounds("golemBreakdown");
    }

    public void GolemDie()
    {
        Shared.player.ThirdMaterial = true;
        BossClearPos.instance.function(3);
        Shared.mapMgr.BossDie();
        Sounds("golemDie");
        Invoke("GolemDestroy", 5.2f);
    }

    public void GolemDestroy()
    {
        Destroy(gameObject);
    }

    public void GolemHit()
    {
        Shared.soundMgr.SFXPlay("golemHitSound",golemHit);
    }
    public void Sounds(string sounds)
    {
        switch (sounds)
        {
            case "bloodEff":
                audioSource.clip = bloodEff;
                break;
            case "beeAtk":
                audioSource.clip = beeAtk;
                break;
            case "Buzzing":
                audioSource.clip = Buzzing;
                break;
            case "boarHit":
                audioSource.clip = boarHit;
                break;
            case "boarDie":
                audioSource.clip = boarDie;
                break;
            case "boarRun":
                audioSource.clip = boarRun;
                break;
            case "lgHit":
                audioSource.clip = lgHit;
                break;
            case "lgDie":
                audioSource.clip = lgDie;
                break;
            case "lgAtk":
                audioSource.clip = lgAtk;
                break;
            case "mrHit":
                audioSource.clip = mrHit;
                break;
            case "mrDie":
                audioSource.clip = mrDie;
                break;
            case "mrAtk":
                audioSource.clip = mrAtk;
                break;
            case "necjump":
                audioSource.clip = necjump;
                break;
            case "necTeleport":
                audioSource.clip = necTeleport;
                break;
            case "necBoom":
                audioSource.clip = necBoom;
                break;
            case "necHit":
                audioSource.clip = necHit;
                break;
            case "necDie":
                audioSource.clip = necDie;
                break;
            case "slimeHit":
                audioSource.clip = slimeHit;
                break;
            case "slimeDie":
                audioSource.clip = slimeDie;
                break;
            case "slimeJump":
                audioSource.clip = slimeJump;
                break;
            case "torchHit":
                audioSource.clip = torchHit;
                break;
            case "torchDie":
                audioSource.clip = torchDie;
                break;
            case "wolfAtk":
                audioSource.clip = wolfAtk;
                break;
            case "wolfHit":
                audioSource.clip = wolfHit;
                break;
            case "wolfDie":
                audioSource.clip = wolfDie;
                break;
            case "ratHit":
                audioSource.clip = ratHit;
                break;
            case "ratDie":
                audioSource.clip = ratDie;
                break;
            case "batHit":
                audioSource.clip = batHit;
                break;
            case "batDie":
                audioSource.clip = batDie;
                break;
            case "orcHit":
                audioSource.clip = orcHit;
                break;
            case "orcDie":
                audioSource.clip = orcDie;
                break;
            case "orcAtk":
                audioSource.clip = orcAtk;
                break;
            case "orcWalkL":
                audioSource.clip = orcWalkL;
                break;
            case "orcWalkR":
                audioSource.clip = orcWalkR;
                break;
            case "snailHit":
                audioSource.clip = snailHit;
                break;
            case "snailDie":
                audioSource.clip = snailDie;
                break;
            case "golemLaser":
                audioSource.clip = golemLaser;
                break;
            case "golemBreakdown":
                audioSource.clip = golemBreakdown;
                break;
            case "golemDie":
                audioSource.clip = golemDie;
                break;
            case "golemHit":
                audioSource.clip = golemHit;
                break;
        }
        audioSource.Play();
    }
}
