using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        float val = 1 + (GameManager.instance.getShipCapacity() * 0.05f);
        this.transform.localScale = new Vector3(val, val, val);
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;
        if (tag == "Scrap") {
            Destroy(other.gameObject);
            GameManager.instance.addScrap();
        }
    }
}
