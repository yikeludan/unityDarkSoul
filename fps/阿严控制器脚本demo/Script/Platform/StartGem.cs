using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGem : MonoBehaviour
{
    private Collider collider;
    private MeshRenderer meshRenderer;
    [SerializeField] float restTime = 3.0f;
    private WaitForSeconds waitForSeconds;

    public ParticleSystem pickUpVFX;
    private void Awake()
    {
        this.collider = this.GetComponent<Collider>();
        this.meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        waitForSeconds = new WaitForSeconds(this.restTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.CanAirJump = true;
            this.meshRenderer.enabled = false;
            this.collider.enabled = false;
            Instantiate(this.pickUpVFX, this.transform.position, this.transform.rotation);
            StartCoroutine(RestCoroutine());
        }
    }

    IEnumerator RestCoroutine()
    {
        yield return waitForSeconds;
        this.Reset();
    }

    private void Reset()
    {
        this.meshRenderer.enabled = true;
        this.collider.enabled = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
