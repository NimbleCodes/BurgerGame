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
            string spritePath = "Sprites/ingre_rail/" + _ingrName;
            gameObject.AddComponent<SpriteRenderer>();
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
            gameObject.GetComponent<Transform>().localScale = new Vector3(3,3);
            gameObject.AddComponent<BoxCollider2D>();
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.layer = LayerMask.NameToLayer("Ingredients");
        }
    }
    public string ingrClass
    {
        get { return _ingrClass; }
        set { _ingrClass = value; }
    }
}
