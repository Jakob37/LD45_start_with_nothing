using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    void Start() {
        
    }

    void Update() {
        if (timeBtwAttack <= 0) {
            if(Input.GetKey(KeyCode.E)) {
              print("punch");
              Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

              for (int i = 0; i < enemiesToDamage.Length; i++) {
                  if (enemiesToDamage[i].GetComponent<ThievingBehaviour>() != null) {
                      enemiesToDamage[i].GetComponent<ThievingBehaviour>().Scare(damage);
                  }
                  // else if (enemiesToDamage[i].GetComponent<BigTomatoGuy>() != null) {
                  //     enemiesToDamage[i].GetComponent<BigTomatoGuy>().Scare(damage);
                  // }
              }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
