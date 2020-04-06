using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipieonTop : MonoBehaviour
{
    public static RecipieonTop recipiontop;//singleton call
    public GameObject imageObj;
    public Image myImage;
    public string[] RecipietoShow;
    public void putImageonTop(){//find image object as a tag, then put sprite following string[]
            ObjectManager.objectManager.GetSpawnableObjTypes(ref RecipietoShow);
            for(int i=1; i < RecipietoShow.Length; i++){
            imageObj = GameObject.FindGameObjectWithTag("userTag"+i);
            myImage.sprite = Resources.Load<Sprite>(RecipietoShow[i-1]) as Sprite;
        }
    }


    void Start()
    {
        myImage = imageObj.GetComponent<Image>();
        putImageonTop();
    }
}
