using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    public int Count { get; private set; }

    private void Start()
    {
        Count = FindObjectsOfType<Coin>().Length;
    }
}