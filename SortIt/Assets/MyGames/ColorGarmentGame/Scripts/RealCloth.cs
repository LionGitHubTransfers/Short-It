using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCloth : MonoBehaviour
{   
    // Start is called before the first frame update
    public bool HitObst = false;
    ObiParticleAttachment obiattatchment;
    private void Start()
    {
        //obiattatchment = GetComponent<ObiParticleAttachment>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("obst"))
        {
            HitObst = true;
        }
    }
}
