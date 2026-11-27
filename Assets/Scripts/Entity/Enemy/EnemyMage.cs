using UnityEngine;

public class EnemyMage : EnemyPrefab
{
    [SerializeField] private Magic magic;
    [SerializeField] private float magicCooldown;
    [SerializeField] private float spawnRange = 2;
    public override void Attack()
    {
        bool negate = false;
        if (timer.finished && IsClose())
        {
            animator.SetBool("Attacking", true);
            if (etat != null && etat.GetComponent<Confusion>())
            {
                ThrowMagic(closestEnemy);
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
                    ThrowMagic(player);
                } 
            }
            
            timer.Play();
        }
    }

    private void ThrowMagic(GameObject target)
    {
        var d = Mathf.Round((attack * (1 + (damage / 100))) * (1 - (Controlleur.instance.defense / 100)));
        GameObject newArrow = Instantiate(magic.gameObject, GetMagicPos(), transform.rotation);
        newArrow.GetComponent<Magic>().Init(target, d, magicCooldown);
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

    private Vector3 GetMagicPos()
    {
        var randX = Random.Range(-spawnRange, spawnRange);
        var randY = Random.Range(-spawnRange, spawnRange);
        
        var pos = Controlleur.instance.gameObject.transform.position;
        var modPos = new Vector3(pos.x + randX, pos.y + randY, pos.z);
        return modPos;
    }
}
