using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Disablebutton : MonoBehaviour
{
    [SerializeField]
    private Button b_button;

    public void disableButton(){
        b_button.gameObject.SetActive(false);
    }




}
