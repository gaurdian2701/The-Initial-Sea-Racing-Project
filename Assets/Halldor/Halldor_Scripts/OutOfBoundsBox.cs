using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OutOfBoundsBox : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OutOfBoundsRetriever>()._rewind == false)
        {
            Instantiate(particleSystem).transform.position = other.transform.position;
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(.2f);
            other.GetComponent<OutOfBoundsRetriever>().OutOfBounds();
        }
    }
}
