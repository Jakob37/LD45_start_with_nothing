using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private AudioController audio_controller;
    private ExclamationMark exclamation_mark;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    void Awake() {
        audio_controller = FindObjectOfType<AudioController>();
        exclamation_mark = GetComponentInChildren<ExclamationMark>();
    }

    void Start() {
        
    }

    void Update() {
        print(timeBtwAttack);
        if (timeBtwAttack <= 0) {
            if (Input.GetKey(KeyCode.X)) {
                audio_controller.MakeShout();
                exclamation_mark.Activate();

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    if (enemiesToDamage[i].GetComponent<ThievingBehaviour>() != null) {
                        enemiesToDamage[i].GetComponent<ThievingBehaviour>().Scare(damage);
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
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
