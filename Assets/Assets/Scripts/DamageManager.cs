using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance {get; private set;}

    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }

        instance = this;
    }

    /*public void SubscribeToEvent(Action <int, HealthBarController> currentAction){
        currentAction += DamageCalculation;
    }*/

    public void SubscribeFunction(BulletController enemy){
        enemy.onCollision += DamageCalculation;
    }

    public void SubscribeFunction(EnemySimpleController enemy){
        enemy.onCollision += DamageCalculation;
    }

    private void DamageCalculation(int damageTaken, HealthBarController healthBarController){
        healthBarController.UpdateHealth(-damageTaken);
    }
}
