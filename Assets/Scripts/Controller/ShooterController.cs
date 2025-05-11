using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    Vector2 MousePos
    {
        get
        {
            Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return Pos;
        }
    }

    private void Update()
    {
        Vector2 dir = (Vector2)transform.position - MousePos;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        transform.eulerAngles = new Vector3(0f, 0f, angle + 180f);
    }


}
