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
        float xRotation = Mathf.PerlinNoise1D(5000 + Time.time / 500.0f);
        float yRotation = Mathf.PerlinNoise1D(10000 + Time.time / 500.0f);
        float zRotation = Mathf.PerlinNoise1D(Time.time / 500.0f);
        xRotation -= 0.5f;
        yRotation -= 0.5f;
        zRotation -= 0.5f;
        float multiplier = 5.0f * gameManager.getShipSpeedMultiplier();
        xRotation *= multiplier;
        yRotation *= multiplier;
        zRotation *= multiplier;
        parent.transform.rotation = Quaternion.Euler(currentParentRotation.x + xRotation,
            currentParentRotation.y + yRotation,
            currentParentRotation.z + zRotation);
    }
}