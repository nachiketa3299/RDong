using UnityEngine;
using UnityEngine.InputSystem;


namespace RDong
{
	using static IA_GameInputs;

	[DisallowMultipleComponent]
	[RequireComponent(typeof(CapsuleCollider2D))]
	public class DongCharacter : MonoBehaviour, IGameplayActions
	{
		[SerializeField] float _mass = 1.0f;
		[SerializeField] float _accMag = 3.0f;
		[SerializeField] float _linearDamping = 1.0f;
		[SerializeField] float _maxSpeed = 2.0f;

		Vector2 _moveInput = Vector2.zero;

		IA_GameInputs _gi;
		Rigidbody2D _rb;

		void Awake()
		{
			// 입력 초기화
			_gi = new IA_GameInputs();
			_gi.Gameplay.SetCallbacks(this);

			_gi.Enable();

			// 컴포넌트 초기화
			_rb = GetComponent<Rigidbody2D>();
			_rb.constraints =
				RigidbodyConstraints2D.FreezePositionY |
				RigidbodyConstraints2D.FreezeRotation;
			_rb.mass = _mass;
			_rb.linearDamping = _linearDamping;
		}

		void OnValidate()
		{
			if (_rb)
			{
                _rb.mass = _mass;
                _rb.linearDamping = _linearDamping;
			}
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

