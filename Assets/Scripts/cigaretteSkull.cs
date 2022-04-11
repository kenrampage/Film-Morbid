using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private UnityEvent onCigPickup;
    [SerializeField] private UnityEvent onArmsPickup;
    [SerializeField] private UnityEvent onCigGive;

    private void Start()
    {
        //assign shit
        skull = GameObject.Find("skull");
        skullHinge = GameObject.Find("skull hinge");
        playerInteractionStateScript = GameObject.Find("PLAYER").GetComponent<playerInteractionState>();
        inventoryScript = GameObject.Find("PLAYER").GetComponent<inventoryManager>();
        cigLighterCollider = GameObject.Find("ciglighter collider");
        cigLighterParent = GameObject.Find("cig,lighter parent");

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

        //im sorry, im tired
        RaycastHit hit;
        Camera playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        float interactionDistance = 4;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.name == "clock arms")
                {
                    inventoryScript.playerHolding_clockhands = true;
                    onArmsPickup?.Invoke();
                    Destroy(hit.collider.gameObject);
                }
            }
        }

    }

    void pickUpCigarettes()
    {
        RaycastHit hit;
        Camera playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        float interactionDistance = 4;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            Debug.Log("Thy opening has been fulfilled.");
            if (hit.collider.gameObject == cigLighterCollider
                && Input.GetMouseButtonDown(0)
                && playerInteractionStateScript.playerIsAllowedToInteract)
            {
                inventoryScript.dropObjects();
                inventoryScript.playerHolding_cigarettes = true;
                onCigPickup?.Invoke();
                //V: gives null error
                //inventoryScript.speaker_pickupsound.PlayOneShot(inventoryScript.pickupsound_cigarettes);
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
            && Input.GetMouseButton(0))
            {
                StartCoroutine(skullOpening());
            }
        }
    }

    public IEnumerator skullOpening()
    {
        inventoryScript.dropObjects();
        skullCigarette.SetActive(true);
        onCigGive?.Invoke();
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
