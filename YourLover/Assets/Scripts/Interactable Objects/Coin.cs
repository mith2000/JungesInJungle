using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    public int coinValue = 2;
    [SerializeField]
    private GameObject coinVFX;
    [SerializeField]
    private float chaseRange = 10f;
    [SerializeField]
    private float speed = 5f;

    private float waitTime = 1f;
    private bool wait = false;
    private Transform targetPlayer;

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(DelayMoveToPlayer());
    }

    private void Update()
    {
        if (wait)
        {
            if (targetPlayer != null)
            {
                if (Vector2.Distance(transform.position, targetPlayer.position) < chaseRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
                }
            }
        }
    }

    IEnumerator DelayMoveToPlayer()
    {
        yield return new WaitForSeconds(waitTime);

        wait = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInfo>().ApplyCoin(coinValue);
            DestroyCoin();
        }
    }

    public void DestroyCoin()
    {
        Instantiate(coinVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
