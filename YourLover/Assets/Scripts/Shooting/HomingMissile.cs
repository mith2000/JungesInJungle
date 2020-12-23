using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{
	[HideInInspector] public Transform target;

	[SerializeField] protected int damage = 5;
	public float speed = 5f;
	[SerializeField] protected float lifeTime = 10;
	[SerializeField] public float rotateSpeed = 200f;
	[SerializeField] GameObject explosionVFX;

	protected Rigidbody2D rb;

	// Use this for initialization
	public virtual void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(SelfDestroyProj());
	}

	public IEnumerator SelfDestroyProj()
	{
		yield return new WaitForSeconds(lifeTime);

		Explode();
	}

	public void Explode()
	{
		Instantiate(explosionVFX, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public virtual void FixedUpdate()
	{
		if (target != null)
		{
			Vector2 direction = (Vector2)target.position - rb.position;

			direction.Normalize();

			float rotateAmount = Vector3.Cross(direction, transform.up).z;

			rb.angularVelocity = -rotateAmount * rotateSpeed;

			rb.velocity = transform.up * speed;
		}
		else
        {
			Explode();
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			collision.GetComponent<Enemy>().TakeDamage(damage);
			Explode();
		}
		if (collision.CompareTag("GoldMine"))
		{
			collision.GetComponent<GoldMine>().TakeDamage(damage);
			Explode();
		}
		if (collision.CompareTag("Block"))
		{
			Explode();
		}
	}
}
