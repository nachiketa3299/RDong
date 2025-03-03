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
			_rb.constraints = 
				RigidbodyConstraints2D.FreezePositionX | 
				RigidbodyConstraints2D.FreezeRotation;
		}

		void ShouldEndLifecycle()
		{
			DongGenerator.Instance.Pool.Release(this);
		}

		/// <summary>
		/// 풀에서 나온 직후 실행되어야 하는 로직
		/// </summary>
		public void InitializeLifecycle() 
        { 
        }

		/// <summary>
		/// 풀에 들어가기 직전 실행되어야 하는 로직
		/// </summary>
		public void DeinitializeLifecycle()
		{
		}
	}
}
