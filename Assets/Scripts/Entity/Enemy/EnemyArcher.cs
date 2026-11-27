using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : EnemyPrefab
{
     [SerializeField] private Arrow arrow;
     [SerializeField] private float arrowSpeed;
     
     public override void Attack()
     {
          bool negate = false;
          if (timer.finished && IsClose())
          {
               animator.SetBool("Attacking", true);
               if (etat != null && etat.GetComponent<Confusion>())
               {
                    ThrowArrow(closestEnemy);
               }
               else
               {
                    foreach (Equipement e in PlayerEquiment.instance.equipements)
                    {
                         if (e.GetComponent<AilesGorgone>() && e.GetComponent<AilesGorgone>().negateChance <= Random.Range(0, 101))
                         {
                              e.EquipementAction();
                              negate = true;
                              break;
                         }
                    }
                    if (!negate)
                    {
                         ThrowArrow(player);
                    } 
               }
            
               timer.Play();
          }
     }

     private void ThrowArrow(GameObject target)
     {
          var d = Mathf.Round((attack * (1 + (damage / 100))) * (1 - (Controlleur.instance.defense / 100)));
          GameObject newArrow = Instantiate(arrow.gameObject, transform.position, transform.rotation);
          newArrow.GetComponent<Arrow>().Init(target, d, arrowSpeed);
     }
     public override void Walk(Vector2 player_pos)
     {
          
          float currentDistance = Vector2.Distance(transform.position, player_pos);
          
          if (currentDistance >= range)
          {
               transform.position = Vector2.MoveTowards(
                    transform.position, 
                    player_pos, 
                    (speed) * Time.deltaTime
               ); 
          }
          
          spriteRenderer.flipX = (transform.position.x - player_pos.x > 0);
     }
}
