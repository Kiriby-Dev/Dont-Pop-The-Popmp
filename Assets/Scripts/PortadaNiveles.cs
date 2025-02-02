using TMPro;
using UnityEngine;

public class PortadaNiveles : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] private string nivel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int scoreInt = PlayerPrefs.GetInt("Score" + nivel);
        score.text = "Score: " + scoreInt.ToString();
    }
}
