using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotatable : MonoBehaviour {
    public bool isMouseRotating = false;
    public Vector3 mousePos;
    public Camera cam;

    public float rotationSpeed = 10f;
    void Start() {
        cam = Camera.main;
    }

    private void OnMouseDrag() {
        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed;
        var transform1 = cam.transform;
        Vector3 rightRot = Vector3.Cross(transform1.up, transform.position - transform1.position);
        Vector3 upRot = Vector3.Cross(transform.position - transform1.position, rightRot);
        transform.rotation = Quaternion.AngleAxis(-rotationX, upRot) * transform.rotation;
        transform.rotation = Quaternion.AngleAxis(rotationY, rightRot) * transform.rotation;
        
    }
}
