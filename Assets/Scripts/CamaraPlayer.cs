using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CamaraPlayer : MonoBehaviour
{
    [Header("Control de la cámara")]
    public float sensibilidadMouse = 100f;

    [Header("Restricciones de movimiento")]
    public float limiteRotacionX = 90f; 

    private float rotacionX = 0f;
    private float rotacionY = 0f;

    [Header("CrossHair")]

    public Image Crosshair;
    public Sprite Dot;
    public Sprite Hand;
    public Sprite Set;

    [Header("Instructions")]
    public Image ImgInstruction;
    public Image ImgInstruction2;
    public Sprite InsNothing;
    public Sprite InsTake;
    public Sprite InsThrow;
    public Sprite InsSet;
    public Sprite InsDrive;

    [Header("Partes del Camion")]
    public TruckParts TruckPartsScript;

    [Header("Pickup Settings")]
    public float pickupDistance = 2f; // Distancia de recolección

    private GameObject piezaSostenida = null;
    private bool sosteniendoPieza = false;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ImgInstruction.sprite = InsNothing;
        ImgInstruction2.sprite = InsNothing;

        
    }

    public Vector3 ScreenCenter2;
    public Ray rays2;
    public RaycastHit hit2;

        

    void Update()
    {
        RotacionCamara();
        MoverPieza();

        if (sosteniendoPieza && piezaSostenida != null && Input.GetKeyDown(KeyCode.Q))
        {
            SoltarPieza();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }
    void LateUpdate()
    {
        DispararRaycast();
        if (sosteniendoPieza && piezaSostenida != null)
        {
            piezaSostenida.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        }

        ScreenCenter2 = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        rays2 = Camera.main.ScreenPointToRay(ScreenCenter2);
        Debug.DrawRay(rays2.origin, rays2.direction * 2f, Color.blue);

        if (sosteniendoPieza)
        {
            ImgInstruction.sprite = InsThrow;
            if (Physics.Raycast(rays2, out hit2, Mathf.Infinity))
            {
                if (TruckPartsScript.PartsCamionTresEjesOFF.Contains(hit2.collider.name))
                {
                    Crosshair.sprite = Set; 
                    ImgInstruction.sprite = InsSet;
                }
                else
                {
                    Crosshair.sprite = Dot; 
                }
            }
            else
            {
                Crosshair.sprite = Dot; 
            }
        }
    }

    void DispararRaycast()
    {
        Vector3 ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray rays = Camera.main.ScreenPointToRay(ScreenCenter);
        RaycastHit hit;

        Debug.DrawRay(rays.origin, rays.direction * 2f, Color.red);

        
        if (Physics.Raycast(rays, out hit, Mathf.Infinity))
        {
            
            if (TruckPartsScript.PartsCamionTresEjesON.Contains(hit.collider.name) || hit.collider.CompareTag("PiezaSoltada"))
            {
                if(DriveTruck())
                {
                    ImgInstruction2.sprite = InsDrive;
                    if(Input.GetKeyDown(KeyCode.F))
                    {
                        SceneManager.LoadScene("City");
                    }
                }
                Crosshair.sprite = Hand;
                ImgInstruction.sprite = InsTake;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ImgInstruction.sprite = InsThrow;
                    if (!sosteniendoPieza) 
                    {
                        ImgInstruction.sprite = InsThrow;
                        if (TruckPartsScript.PartsCamionTresEjesON.Contains(hit.collider.name)) 
                        {
                            
                            hit.collider.gameObject.SetActive(false);

                            int index = TruckPartsScript.PartsCamionTresEjesOFF.IndexOf(hit.collider.name + "OFF");
                            if (index != -1)
                            {
                                string NameOfObject = TruckPartsScript.PartsCamionTresEjesOFF[index];
                                Transform objectOff = TruckPartsScript.Camion_Tres_Ejes_OFF.transform.Find(NameOfObject);
                                if (objectOff != null)
                                {
                                    objectOff.gameObject.SetActive(true);
                                }
                            }

                            // Crear el clon de la pieza original
                            piezaSostenida = Instantiate(hit.collider.gameObject, hit.point, hit.collider.transform.rotation);
                            piezaSostenida.transform.localScale *= 0.5f;
                            piezaSostenida.SetActive(true);
                        }
                        else if (hit.collider.CompareTag("PiezaSoltada")) 
                        {
                            ImgInstruction.sprite = InsThrow;
                            piezaSostenida = hit.collider.gameObject; 
                        }

                        // Configurar físicas y colisión
                        Rigidbody rb = piezaSostenida.GetComponent<Rigidbody>();
                        if (rb == null)
                        {
                            rb = piezaSostenida.AddComponent<Rigidbody>();
                        }
                        rb.isKinematic = true; 
                        rb.useGravity = false;

                        Collider piezaCollider = piezaSostenida.GetComponent<Collider>();
                        if (piezaCollider != null)
                        {
                            piezaCollider.enabled = false; 
                        }

                        piezaSostenida.tag = "PiezaSoltada"; 
                        sosteniendoPieza = true;
                    }
                    else // Si ya sostiene una, la suelta
                    {
                        ImgInstruction.sprite = InsThrow;
                        SoltarPieza();
                    }
                }
            }
            else
            {
                Crosshair.sprite = Dot;
                ImgInstruction.sprite = InsNothing;
                ImgInstruction2.sprite = InsNothing;
            }
        } 
    
    }

    void MoverPieza()
    {
        if (sosteniendoPieza && piezaSostenida != null)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray rays = Camera.main.ScreenPointToRay(screenCenter);
            piezaSostenida.transform.position = rays.origin + rays.direction * 1.5f; 
        }
    }
    void SoltarPieza()
    {
        if (piezaSostenida != null)
        {
            ImgInstruction.sprite = InsThrow;

            string nombrePiezaSostenida = piezaSostenida.name.Replace("(Clone)", "").Trim();


            

            if (Physics.Raycast(rays2, out hit2, Mathf.Infinity))
            {

                if (TruckPartsScript.PartsCamionTresEjesOFF.Contains(hit2.collider.name))
                {
                    
                    
                    Debug.Log("Coinciden");

                    Transform piezaEnOFF = TruckPartsScript.Camion_Tres_Ejes_OFF.transform.Find(nombrePiezaSostenida + "OFF");
                    if (piezaEnOFF != null && hit2.collider == piezaEnOFF.GetComponent<Collider>())
                    {

                        Destroy(piezaSostenida); 

                        piezaEnOFF.gameObject.SetActive(false); 

                        Transform piezaEnON = TruckPartsScript.Camion_Tres_Ejes_ON.transform.Find(nombrePiezaSostenida);
                        if (piezaEnON != null)
                        {
                            piezaEnON.gameObject.SetActive(true); 
                        }
                    }
                }
            }

            Rigidbody rb = piezaSostenida.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; 
                rb.useGravity = true; 
            }

            MeshCollider meshCol = piezaSostenida.GetComponent<MeshCollider>();
            if (meshCol != null && !meshCol.convex)
            {
                meshCol.convex = true;
            }

            Collider piezaCollider = piezaSostenida.GetComponent<Collider>();
            if (piezaCollider != null)
            {
                piezaCollider.enabled = true; 
            }

            piezaSostenida = null;
            sosteniendoPieza = false;
        }

    }

    void RotacionCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;


        rotacionY += mouseX;


        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -limiteRotacionX, limiteRotacionX);


        transform.localRotation = Quaternion.Euler(rotacionX, rotacionY, 0f);
    }

    bool DriveTruck()
    {
        foreach (Transform part in TruckPartsScript.Camion_Tres_Ejes_ON.transform)
        {
            if (!part.gameObject.activeSelf) 
            {
                return false;
            }
        }
        return true;
    }
}
