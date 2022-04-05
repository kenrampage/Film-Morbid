using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cigaretteSkull : MonoBehaviour
{
    public playerInteractionState playerInteractionStateScript;
    public inventoryManager inventoryScript;
    public GameObject cigLighterCollider;
    public GameObject cigLighterParent;
    public GameObject skull;
    public GameObject skullHinge;
    public GameObject skullCigarette;
    public ParticleSystem particle_cigarette;
    public ParticleSystem particle_eye1;
    public ParticleSystem particle_eye2;
    public ParticleSystem particle_head;

    private void Start()
    {
        //assign shit
        skull = this.gameObject.transform.GetChild(1).gameObject;
        skullHinge = this.gameObject.transform.GetChild(1).GetChild(2).gameObject;
        playerInteractionStateScript = GameObject.Find("PLAYER").GetComponent<playerInteractionState>();
        inventoryScript = GameObject.Find("PLAYER").GetComponent<inventoryManager>();
        cigLighterCollider = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        cigLighterParent = this.gameObject.transform.GetChild(0).gameObject;

        skullCigarette = GameObject.Find("skull cigarette");
        
        particle_cigarette = GameObject.Find("particle_skull cigarette").GetComponent<ParticleSystem>();
        particle_eye1 = GameObject.Find("particle_skull smoke1").GetComponent<ParticleSystem>();
        particle_eye2 = GameObject.Find("particle_skull smoke2").GetComponent<ParticleSystem>();
        particle_head = GameObject.Find("particle_skull smoke head").GetComponent<ParticleSystem>();

        skullCigarette.SetActive(false);
        skullHinge.transform.localRotation = Quaternion.Euler(0, 0, 0);

    }

    private void Update()
    {
        disableInteractableTag();
        pickUpCigarettes();
        offerCigarette();
    }

    void pickUpCigarettes()
    {
        RaycastHit hit;
        Camera playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        float interactionDistance = 4;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == cigLighterCollider
                && Input.GetKeyDown("e")
                && playerInteractionStateScript.playerIsAllowedToInteract)
            {
                inventoryScript.dropObjects();
                inventoryScript.playerHolding_cigarettes = true;
                inventoryScript.speaker_pickupsound.PlayOneShot(inventoryScript.pickupsound_cigarettes);
                cigLighterParent.SetActive(false);
            }
        }
    }

    void disableInteractableTag()
    {
        if (inventoryScript.playerHolding_cigarettes)
        {
            skull.tag = "interactable";
            skullHinge.tag = "interactable";
        }
        else
        {
            skull.tag = "Untagged";
            skullHinge.tag = "Untagged";
        }
    }

    void offerCigarette()
    {
        RaycastHit hit;
        Camera playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        float interactionDistance = 4;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == skull
            && playerInteractionStateScript.playerIsAllowedToInteract
            && inventoryScript.playerHolding_cigarettes
            && Input.GetKeyDown("e"))
            {
                StartCoroutine(skullOpening());
            }
        }
    }

    public IEnumerator skullOpening()
    {
        inventoryScript.dropObjects();
        skullCigarette.SetActive(true);
        particle_cigarette.Play();
        yield return new WaitForSeconds(2);
        particle_eye1.Play();
        particle_eye2.Play();
        yield return new WaitForSeconds(2);
        skullHinge.transform.localRotation = Quaternion.Euler(-79.47f, -116.854f, 119.919f);
        particle_head.Play();
        yield return new WaitForSeconds(2);
        particle_eye1.Stop();
        particle_eye2.Stop();
        particle_head.Stop();
    }
}
