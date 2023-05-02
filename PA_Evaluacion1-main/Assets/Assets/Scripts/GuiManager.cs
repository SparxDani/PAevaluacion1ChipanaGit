using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuiManager : MonoBehaviour
{
    public static GuiManager instance {get; private set;}
    [SerializeField] private TMP_Text scoreText;
    private int scoreTotal = 0;

    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public void UpdateText(int pointsGained){
        scoreTotal += pointsGained;
        scoreText.text = string.Format("Score: {0} (+ {1})", scoreTotal, pointsGained);
    }
}
