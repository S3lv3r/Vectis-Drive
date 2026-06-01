using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamaraController : MonoBehaviour
{

    [Header("Control de la cámara")]
    public float sensibilidadMouse = 100f;

    [Header("Restricciones de movimiento")]
    public float limiteRotacionX = 10f; // Cuánto puede mirar hacia arriba/abajo
    public float limiteRotacionY = 60f; // Cuánto puede girar hacia los lados

    private float rotacionX = 0f; 
    private float rotacionY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    void Update()
    {
        RotacionCamara();
        if(Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Taller");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void RotacionCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;


        rotacionY += mouseX;
        rotacionY = Mathf.Clamp(rotacionY, -limiteRotacionY, limiteRotacionY);


        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -limiteRotacionX, limiteRotacionX);


        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotacionX, rotacionY, 0f), Time.deltaTime * 10f);
    }
}
