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
		/// 풀에서 나온 직후 실행되어야 하는 로직
		/// </summary>
		public void InitializeLifecycle(Vector3 position) 
        { 
			_rb.MovePosition(position);
        }

		/// <summary>
		/// 풀에 들어가기 직전 실행되어야 하는 로직
		/// </summary>
		public void DeinitializeLifecycle()
		{
			_rb.linearVelocity = Vector3.zero;
		}
	}
}
