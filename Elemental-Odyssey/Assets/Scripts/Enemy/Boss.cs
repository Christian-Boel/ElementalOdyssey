using System.Collections;
using UnityEngine;

public class Boss : Enemy, IAttackedPossible
{
    // Boss-specific properties
    [Header("Boss Phases")]
    public BossPhase currentPhase = BossPhase.PhaseOne;
    public bool isInvulnerable = false;
    public bool isPhaseTransitioning = false;
    public BossPhaseData[] phaseData; // Array to hold data for each phase
    public float hpPercentage = 100;
    [Header("Minion Spawner")]
    public EnemySpawner minionSpawner; // Reference to the minion spawner
    public int currentPhaseIndex = 0; // Index to track current phase


    protected override void Start()
    {
        base.Start();

        if (phaseData.Length == 0)
        {
            Debug.LogError("Boss - Phase data is not set!");
            return;
        }

        // Sort phaseData by phaseThreshold in descending order
        System.Array.Sort(phaseData, (a, b) => b.phaseThreshold.CompareTo(a.phaseThreshold));

        ApplyPhaseSettings(currentPhaseIndex);
    }

    public override void TakeDmg(float dmg)
    {
        if (isInvulnerable)
            return;

        base.TakeDmg(dmg);

        CheckPhaseTransition();
    }

    private void CheckPhaseTransition()
    {
        if (isPhaseTransitioning)
            return;

        hpPercentage = hp / maxHp;
        
        // Check if we can transition to the next phase
        if (currentPhaseIndex + 1 < phaseData.Length)
        {
            var nextPhaseData = phaseData[currentPhaseIndex + 1];
            if (hpPercentage <= nextPhaseData.phaseThreshold)
            {
                StartCoroutine(TransitionToPhase(currentPhaseIndex + 1));
            }
        }
    }

    private IEnumerator TransitionToPhase(int newPhaseIndex)
    {
        isPhaseTransitioning = true;

        Debug.Log("transitioning to "+ newPhaseIndex.ToString());
        // Become invulnerable
        isInvulnerable = true;

        var data = phaseData[newPhaseIndex];

        // Update to the new phase
        currentPhaseIndex = newPhaseIndex;
        ApplyPhaseSettings(currentPhaseIndex);
        
        yield return new WaitForSeconds(data.invulnerableDuration);

        // End invulnerability
        isInvulnerable = false;
        isPhaseTransitioning = false;
    }

    private void ApplyPhaseSettings(int phaseIndex)
    {
        var data = phaseData[phaseIndex];
        var colour = data.phaseColor;
        currentPhase = data.phase;
    
        foreach (var sr in spriteRenderers)
        {
            sr.color = colour;
        }

        // Update originalColors to the new phase colors
        originalColors.Clear();
        foreach (var sr in spriteRenderers)
        {
            originalColors.Add(sr.color);
        }

        speed = data.speed;
        detectionRange = data.detectionRange;

        if (data.spawnMinions && minionSpawner)
        {
            minionSpawner.ActivateSpawner();
        }
        else
        {
            minionSpawner.DeactivateSpawner();
        }
    }
}
