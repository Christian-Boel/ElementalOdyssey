using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    // Boss-specific properties
    [Header("Boss Phases")]
    public BossPhase currentPhase = BossPhase.PhaseOne;
    public BossPhaseData[] phaseData; // Array to hold data for each phase

    [Header("Minion Spawner")]
    public EnemySpawner minionSpawner; // Reference to the minion spawner

    private bool isInvulnerable = false;
    private bool isPhaseTransitioning = false;

    protected override void Start()
    {
        base.Start();
        // Initialize boss-specific setup
        // Ensure phaseData is correctly set up
        if (phaseData.Length == 0)
        {
            Debug.LogError("Boss - Phase data is not set!");
            return;
        }
        ApplyPhaseSettings(currentPhase);
    }

    protected override void Update()
    {
        // Boss doesn't move when invulnerable
        if (isInvulnerable)
            return;

        base.Update();
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
        // Don't transition if already transitioning
        if (isPhaseTransitioning)
            return;

        float hpPercentage = hp / maxHp;

        // Determine the next phase based on HP percentage
        foreach (var data in phaseData)
        {
            if (hpPercentage <= data.phaseThreshold && currentPhase != data.phase)
            {
                StartCoroutine(TransitionToPhase(data.phase));
                break;
            }
        }
    }

    private IEnumerator TransitionToPhase(BossPhase newPhase)
    {
        isPhaseTransitioning = true;

        // Optional: Play transition animation or effect
        // e.g., spriter.PlayAnimation("PHASE_TRANSITION");

        // Become invulnerable
        isInvulnerable = true;

        // Activate minion spawner if specified
        BossPhaseData data = GetPhaseData(newPhase);
        if (data.spawnMinions && minionSpawner)
        {
            minionSpawner.ActivateSpawner();
        }

        // Wait for invulnerability duration
        yield return new WaitForSeconds(data.invulnerableDuration);

        // Update to the new phase
        currentPhase = newPhase;
        ApplyPhaseSettings(currentPhase);

        // End invulnerability
        isInvulnerable = false;
        isPhaseTransitioning = false;
    }

    private void ApplyPhaseSettings(BossPhase phase)
    {
        BossPhaseData data = GetPhaseData(phase);

        // Update stats
        speed = data.speed;
        detectionRange = data.detectionRange;

        // Change sprite or animation
        // if (data.phaseSprite)
        // {
        //     // Assuming you have a SpriteRenderer component
        //     spriteRenderer.sprite = data.phaseSprite;
        // }
        
    }

    private BossPhaseData GetPhaseData(BossPhase phase)
    {
        foreach (var data in phaseData)
        {
            if (data.phase == phase)
                return data;
        }
        Debug.LogError($"Boss - Phase data for {phase} not found!");
        return null;
    }
}
