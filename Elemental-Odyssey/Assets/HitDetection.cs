using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{

    public IAttackedPossible enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(float dmg)
    {
        Debug.Log("hitDetection - Hit called with dmg" + dmg);
        enemyScript.TakeDmg(dmg);
    }
}
