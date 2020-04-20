using UnityEngine;

public class TrackActive : MonoBehaviour
{
    private void OnEnable()
    {
        ObjectManager.objectManager.AddToActiveList(gameObject);
    }
    private void OnDisable()
    {
        ObjectManager.objectManager.RemoveFromActiveList(gameObject);
    }
}
