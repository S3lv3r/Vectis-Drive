using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    RectTransform rt_Inicio;
    RectTransform rt_Perfil;
    RectTransform rt_Opciones;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        rt_Inicio = Btn_Inicio_Img.GetComponent<RectTransform>();
        rt_Perfil = Btn_Perfil_Img.GetComponent<RectTransform>();
        rt_Opciones = Btn_Opciones_Img.GetComponent<RectTransform>();

    }

    /* 

        Inicio

        NORMAL
        widht 280 | height 41
        PosX -740.8 PosY 450

        CLICKED
        
        widht 296 | height 117
        PosX -740.8 PosY 483

        -----------------------------------

        Perfil

        NORMAL
        widht 280 | height 41
        PosX -422 PosY 450

        CLICKED
        
        widht 296 | height 117
        PosX -422 PosY 483

        ------------------------------------

        Opciones

        NORMAL
        widht 280 | height 41
        PosX -103 PosY 450

        CLICKED
        
        widht 296 | height 117
        PosX -103 PosY 483
    
    */

    [Header("Imagenes")]
    public Image Btn_Inicio_Img;
    public Image Btn_Perfil_Img;
    public Image Btn_Opciones_Img;

    [Header("Sprites")]
    public Sprite Inicio_N;
    public Sprite Inicio_C;
    public Sprite Perfil_N;
    public Sprite Perfil_C;
    public Sprite Opciones_N;
    public Sprite Opciones_C;

    [Header("Panels")]
    public GameObject Pnl_Inicio;
    public GameObject Pnl_InBuilt;

    private void ActualizarBoton(RectTransform boton, Sprite sprite, Vector2 posicion, Vector2 tamaño)
    {
        boton.GetComponent<Image>().sprite = sprite;
        boton.anchoredPosition = posicion;
        boton.sizeDelta = tamaño;
    }

    public void Btn_Inicio_Header()
    {
        Pnl_InBuilt.SetActive(false);
        Pnl_Inicio.SetActive(true);

        ActualizarBoton(rt_Inicio, Inicio_C, new Vector2(-740.8f, 483f), new Vector2(296, 117));
        ActualizarBoton(rt_Perfil, Perfil_N, new Vector2(-422f, 450f), new Vector2(280, 41));
        ActualizarBoton(rt_Opciones, Opciones_N, new Vector2(-103f, 450f), new Vector2(280, 41));
    }

    public void Btn_Perfil_Header()
    {
        Pnl_InBuilt.SetActive(true);
        Pnl_Inicio.SetActive(false);

        ActualizarBoton(rt_Inicio, Inicio_N, new Vector2(-740.8f, 450f), new Vector2(280, 41));
        ActualizarBoton(rt_Perfil, Perfil_C, new Vector2(-422f, 483f), new Vector2(296, 117));
        ActualizarBoton(rt_Opciones, Opciones_N, new Vector2(-103f, 450f), new Vector2(280, 41));
    }

    public void Btn_Opciones_Header()
    {
        Pnl_InBuilt.SetActive(true);
        Pnl_Inicio.SetActive(false);

        ActualizarBoton(rt_Inicio, Inicio_N, new Vector2(-740.8f, 450f), new Vector2(280, 41));
        ActualizarBoton(rt_Perfil, Perfil_N, new Vector2(-422f, 450f), new Vector2(280, 41));
        ActualizarBoton(rt_Opciones, Opciones_C, new Vector2(-103f, 483f), new Vector2(296, 117));
    }

    public void Btn_Taller()
    {
        SceneManager.LoadScene("Taller");
    }

    public void Btn_Drive()
    {
        SceneManager.LoadScene("City");
    }

    public void Btn_PageWeb()
    {
        Application.OpenURL("https://vectisdrive.github.io/");
    }


    public void Salir()
    {
        Application.Quit();
    }
}
