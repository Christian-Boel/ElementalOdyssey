using UnityEngine;

[CreateAssetMenu(fileName = "SpriteKeyframeCollection", menuName = "ScriptableObjects/SpriteKeyframeCollection")]
public class SpriteKeyframeCollection : ScriptableObject
{
    public SpriteKeyframeData[] keyframes;  // Array to hold all sprite keyframes
}
