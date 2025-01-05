using System.Collections;
using UnityEngine;

public class mutatorManager : MonoBehaviour
{

    [Header("TTgrassy MADE THIS SCRIPT!")]
    [Header("GIVE CREDITS!")]
    [Header("Message me on discord if you want any more mutators!")]
    [Header("YouTube: @ttgrassy, Discord: @ttgrassy")]

    public float mutatorInterval = 30f;
    private float timer;
    public float gravityMultiplier = 0.17f;

    public GameObject[] gorillaPlayer;
    public string[] mutators = { "LowGravity", "JumpBoost", "Freeze" };

    void Start()
    {
        timer = mutatorInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ActivateRandomMutator();
            timer = mutatorInterval;
        }
    }

    void ActivateRandomMutator()
    {
        string selectedMutator = mutators[Random.Range(0, mutators.Length)];
        Debug.Log("Activated Mutator: " + selectedMutator);

        switch (selectedMutator)
        {
            case "LowGravity":
                SetGravityScale(0.2f);
                break;

            case "JumpBoost":
                SetJumpMultiplier(2f);
                break;

            case "Freeze":
                FreezePlayers(5f);
                break;

            default:
                Debug.Log("Unknown mutator!");
                break;
        }

        StartCoroutine(ResetMutatorsAfterTime(mutatorInterval));
    }

    void SetGravityScale(float scale)
    {
        Physics.gravity = new Vector3(0, -9.81f * gravityMultiplier, 0); // Set to moon-like gravity
    }


    void SetJumpMultiplier(float multiplier)
    {
        foreach (var player in gorillaPlayer)
        {
            var locomotion = player.GetComponent<GorillaLocomotion.Player>();
            if (locomotion != null)
            {
                locomotion.jumpMultiplier *= multiplier;
            }
        }
    }

    void FreezePlayers(float duration)
    {
        foreach (var player in gorillaPlayer)
        {
            var rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }

        StartCoroutine(UnFreezePlayersAfterTime(duration));
    }

    IEnumerator UnFreezePlayersAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        foreach (var player in gorillaPlayer)
        {
            var rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    IEnumerator ResetMutatorsAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        SetGravityScale(1f);
        SetJumpMultiplier(1f);
    }
}
