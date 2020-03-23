using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, ISpawnedObject
{
    //재료의 분류 (고기, 야채, 소스 등등)
    public string ingreClass;
    //재료의 이름
    public string ingreName;
    //재료의 Sprite 경로
    public SpriteRenderer ingreSprite;

    /*---------------------------초기화 관련----------------------------*/
    public string getName()
    {
        return ingreName;
    }
    public void setName(string val)
    {
        ingreName = val;
    }
    public void setSprite(Sprite _sprite){
        ingreSprite.sprite = _sprite;
    }
    //ingredient 정보 초기화
    public void initIngre(string _class, string _name){
        ingreClass = _class;
        ingreName = _name;
        ingreSprite.sprite = Resources.Load<Sprite>("Sprites/" + ingreName);
    }
    /*---------------------------초기화 관련----------------------------*/

    /*---------------------------이벤트 관련----------------------------*/
    public void Eaten(){

    }
    public void Recycled(){

    }
    public void Destroyed(){

    }
    public void OnSpawn()
    {
        //do nothing
    }
    /*---------------------------이벤트 관련----------------------------*/

    
    private void Awake() {
        ingreSprite = GetComponent<SpriteRenderer>();
    }
}
