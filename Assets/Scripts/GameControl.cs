using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class ingredient{
    //재료의 분류 (고기, 야채, 소스 등등)
    public string ingreClass;
    //재료의 이름
    public string ingreName;
    //재료의 Sprite 경로
    public Sprite ingreSprite{
        get{
            return ingreSprite;
        }   
        set{
            if(ingreName != null){
                ingreSprite = Resources.Load("Sprites/" + ingreName + ".png") as Sprite;
            }
        }
    }
}


public class GameControl : MonoBehaviour
{   

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
