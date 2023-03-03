using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer scoreThousands;

    [SerializeField]
    SpriteRenderer scoreHundreds;

    [SerializeField]
    SpriteRenderer scoreTens;

    [SerializeField]
    SpriteRenderer scoreOnes;

    [SerializeField]
    bool useShadowed = false;
    
    NumberDisplayManager numberDisplayManager;



    private void Start() {
        numberDisplayManager = GetComponent<NumberDisplayManager>();
        Sprite zeroSprite = useShadowed ? numberDisplayManager.GetShadowedNumberSprite(0) : numberDisplayManager.GetNumberSprite(0);
        scoreThousands.sprite = zeroSprite;
        scoreHundreds.sprite = zeroSprite;
        scoreTens.sprite = zeroSprite;
        scoreOnes.sprite = zeroSprite;
    }
}
