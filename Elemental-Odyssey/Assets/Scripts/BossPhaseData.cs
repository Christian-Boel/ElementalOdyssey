using UnityEngine;

[System.Serializable]
public class BossPhaseData
{
    public BossPhase phase;             // The phase this data represents
    public float phaseThreshold;        // HP percentage threshold to trigger this phase
    public float speed;                 // Boss speed during this phase
    public float detectionRange;        // Detection range during this phase
    public Color phaseColor;          // Sprite to display during this phase
    public bool spawnMinions;           // Whether to spawn minions during this phase
    public float invulnerableDuration;  // Duration of invulnerability during phase transition
}