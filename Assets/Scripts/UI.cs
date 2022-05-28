using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI instance;

    public TMP_Text moneyText;
    public TMP_Text warningText;

    float warningTime = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        warningText.text = " ";
    }
    private void Update()
    {
        moneyText.text = Game.money.ToString();

        if (warningTime <= 0)
            return;

        warningTime -= Time.deltaTime;

        if (warningTime <= 0)
        {
            warningText.text = " ";
        }
    }

    private void Warning(string message)
    {
        warningText.text = message;
        warningTime = 7;
    }

    public static void ShowWarning(string message)
    {
        instance.Warning(message);
    }
}
