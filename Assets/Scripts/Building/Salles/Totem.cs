using UnityEngine;

public class Totem : MonoBehaviour
{
    public delegate void FiringDelegate();
    public FiringDelegate OnAnimationEvent;
    public delegate void AnimationEndedDelegate();
    public AnimationEndedDelegate OnAnimationEnded;

    [SerializeField] private string namep = "";
    
    void AnimationEvent()
    {
        if (OnAnimationEvent != null) {
            OnAnimationEvent();
        }
    }

    void AnimationEnded()
    {
        if (OnAnimationEnded != null) {
            OnAnimationEnded();
        }
    }

    void Update()
    {
        if (namep != "" )
        {
            print( namep +" : "+ transform.position + " player : " + Controlleur.instance.gameObject.transform.position);
        }
    }
}
