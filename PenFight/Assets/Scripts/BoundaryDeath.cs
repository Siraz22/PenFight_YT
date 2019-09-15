using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pen"))
        {
            collision.gameObject.GetComponent<PenRot>().GameOver();
        }
    }
}
