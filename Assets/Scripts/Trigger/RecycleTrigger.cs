using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleTrigger : Trigger
{
    override public void Action(GameObject g)
    {
        g.SetActive(false);
        g.GetComponent<Ingredient>().Recycled();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = transform.position;
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
