#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SpriteKeyframeExtractor : MonoBehaviour
{
    public AnimationClip clip;                           // The animation clip to extract from
    public string spritePropertyPath = "";               // The path to the Image component
    public string spritePropertyName = "m_Sprite";       // The property name for the sprite
    public SpriteKeyframeCollection keyframeCollection;  // The ScriptableObject to store keyframe data

    [ContextMenu("Extract Sprite Keyframes")]
    void ExtractSpriteKeyframes()
    {
        if (clip == null || keyframeCollection == null)
        {
            Debug.LogError("Please assign the AnimationClip and SpriteKeyframeCollection ScriptableObject.");
            return;
        }

        // Get the object reference curve bindings for the clip
        EditorCurveBinding[] bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);

        foreach (var binding in bindings)
        {
            // Check if the binding is for the Image component's sprite property
            if (binding.propertyName == spritePropertyName && binding.path == spritePropertyPath && binding.type == typeof(Image))
            {
                // Get the sprite keyframes
                ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);

                keyframeCollection.keyframes = new SpriteKeyframeData[keyframes.Length];

                for (int i = 0; i < keyframes.Length; i++)
                {
                    float time = keyframes[i].time;
                    int frame = Mathf.RoundToInt(time * clip.frameRate);

                    keyframeCollection.keyframes[i] = new SpriteKeyframeData
                    {
                        time = time,
                        frame = frame
                    };
                }

                // Save the ScriptableObject asset
                EditorUtility.SetDirty(keyframeCollection);
                AssetDatabase.SaveAssets();

                Debug.Log($"Extracted {keyframes.Length} sprite keyframes from clip '{clip.name}'.");
                return;
            }
        }

        Debug.LogWarning($"Sprite property '{spritePropertyName}' not found in clip '{clip.name}'.");
    }
}
#endif
