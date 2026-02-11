using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private int maxBullets = 7;
    [SerializeField] private int currentBullets;
    [SerializeField] private Animator handAnimator;
    [SerializeField] private Animator firePointAnimator;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    private bool isReloading = false;
    [SerializeField]private Text bulletsText;
    private PlayerMovement movement;
        public static PlayerShoot instance;


    void Start()
    {
        currentBullets = maxBullets;
        bulletsText.text = currentBullets.ToString() + " / " + maxBullets.ToString();
    }
    void Awake()
    {
        instance = this;
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(movement.isMoving) {
            return;
        }
        if(PauseScript.instance.isPaused) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ReLoad();
        }
    }

    private void Shoot()
    {
        if(isReloading)
        {
            return;
        }
        
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        firePointAnimator.SetTrigger("shoot");
        SFXManager.instance.PlaySFX(shootSound, transform, 0.5f);
        currentBullets--;
        bulletsText.text = currentBullets.ToString() + " / " + maxBullets.ToString();
        if(currentBullets <= 0)
        {
            ReLoad();
            return;
        }
    }

    private void ReLoad()
    {
        if (isReloading) return;
        isReloading = true;
        handAnimator.SetBool("isReloading", true);
        SFXManager.instance.PlaySFX(reloadSound, transform, 0.5f);
        bulletsText.text = "Reloading...";
        StartCoroutine(EndReload());
    }

    private IEnumerator EndReload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentBullets = maxBullets;
        isReloading = false;
        bulletsText.text = currentBullets.ToString() + " / " + maxBullets.ToString();
        handAnimator.SetBool("isReloading", false);
    }

    public void RefillBullets()
    {
        currentBullets = maxBullets;
    }
    
}
