using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] GameObject dropper;

    [HideInInspector] public bool isOpened = false;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Drop");
    }

    public void OpenChest()
    {
        //Instantiate rewards
        int rand = Random.Range(2, 5);
        for (int i = 0; i < rand; i++)
        {
            float posXRand = Random.Range(-2f, 2f);
            float posYRand = Random.Range(-2f, 2f);
            dropper.GetComponent<DropMachine>().Drop(transform.position + new Vector3(posXRand, posYRand, 0));
        }
        isOpened = true;
        StartCoroutine(DestroyChest());
    }

    IEnumerator DestroyChest()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Open");
        }
    }
}
