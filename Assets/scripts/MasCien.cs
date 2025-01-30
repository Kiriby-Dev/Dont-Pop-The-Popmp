using System.Threading.Tasks;
using UnityEngine;

public class MasCien : MonoBehaviour
{
    [SerializeField] int Permanencia = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        await Task.Delay(Permanencia);
        Destroy(gameObject);
    }
}
