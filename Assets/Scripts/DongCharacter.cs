using System;

using UnityEngine;
using UnityEngine.InputSystem;

namespace RDong
{
	using static IA_GameInputs;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(CapsuleCollider2D))]
	public class DongCharacter : MonoBehaviour, IGameplayActions
	{
		public Action OnCharacterDied;

		[Header("Movement Settings")]

		[SerializeField] float _accMag = 3.0f;
		[SerializeField] float _maxSpeed = 2.0f;

		[Header("Health Settings")]

		[SerializeField] int _maxHealth = 2;

		Vector2 _moveInput = Vector2.zero;
		int _curHealth;

		Rigidbody2D _rb;

		void Awake()
		{
			// ������Ʈ �ʱ�ȭ
			_rb = GetComponent<Rigidbody2D>();
		}

		/// <summary>
		/// ���� ���۽� �÷��̾��� ���� ����
		/// </summary>
		public void Initialize() 
        { 
			_curHealth = _maxHealth;
        }

		void FixedUpdate()
		{
			Vector2 targetForce = _rb.mass * _accMag * _moveInput.normalized;

			_rb.AddForce(targetForce, ForceMode2D.Force);

			if (_rb.linearVelocity.sqrMagnitude < _maxSpeed * _maxSpeed)
			{
				return;
			}

			_rb.linearVelocity = _maxSpeed * _rb.linearVelocity.normalized;
		}

        private void OnTriggerEnter2D(Collider2D collision)
        {
			if (_curHealth > 0)
			{
				--_curHealth;
			} 
			else
			{
				OnCharacterDied?.Invoke();
				Debug.Log("ĳ���Ͱ� �׾����ϴ�.");
			}
        }

        void IGameplayActions.OnMove(InputAction.CallbackContext context)
		{
			Vector2 rawInput = context.ReadValue<Vector2>();

			switch (context.phase)
			{
			case InputActionPhase.Started:
			case InputActionPhase.Performed:
				_moveInput.x = rawInput.x;
				break;
			case InputActionPhase.Canceled:
				_moveInput.x = 0.0f;
				break;
			default:
				break;
			}
		}
	}
}

