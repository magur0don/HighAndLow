using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoice : MonoBehaviour
{
    public bool PlayerChoiced = false;

    public bool Higher = true;

    public void ChoiceInit()
    {
        PlayerChoiced = false;
        Higher = true;
    }

    public void HighChoice()
    {
        PlayerChoiced = true;
        Higher = true;
    }

    public void LowChoice()
    {
        PlayerChoiced = true;
        Higher = false;
    }
}
