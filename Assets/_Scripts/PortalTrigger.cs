using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public GameObject Panel_RawaPening, Panel_JakaPoleng, Panel_CurugCipendokBanyumas, Panel_TelagaMenjer, Panel_MisteriBahurekso, Panel_DesaKaliKebo;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Panel_RawaPening.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Panel_RawaPening.SetActive(false);
        }
    }
}