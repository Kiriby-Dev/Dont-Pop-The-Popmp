using UnityEngine;

public class Sonido : MonoBehaviour
{
    public static Sonido Instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
}
