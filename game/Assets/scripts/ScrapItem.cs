using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapItem : MonoBehaviour
{
    // Start is called before the first frame update
    public float size = 1;
    void Start() {
        size = Random.Range(0.8f, 1.2f);
    }

    // Update is called once per frame
    void Update() {
        float calculatedSize = 0.5f * size;
        transform.localScale = new Vector3(calculatedSize, calculatedSize, calculatedSize);
        size -= Time.deltaTime / 10;
        if (size < 0.1f) {
            Destroy(gameObject);
        }
    }
    
    private void OnMouseDown() {
        GameManager.instance.addScrap();
        Destroy(gameObject);
    }
}
