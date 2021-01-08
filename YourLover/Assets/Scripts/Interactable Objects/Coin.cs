using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public int coinValue = 2;
    [SerializeField] GameObject coinVFX;
    [SerializeField] float chaseRange = 10f;
    [SerializeField] float speed = 5f;

    float waitTime = 1f;
    bool wait = false;
    Transform targetPlayer;

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
            AudioManager.GetInstance().Play("Coin");

            collision.GetComponent<PlayerInfo>().ApplyCoin(coinValue);
            GameObject dmgInfo = Instantiate(PrefabContainer.GetInstance().damageInfoPrefab, transform.position, Quaternion.identity);
            dmgInfo.GetComponent<DamageInfo>().damage = "+" + coinValue;
            dmgInfo.GetComponent<DamageInfo>().text.color = new Color(255, 255, 0);
            DestroyCoin();
        }
    }

    public void DestroyCoin()
    {
        Instantiate(coinVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
