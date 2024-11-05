using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(DestroyAfterOneFrame());
    }
    IEnumerator DestroyAfterOneFrame()
    {
        yield return new WaitForSeconds(.02f);

        Destroy(this.gameObject);
        Debug.Log("Attacked");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //If the collider is enemy
            Debug.Log("Hit Enemy");
        }
    }
}
