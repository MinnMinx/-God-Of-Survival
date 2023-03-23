using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWaveBehavior : MonoBehaviour
{
	private float _time = 0;
	[SerializeField]
	private float speed = 2f;
	[SerializeField]
	private float despawnCd = 3f;
	[SerializeField]
	private SpriteRenderer spr;
	[SerializeField]
	private AnimationCurve alphaCurve;
	[SerializeField]
	private AnimationCurve xCurve;
	private float angle = 0;
	private float damage = 0;

	public void SetAngle(float angle, float damage)
	{
		this.angle = angle;
		this.damage = damage;
	}

	private void Update()
	{
		_time += Time.deltaTime;
		Color color = spr.color;
		color.a = alphaCurve.Evaluate(_time / despawnCd);
		spr.color = color;
		transform.position += GetDirection(angle) * speed * Time.deltaTime;
		var scale = transform.localScale;
		scale.x = xCurve.Evaluate(_time / despawnCd);
		transform.localScale = scale;
		if (_time >= despawnCd)
		{
			Destroy(gameObject);
		} 
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Monster.Monster enemy = collision.GetComponent<Monster.Monster>();
		if (enemy != null)
		{
			enemy.takedamage(damage);
		}
	}

	Vector3 GetDirection(float angle)
	{
		return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
	}
}
