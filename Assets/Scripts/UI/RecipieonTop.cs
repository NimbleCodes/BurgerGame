using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecipieonTop : MonoBehaviour
{
    public GameObject imageObj;
    public Image myImage;
    public Image myPanel;
    public string[] RecipietoShow;

    public void putImageonTop(){//find image object as a tag, then put sprite following string[]
            
            BurgerRecipe.burgerRec.currrecTotop(ref RecipietoShow);
            Debug.Log(RecipietoShow[0]);
            for(int i=0; i < 6; i++){
                imageObj = GameObject.FindGameObjectWithTag("T_image"+(i+1));//태그로서 이미지 오브젝트를 찾아옴
                myImage = imageObj.GetComponent<Image>();
                myPanel = GameObject.FindGameObjectWithTag("T_Panel"+(i+1)).GetComponent<Image>();//태그로서 패널 찾아옴
                myPanel.color = UnityEngine.Color.black;//패널 색상 초기화
                if(i < RecipietoShow.Length){//보여줄 레시비 갯수 만큼만 이미지를 보여주고 나머지는 비활성화
                    myImage.sprite = Resources.Load<Sprite>("Sprites/Ingredients/" + RecipietoShow[i]);
                    myImage.enabled = true;
                }else{
                    myImage.enabled=false;
                }
        }
        
    }
    public void changeColor(bool change){  //이벤트 등록해서 호출
            putImageonTop();  
    }

    void Start()
    {
        EventManager.eventManager.BurgerCompleteEvent += changeColor;//이벤트 등록
        putImageonTop();
    }
}
