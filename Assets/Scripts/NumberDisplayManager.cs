using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplayManager : MonoBehaviour
{
    [SerializeField] Sprite[] numbers;
    [SerializeField] Sprite[] shadowedNumbers;

    [SerializeField] SpriteRenderer scoreThousands;
    [SerializeField] SpriteRenderer scoreHundreds;
    [SerializeField] SpriteRenderer scoreTens;
    [SerializeField] SpriteRenderer scoreOnes;
    [SerializeField] bool useShadowed = false;

    [SerializeField] SpriteRenderer ammoOnes;

    public void UpdateAmmoDisplay(int ammo) {
        ammoOnes.sprite = GetNumberSprite(ammo);
    }

    public void ResetScoreDisplay() {
        UpdateScoreDisplay(0);
    }

    public void UpdateScoreDisplay(int score) {
        if(score > 9999) { score = 9999; }

        List<int> digits = ScoreToDigits(score);
        scoreThousands.sprite = GetNumberSprite(digits[0]);
        scoreHundreds.sprite = GetNumberSprite(digits[1]);
        scoreTens.sprite = GetNumberSprite(digits[2]);
        scoreOnes.sprite = GetNumberSprite(digits[3]);
    }

    private List<int> ScoreToDigits(int score) {
        List<int> digits = new List<int>();

        // Get right most digit, then remove it from number
        while(score > 0) {
            digits.Add(score % 10);
            score = score / 10;
        }

        // Get digits array in correct order
        digits.Reverse();

        // Left pad With 0s to get the 4 digits for the display 
        while(digits.Count < 4) {
            digits.Insert(0, 0);
        }

        return digits;
    }

    private Sprite GetNumberSprite(int number) {
        return useShadowed ? shadowedNumbers[number] : numbers[number];
    }

}
