using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public VisualEffect muzzleFlashVFX; // VFX Component
    public float muzzleFlashDuration = 0.05f; // Efektin süresi
    public VisualEffect impactEffect;

    public AudioSource shootAudio;

    private void Start()
    {
        muzzleFlashVFX.Stop();
        shootAudio.Stop();
        shootAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }
    }

    void Shoot()
    {
        
        StartCoroutine(PlayVFX());

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.LogWarning("merminin degdigi cisim: "+hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            VisualEffect impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.9f);
        }
    }


    IEnumerator PlayVFX()
    {
        

        muzzleFlashVFX.Play();
        shootAudio.Play();

        yield return new WaitForSeconds(0.17f);

        muzzleFlashVFX.Stop();
    }
}