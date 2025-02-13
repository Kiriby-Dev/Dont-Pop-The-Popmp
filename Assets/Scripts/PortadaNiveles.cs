using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PortadaNiveles : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI grade;
    [SerializeField] Image image;
    [SerializeField] Sprite sprite;
    [SerializeField] private string nivel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int scoreInt = PlayerPrefs.GetInt("Score" + nivel);
        score.text = "Score: " + scoreInt.ToString();

        float timeFloat = PlayerPrefs.GetFloat("Time" + nivel);
        time.text = "Time: " + timeFloat.ToString("F2");

        string gradeString = PlayerPrefs.GetString("Grade" + nivel);
        grade.text = gradeString;

        int nivelInt = int.Parse(nivel);

        DesbloquearNiveles(nivelInt);
    }

    private void DesbloquearNiveles(int nivelInt)
    {
        if (nivelInt > 0)
        {
            int nivelAnterior = nivelInt - 1;
            string notaAnterior = PlayerPrefs.GetString("Grade" + nivelAnterior, "F");
            int unlock = PlayerPrefs.GetInt("Unlocked" + nivel);

            // Solo desbloquear si la nota del nivel anterior es "A" o "S"
            if (notaAnterior == "A" || notaAnterior == "S" || unlock == 1)
            {
                PlayerPrefs.SetInt("Unlocked" + nivel, 1);
                PlayerPrefs.Save();
            }
            else
            {
                image.sprite = sprite;  // Muestra el icono de bloqueo
                image.rectTransform.sizeDelta = new Vector2(200, 250);
            }
        }
        else
        {
            // El primer nivel siempre está desbloqueado
            PlayerPrefs.SetInt("Unlocked0", 1);
            PlayerPrefs.Save();
        }
    }
}


