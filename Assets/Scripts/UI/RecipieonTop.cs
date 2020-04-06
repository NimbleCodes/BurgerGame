using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipieonTop : MonoBehaviour
{
    public GameObject imageObj;
    public Image myImage;
    public string[] RecipietoShow;

    public void putImageonTop(){//find image object as a tag, then put sprite following string[]
            
            BurgerRecipe.burgerRec.currrecTotop(ref RecipietoShow);
            Debug.Log(RecipietoShow[0]);
            for(int i=0; i < 6; i++){
                imageObj = GameObject.FindGameObjectWithTag("T_image"+(i+1));
                myImage = imageObj.GetComponent<Image>();
                if(i < RecipietoShow.Length){
                    myImage.sprite = Resources.Load<Sprite>("Sprites/Ingredients/" + RecipietoShow[i]);
                    myImage.enabled = true;
                }else{
                    myImage.enabled=false;
                }
        }
        
    }


    void Start()
    {
        putImageonTop();
    }
}
