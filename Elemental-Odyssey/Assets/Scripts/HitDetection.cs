using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    [SerializeField] private AudioClip damageSoundClip;
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
        Debug.Log(hpScript);
        hpScript.TakeDmg(dmg);
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, .1f);
    }
}
