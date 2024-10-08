using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject AO;
    public Vector3 CursorDirection;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 mouseVector = mousePos - transform.position;

            CursorDirection = mouseVector.normalized;

            float angle = Mathf.Atan2(CursorDirection.y, CursorDirection.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
            Instantiate(AO, transform.position + CursorDirection * range, rotation);
        }
    }
}
