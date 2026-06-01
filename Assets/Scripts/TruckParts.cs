using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckParts : MonoBehaviour
{
    [Header("Camiones")]
    public GameObject Camion_Tres_Ejes_ON; // C3ejes
    public GameObject Camion_Tres_Ejes_OFF; // C3ejesOFF


    //-----

    void Start()
    {
        AssingPartsInArray();
    }

    public List<string> PartsCamionTresEjesON = new List<string>();
    public List<string> PartsCamionTresEjesOFF = new List<string>();
    public void AssingPartsInArray()
    {
        foreach(Transform  part in Camion_Tres_Ejes_ON.transform)
        {
            PartsCamionTresEjesON.Add(part.gameObject.name);
            
        }
        foreach(Transform  part in Camion_Tres_Ejes_OFF.transform)
        {
            PartsCamionTresEjesOFF.Add(part.gameObject.name);
            
        }
    }
}
