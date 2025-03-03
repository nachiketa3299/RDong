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

        void OnCollisionEnter2D(Collision2D collision)
        {
			DongGenerator.Instance.Pool.Release(this);
        }

		/// <summary>
		/// Ǯ���� ���� ���� ����Ǿ�� �ϴ� ����
		/// </summary>
		public void InitializeLifecycle(Vector3 position) 
        { 
			_rb.MovePosition(position);
        }

		/// <summary>
		/// Ǯ�� ���� ���� ����Ǿ�� �ϴ� ����
		/// </summary>
		public void DeinitializeLifecycle()
		{
			_rb.linearVelocity = Vector3.zero;
		}
	}
}
