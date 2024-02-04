using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;
        if (tag == "Scrap") {
            Destroy(other.gameObject);
            GameManager.instance.addScrap();
        }
    }
}
