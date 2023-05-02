using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySimpleController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int score = 50;
    public event Action<int, HealthBarController> onCollision;

    private void Start() {
        DamageManager.instance.SubscribeFunction(this);
        GetComponent<HealthBarController>().onDeath += OnDeath;
    }

    private void OnDeath(){
        GetComponent<AnimatorController>().SetDie();
        GuiManager.instance.UpdateText(score);
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(other.GetComponent<HealthBarController>()){
                onCollision?.Invoke(damage,other.GetComponent<HealthBarController>());
            }
        }
    }
}
