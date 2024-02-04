using UnityEngine;

public class OrbitFollower : MonoBehaviour
{
    public Transform parent;
    public GameManager gameManager;
    
    

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        Vector3 currentParentRotation = parent.rotation.eulerAngles;
        float xRotation = Mathf.PerlinNoise(5000 + Time.time / 500.0f, this.GetInstanceID());
        float yRotation = Mathf.PerlinNoise(10000 + Time.time / 500.0f, this.GetInstanceID());
        float zRotation = Mathf.PerlinNoise(Time.time / 500.0f, this.GetInstanceID());
        xRotation -= 0.5f;
        yRotation -= 0.5f;
        zRotation -= 0.5f;
        xRotation = Mathf.Clamp(xRotation, 0.0f, 1.0f);
        yRotation = Mathf.Clamp(yRotation, 0.0f, 1.0f);
        zRotation = Mathf.Clamp(zRotation, 0.0f, 1.0f);
        float multiplier = 5.0f * gameManager.getShipSpeedMultiplier();
        xRotation *= multiplier;
        yRotation *= multiplier;
        zRotation *= multiplier;
        parent.transform.rotation = Quaternion.Euler(currentParentRotation.x + xRotation,
            currentParentRotation.y + yRotation,
            currentParentRotation.z + zRotation);
    }
}