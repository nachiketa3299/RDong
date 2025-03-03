using UnityEngine;

namespace RDong
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	public class Dong : MonoBehaviour
	{
		Rigidbody2D _rb;


        void Awake()
		{
			_rb = GetComponent<Rigidbody2D>();
		}

        private void OnTriggerEnter2D(Collider2D other)
        {
			Debug.Log($"{name}�� �浹: {other.name}");

			DongGenerator.Instance.Pool.Release(this);
        }

		public void Initialize(Vector3 pos)
		{
			transform.position = pos;
            _rb.WakeUp();
		}

		public void Deinitialize()
		{
			_rb.linearVelocity = Vector2.zero;
			_rb.position = Vector2.zero;
			_rb.rotation = 0;

			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
			transform.localScale = Vector3.one;
		}
	}
}
