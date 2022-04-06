using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject phone;
    public GameObject cigarettes;
    public GameObject clockhands;
    public GameObject sheetmusic;

    public AudioSource speaker_pickupsound;
    public AudioClip pickupsound_phone;
    public AudioClip pickupsound_cigarettes;
    public AudioClip pickupsound_clockhands;
    public AudioClip pickupsound_sheetmusic;

    public bool playerHolding_phone;
    public bool playerHolding_cigarettes;
    public bool playerHolding_clockhands;
    public bool playerHolding_sheetmusic;

    private void Update()
    {
        cigarettes.SetActive(playerHolding_cigarettes);
        clockhands.SetActive(playerHolding_clockhands);
        sheetmusic.SetActive(playerHolding_sheetmusic);
    }

    public void dropObjects()
    {
        playerHolding_cigarettes = false;
        playerHolding_clockhands = false;
        playerHolding_sheetmusic = false;
    }
}
