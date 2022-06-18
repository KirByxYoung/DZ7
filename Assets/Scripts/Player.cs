using UnityEngine;

public class Player : MonoBehaviour 
{
    [SerializeField] private CoinsCounter _coinsCounter;

    private int _wallet = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy emeny))
        {
            Dead();
        }

        if (collision.TryGetComponent<Coin>(out Coin coin))
        {
            AddCoin();
            coin.StopFlip();
            Destroy(coin.gameObject);
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
        Debug.Log("Вы погибли");
    }

    private void AddCoin()
    {
        _wallet++;

        if (_wallet == _coinsCounter.Count)
        {
            Debug.Log("Вы победили!");
        }
    }
}