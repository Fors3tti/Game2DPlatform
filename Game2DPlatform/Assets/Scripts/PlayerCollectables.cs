using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectables : MonoBehaviour
{
    public Text textComponent;
    public int gemNumber;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        textComponent.text = gemNumber.ToString();
    }

    public void GemCollected()
    {
        gemNumber += 1;
        UpdateText();
    }
}
