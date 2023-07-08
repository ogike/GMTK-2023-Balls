using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowPanel : MonoBehaviour
{

    public GameObject modalWindowPanel;
    
    

    public Button okButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
        okButton.onClick.AddListener(() =>
        {
            Debug.Log("Hello?");
            modalWindowPanel.SetActive(false);
        });
        
        modalWindowPanel.SetActive(true);
    }
    
    internal void showModalWindow()
    {
        modalWindowPanel.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
