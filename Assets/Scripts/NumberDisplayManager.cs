using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDisplayManager : MonoBehaviour
{
    [SerializeField]
    Sprite[] numbers;

    [SerializeField]
    Sprite[] shadowedNumbers;


    public Sprite GetNumberSprite(int number) {
        return numbers[number];
    }

    public Sprite GetShadowedNumberSprite(int number) {
        return shadowedNumbers[number];
    }

}
