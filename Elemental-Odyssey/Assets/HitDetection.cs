using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public IAttackedPossible hpScript;
    // Start is called before the first frame update
    void Start()
    {
        hpScript = GetComponentInParent<IAttackedPossible>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(float dmg)
    {
        hpScript.TakeDmg(dmg);
    }
}
