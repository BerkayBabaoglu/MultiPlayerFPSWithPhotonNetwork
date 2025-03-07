using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviourPunCallbacks
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public VisualEffect muzzleFlashVFX; 
    public VisualEffect impactEffect;
    public AudioSource shootAudio;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        muzzleFlashVFX.Stop();
        shootAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PV.IsMine) 
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PV.RPC("Shoot", RpcTarget.All); 
            }
        }
    }

    [PunRPC]
    void Shoot()
    {
        StartCoroutine(PlayVFX());

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.LogWarning("Merminin deðdiði cisim: " + hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            VisualEffect impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact.gameObject, 0.9f);
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
