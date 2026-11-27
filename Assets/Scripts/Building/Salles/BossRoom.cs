using UnityEngine;

public class BossRoom : MonoBehaviour
{
    public Cooldown timeToRespawn;
    [SerializeField] private Boss boss;

    void Start()
    {
        timeToRespawn.OnTimeout = RespawnBoss;
    }

    public void BossDied()
    {
        timeToRespawn.Play();
    }

    public void RespawnBoss()
    {
        boss.Revive();
    }
}
