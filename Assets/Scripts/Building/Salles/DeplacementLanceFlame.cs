using UnityEngine;
using UnityEngine.UIElements;

public class DeplacementLanceFlame : MonoBehaviour
{
    public Vector2 newScale = new Vector2(0.8f, 0.3f);
    public Vector2 scale = new Vector2(0.001f, 0.3f);
    public float speed;
    
   public void Aller()
   {
       transform.localScale = Vector2.Lerp(transform.localScale, newScale, Time.deltaTime * speed);
       
   }

   public void Retour()
   {
       transform.localScale = Vector2.Lerp(transform.localScale, scale, Time.deltaTime * speed);
   }
}
