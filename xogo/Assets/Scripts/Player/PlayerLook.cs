using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera lookCamera;
    private float xRotation = 0f;

    public float xSensivity = 30f;
    public float ySensivity = 30f;

    //tiven que usar smoothDeltatime senon iba a tropezons o movimiento da camara (buscar porque)
    public void ProcessCameraRotation(Vector2 input){
        float mouseX = input.x;
        float mouseY = input.y;

        //calculo a rotacion da camara pa mirar pa arriba e pa abaixo
        xRotation -= (mouseY * Time.smoothDeltaTime) * ySensivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //aplicamos a rotacion a camara
        lookCamera.transform.localRotation = Quaternion.Euler(xRotation,0,0);

        //roto o xogador
        transform.Rotate(Vector3.up * (mouseX*Time.smoothDeltaTime) * ySensivity);
    }
}
