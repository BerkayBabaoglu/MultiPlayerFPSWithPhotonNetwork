
using Photon.Pun;

using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Realtime;
using ExitGames.Client.Photon;

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
        shootAudio.Stop();
    }

    void Update()
    {

        if (PV.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                PV.RPC("Shoot", RpcTarget.All, PhotonNetwork.LocalPlayer);

            }
        }
    }

    [PunRPC]
    void Shoot(Player shooter)

    {
        StartCoroutine(PlayVFX());

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.LogWarning("Merminin deÄdiÄi cisim: " + hit.transform.name);


            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                if (!CanDamage(shooter, target.PV.Owner))
                {
                    Debug.Log("Friendly fire! Hasar verilmedi.");
                    return;
                }

                target.TakeDamage(damage);
            }

            VisualEffect impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact.gameObject, 0.9f);
        }
    }


    bool CanDamage(Player shooter, Player target)
    {
        if (shooter == null || target == null) return true;

        string shooterTeam = shooter.CustomProperties.ContainsKey("Team") ? (string)shooter.CustomProperties["Team"] : "None";
        string targetTeam = target.CustomProperties.ContainsKey("Team") ? (string)target.CustomProperties["Team"] : "None";

        return shooterTeam != targetTeam; // eger farkli takimdalarsa hasar ver
    }


    IEnumerator PlayVFX()
    {
        muzzleFlashVFX.Play();
        shootAudio.Play();
        yield return new WaitForSeconds(0.17f);
        muzzleFlashVFX.Stop();
    }
}
