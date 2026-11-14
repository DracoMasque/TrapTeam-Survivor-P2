using JetBrains.Annotations;
using UnityEngine;

public abstract class Etat : MonoBehaviour
{
    public Cooldown duration;

    void Start()
    {
        duration.OnTimeout = EndEtat;
    }
    
    public abstract void StartEtat();

    public abstract void EndEtat();
}
