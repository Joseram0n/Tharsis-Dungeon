using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioClip playerHitSound, shootSound, slashSound, enemyDeathSound, DefeatSound, MainMusic, TownMusic, BossRoar, WinSound, DungeonMusic, pickUpSound, bossAttack, bossDeath, goblinSlash;
    
    private static AudioSource audioSrc;
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("PlayerHit");
        shootSound = Resources.Load<AudioClip>("ArrowShot");
        bossAttack = Resources.Load<AudioClip>("BossAttack");
        bossDeath = Resources.Load<AudioClip>("BossDeath");
        BossRoar = Resources.Load<AudioClip>("BossRoar");
        DungeonMusic = Resources.Load<AudioClip>("DungeonMusic");
        enemyDeathSound = Resources.Load<AudioClip>("GoblinDeath");
        goblinSlash = Resources.Load<AudioClip>("GoblinSlash");
        DefeatSound = Resources.Load<AudioClip>("LostSound");
        MainMusic = Resources.Load<AudioClip>("MenuMusic");
        pickUpSound = Resources.Load<AudioClip>("PickUp");
        slashSound = Resources.Load<AudioClip>("SwordSlash");
        TownMusic = Resources.Load<AudioClip>("TownMusic");

        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = GameVariables.getVolume();

    }

    // Update is called once per frame
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "playerHit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "shoot":
                audioSrc.PlayOneShot(shootSound);
                break;
            case "bossAttack":
                audioSrc.PlayOneShot(bossAttack);
                break;
            case "bossDeath":
                audioSrc.PlayOneShot(bossDeath);
                break;
            case "bossRoar":
                audioSrc.PlayOneShot(BossRoar);
                break;
            case "enemyDeath":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
            case "pickUp":
                audioSrc.PlayOneShot(pickUpSound);
                break;
            case "slash":
                audioSrc.PlayOneShot(slashSound);
                break;
            case "enemyAttack":
                audioSrc.PlayOneShot(goblinSlash);
                break;

            


        }
    }
}
