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
		/// Ǯ���� ���� ���� ����Ǿ�� �ϴ� ����
		/// </summary>
		public void InitializeLifecycle() 
        { 
        }

		/// <summary>
		/// Ǯ�� ���� ���� ����Ǿ�� �ϴ� ����
		/// </summary>
		public void DeinitializeLifecycle()
		{
		}
	}
}
