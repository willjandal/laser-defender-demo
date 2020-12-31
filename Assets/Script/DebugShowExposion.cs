using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShowExposion : MonoBehaviour
{

    [SerializeField] GameObject explodeVFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowExplosion()
    {
        Debug.Log("Debug button pressed");

        GameObject explosion = Instantiate(explodeVFX,
            transform.position, transform.rotation);
    }
}
