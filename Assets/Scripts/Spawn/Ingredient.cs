using UnityEngine;

public class Ingredient : MonoBehaviour
{
    string _ingrName;
    string _ingrClass;

    public string ingrName
    {
        get { return _ingrName; }
        //ingrName을 설정하면 이미지를 가지고 온다
        set
        {
            _ingrName = value;
            string spritePath = "Sprites/Ingredients/" + _ingrName;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
        }
    }
    public string ingrClass
    {
        get { return _ingrClass; }
        set { _ingrClass = value; }
    }
}
