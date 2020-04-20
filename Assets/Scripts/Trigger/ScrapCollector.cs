using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
        EventManager.eventManager.Invoke_IngrDestroyedEvent();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position;
        BoxCollider2D temp = GetComponent<BoxCollider2D>();
        Vector2 size = new Vector2(temp.bounds.extents.x, temp.bounds.extents.y);

        Vector3 upperLeft = center + new Vector3(-size.x, size.y, 0);
        Vector3 upperRight = center + new Vector3(size.x, size.y, 0);
        Vector3 lowerLeft = center + new Vector3(-size.x, -size.y, 0);
        Vector3 lowerRight = center + new Vector3(size.x, -size.y, 0);

        Gizmos.DrawLine(upperLeft, upperRight);
        Gizmos.DrawLine(upperRight, lowerRight);
        Gizmos.DrawLine(lowerRight, lowerLeft);
        Gizmos.DrawLine(lowerLeft, upperLeft);
    }
}
