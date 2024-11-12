using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private AudioClip heroDamageSoundClip;
    public float attackDamage = 10f;
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
        Debug.Log("Hit Enemy");
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IAttackedPossible>().TakeDmg(attackDamage);
            SoundFXManager.instance.PlaySoundFXClip(heroDamageSoundClip, transform, 1f); // play sound
        }
    }
}
