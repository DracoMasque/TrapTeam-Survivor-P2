using JetBrains.Annotations;
using UnityEngine;

public abstract class Etat : MonoBehaviour
{
    public Cooldown duration;

    void start()
    {
        duration.OnTimeout = EndEtat;
    }

    public abstract void EndEtat();
}
