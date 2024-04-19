using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickUnbuild : MonoBehaviour
{
    public Renderer rend;

    public CapsuleCollider collider;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        isActive = false;    
    }

    
    private void OnTriggerEnter(Collider collision) 
    {
        if (collision.tag == "Player" && !isActive)
        {
            rend.enabled = true;
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider collision) 
    {
        if (collision.tag == "Player")
        {
            collider = GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                // Tắt cap collider
                collider.enabled = false;
            }
        }
    }
}
