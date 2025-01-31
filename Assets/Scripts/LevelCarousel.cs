using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelCarousel : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public Button[] levelButtons;
    public Button prevButton, nextButton; // Botones de navegación
    public float snapSpeed = 10f;
    private int totalLevels;
    private float[] positions;
    private int currentIndex = 0;
    private bool dragging = false;

    void Start()
    {
        totalLevels = levelButtons.Length;
        positions = new float[totalLevels];

        // Configurar posiciones centradas
        for (int i = 0; i < totalLevels; i++)
        {
            positions[i] = (float)i / (totalLevels - 1);
            int levelIndex = i + 1;
            levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
        }

        // Asegúrate de que los botones de navegación sean visibles inicialmente
        UpdateNavigationButtons();
    }

    void Update()
    {
        if (!dragging)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
                scrollRect.horizontalNormalizedPosition,
                positions[currentIndex],
                Time.deltaTime * snapSpeed
            );
        }

        // Efecto de escala en el botón centrado
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].transform.localScale = (i == currentIndex) ? Vector3.one * 1.2f : Vector3.one;
        }
    }

    public void OnDragStart() => dragging = true;

    public void OnDragEnd()
    {
        dragging = false;

        float minDistance = Mathf.Infinity;
        for (int i = 0; i < positions.Length; i++)
        {
            float distance = Mathf.Abs(scrollRect.horizontalNormalizedPosition - positions[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                currentIndex = i;
            }
        }

        // Actualiza los botones de navegación al final de la transición
        UpdateNavigationButtons();
    }

    void UpdateNavigationButtons()
    {
        prevButton.gameObject.SetActive(currentIndex > 0);  // Solo visible si no es el primer nivel
        nextButton.gameObject.SetActive(currentIndex < totalLevels - 1);  // Solo visible si no es el último nivel
    }

    void LoadLevel(int level)
    {
        Debug.Log("Cargando Nivel " + level);
        SceneManager.LoadScene("Nivel" + level);
    }

    // Funciones de avance y retroceso
    public void NextLevel()
    {
        if (currentIndex < totalLevels - 1)
        {
            currentIndex++;
            UpdateNavigationButtons();
        }
    }

    public void PreviousLevel()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateNavigationButtons();
        }
    }
}
