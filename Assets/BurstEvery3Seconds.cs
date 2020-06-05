using System.Collections;
using UnityEngine;

public class BurstEvery3Seconds : MonoBehaviour
{
    public GameObject burstEffect;

    private void Awake()
    {
        if(burstEffect == null)
        {
            this.enabled = false;
            return;
        }
        StartCoroutine("BurstCoroutine");
    }

    IEnumerator BurstCoroutine()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(burstEffect, transform.position, transform.rotation);
        StartCoroutine("BurstCoroutine");
    }
}
