using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelCarousel : MonoBehaviour
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] RectTransform content;
    [SerializeField] Button[] levelButtons;
    [SerializeField] Button prevButton, nextButton; // Botones de navegación
    [SerializeField] float snapSpeed = 10f;
    [SerializeField] SceneTransition transicion;
    [SerializeField] GameObject cartel;
    private int totalLevels;
    private float[] positions;
    private int currentIndex = 0;
    private bool dragging = false;

    // Arreglo de nombres de escenas para cada nivel
    public string[] levelScenes;

    void Start()
    {
        totalLevels = levelButtons.Length;
        positions = new float[totalLevels];

        // Configurar posiciones centradas y asociar las escenas a los botones
        for (int i = 0; i < totalLevels; i++)
        {
            positions[i] = (float)i / (totalLevels - 1);
            int levelIndex = i; // Guardamos el índice de nivel para usarlo en el callback

            // Asociamos la acción de carga de nivel a cada botón
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

    // Modificado: Cargar nivel a partir del índice de la escena
    void LoadLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("CurrentLevel", levelIndex);
        string nivel = levelIndex.ToString();
        int levelUnlock = PlayerPrefs.GetInt("Unlocked" + nivel, 0); // Valor por defecto 0 (bloqueado)

        if (levelScenes.Length > levelIndex && levelUnlock == 1)
        {
            transicion.StartGame(levelScenes[levelIndex]);
            PlayerPrefs.SetString("Nivel", nivel);
            PlayerPrefs.Save();

            GameObject prefMenu = GameObject.Find("UniquePrefab");
            if (prefMenu != null)
            {
                Destroy(prefMenu);
            }
        }
        else
        {
            cartel.SetActive(true);
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

    public void CerrarCartel() 
    {
        cartel.SetActive(false);
        prevButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
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
