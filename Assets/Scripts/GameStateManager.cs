using RDong;

using UnityEngine;
using UnityEngine.InputSystem;


namespace RDong
{
    using static IA_GameInputs;

    [DisallowMultipleComponent]
    public class GameStateManager : MonoBehaviour, IUserInterfaceActions
    {
        [Header("UI Settings")]

        [SerializeField] Canvas _UI_Start;
        [SerializeField] Canvas _UI_Gameplay;
        [SerializeField] Canvas _UI_Gameover;

        [Header("Gameplay Settings")]

        [SerializeField] DongCharacter _character;
        [SerializeField] DongGenerator _generator;

        IA_GameInputs _gi;

        IGameState _state;

        readonly TitleState Title = new();
        readonly GameplayState Gameplay = new();
        readonly GameoverState Gameover = new();

        interface IGameState
        {
            void EnterState(GameStateManager context);
        }

        class TitleState : IGameState
        {
            public void EnterState(GameStateManager context)
            {
                context._gi.UserInterface.Enable();
                context._gi.Gameplay.Disable();

                context._UI_Start.gameObject.SetActive(true);
                context._UI_Gameplay.gameObject.SetActive(false);
                context._UI_Gameover.gameObject.SetActive(false);

                context._character.gameObject.SetActive(false);
                //context._generator.gameObject.SetActive(false);
            }
        }

        class GameplayState : IGameState
        {
            public void EnterState(GameStateManager context)
            {
                context._gi.UserInterface.Disable();
                context._gi.Gameplay.Enable();

                context._character.Initialize();

                context._UI_Start.gameObject.SetActive(false);
                context._UI_Gameplay.gameObject.SetActive(true);
                context._UI_Gameover.gameObject.SetActive(false);

                context._character.gameObject.SetActive(true);

                context._generator.GenerationStart();

                ScoreManager.Instance.StartCountScore();
            }
        }

        class GameoverState : IGameState
        {
            public void EnterState(GameStateManager context)
            {
                context._gi.UserInterface.Enable();
                context._gi.Gameplay.Disable();

                context._UI_Start.gameObject.SetActive(false);
                context._UI_Gameplay.gameObject.SetActive(false);
                context._UI_Gameover.gameObject.SetActive(true);

                context._character.gameObject.SetActive(false);

                context._generator.GenerationEnd();

                ScoreManager.Instance.FinishCountScore();
            }
        }

        public static GameStateManager Instance => _instance;

        static GameStateManager _instance;


        void Awake()
        {
            if (Instance == null)
            {
                _instance = this;
            }

			// 입력 초기화
			_gi = new IA_GameInputs();

            _gi.Gameplay.SetCallbacks(_character);
            _gi.UserInterface.SetCallbacks(this);

            _character.OnCharacterDied += GameStateManager_OnCharacterDied;
        }

        void Start()
        {
            ChangeState(Title);
        }

        void IUserInterfaceActions.OnAnyStart(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    ChangeState(Gameplay);
                    break;
                default:
                    break;
            }
        }

        void GameStateManager_OnCharacterDied()
        {
            ChangeState(Gameover);
        }

        void ChangeState(IGameState newState)
        {
            _state = newState;
            _state.EnterState(this);
        }
    }
}
