using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item pickup sound.")]
    public AudioClip ItemSoundClip;
    
    public ItemType itemType;
    
    public int healAmount;
    
    public KeyType keyType;
    
    public Upgrade upgrade;
}