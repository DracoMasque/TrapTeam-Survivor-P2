using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyClerc : EnemyPrefab
{
     [SerializeField] private DetectEnemy healDetection;
     
     public override void Attack()
     {
          bool negate = false;
          if (timer.finished && IsClose())
          {
               print("heal att");
               animator.SetBool("Attacking", true);
               if (etat != null && etat.GetComponent<Confusion>())
               {
                    //ThrowArrow(closestEnemy);
               }
               else
               {
                    Heal(player);
                   
               }
            
               timer.Play();
          }
     }

     private void Heal(GameObject target)
     {
          if (healDetection.enemies.Count <= 0) return;

          foreach (var ht in healDetection.enemies)
          {
               ht.GetComponent<EnemyPrefab>().Heal(1);
          }
     }
     
     /*private List<EnemyPrefab> GetClosests()
     {
          List<EnemyPrefab> healTargets = new List<EnemyPrefab>();
          healTargets.Add(healDetection.enemies[0].GetComponent<EnemyPrefab>());
          foreach (var target in healDetection.enemies)
          {
               for (int i = 0; i <= healTargets.Count - 1; i++)
               {
                    var previous = Math.Clamp(i - 2, 0, i);
                    print(previous);
                    var distanceTarget = target.transform.position - transform.position;
                    var distanceTargetPrevious = healTargets[previous].transform.position - transform.position;
                    print(i + " count " + healTargets.Count);
                    var distanceT = healTargets[i].transform.position - transform.position;

                    if (distanceTarget.magnitude < distanceT.magnitude &&
                        distanceTarget.magnitude >= distanceTargetPrevious.magnitude)
                    {
                         healTargets.Insert(i, target.GetComponent<EnemyPrefab>());
                    }
               }
          }
          return healTargets;
     }*/
     public override void Walk(Vector2 player_pos)
     {
          float currentDistance = Vector2.Distance(transform.position, player_pos);
          
          transform.position = Vector2.MoveTowards(
               transform.position, 
               player_pos, 
               (speed) * Time.deltaTime
          ); 
          
          spriteRenderer.flipX = (transform.position.x - player_pos.x > 0);
     }
}
