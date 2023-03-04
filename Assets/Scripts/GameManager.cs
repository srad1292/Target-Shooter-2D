using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] NumberDisplayManager hud;
    
    int gameScore = 0;
    int streak = 0;

    private void Start() {
        hud.ResetScoreDisplay();
    }

    public void HandleTargetShot(Target target) {
        AddToScore(target.GetPoints());

        streak = target.GetIsFriendly() ? 0 : streak + 1;
        print(streak);
        // TODO: If reached multiple of X, update multiplier
    }

    public void HandleTargetEscaped() {
        streak = 0;
    }


    private void AddToScore(int value) {
        gameScore += value;
        hud.UpdateScoreDisplay(gameScore);
    }

}
