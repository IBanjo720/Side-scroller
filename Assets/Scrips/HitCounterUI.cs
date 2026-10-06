using UnityEngine;
using TMPro;

public class HitCounterUI : MonoBehaviour
{
    public TextMeshProUGUI hitText;

    void Start()
    {
        UpdateHits(0, 3);
    }

    public void UpdateHits(int currentHits, int maxHits)
    {
        hitText.text = "Hits: " + currentHits + " / " + maxHits;
    }
}
