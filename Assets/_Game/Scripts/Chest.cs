using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private GameObject chestClosed;

    public static Chest instance;

    void Awake()
    {
        instance = this;
    }


    public void ChestOpen()
    {
        chestClosed.gameObject.SetActive(false);
        chestOpen.gameObject.SetActive(true);
    }

    public void ChestClose()
    {
        chestClosed.gameObject.SetActive(true);
        chestOpen.gameObject.SetActive(false);
    }

    
}
