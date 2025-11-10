using Modules.AudioModule.Scripts;
using Modules.AudioModule.Scripts.Core.Data.UnityObjects;
using Modules.CardModule.Scripts.Core.Data.UnityObjects;
using Modules.GameModule.Scripts.Core.Data;
using Modules.GridModule.Scripts;
using Modules.GridModule.Scripts.Core.Data.UnityObjects;
using Modules.HapticsModule.Scripts;
using Modules.HapticsModule.Scripts.Core.Data.UnityObjects;
using Modules.InputModule.Scripts;
using Modules.SaveModule.Scripts;
using Modules.SaveModule.Scripts.Core.Data.UnityObjects;
using Modules.ScoringModule.Scripts;
using Modules.ScoringModule.Scripts.Core.Data.UnityObjects;
using UnityEngine;

namespace Modules.GameModule.Scripts
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Configurations")]
        [SerializeField] private GridConfigurationData gridConfig;
        [SerializeField] private CardConfig cardConfig;
        [SerializeField] private AudioConfig audioConfig;
        [SerializeField] private HapticsConfig hapticsConfig;
        [SerializeField] private ScoringConfig scoringConfig;
        [SerializeField] private SaveConfig saveConfig;

        [Header("View")]
        [SerializeField] private GameView gameView;

        private GamePresenter _gamePresenter;

        private void Awake()
        {
            var gridLayoutGenerator = new GridLayoutGenerator();

            var audioParent = new GameObject("AudioPool").transform;
            audioParent.SetParent(transform);
            var audioService = new AudioService(audioConfig, audioParent);
            audioService.Initialize();

            var hapticsEngine = new HapticsEngine(hapticsConfig);

            var scoreCalculator = new ScoreCalculator(scoringConfig);
            scoreCalculator.Initialize();

            var gameSaveManager = new GameSaveManager(saveConfig);

            var inputHandler = new CardInputHandler();
            inputHandler.Initialize();

            var gameState = new GameStateModel();

            _gamePresenter = new GamePresenter(
                gameState,
                gameView,
                gridLayoutGenerator,
                audioService,
                hapticsEngine,
                scoreCalculator,
                gameSaveManager,
                inputHandler,
                this
            );

            _gamePresenter.StartGame(gridConfig, cardConfig);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause && _gamePresenter != null)
            {
                _gamePresenter.SaveGame();
            }
        }

        private void OnApplicationQuit()
        {
            if (_gamePresenter != null)
            {
                _gamePresenter.SaveGame();
            }
        }

        private void OnDestroy()
        {
            _gamePresenter?.Cleanup();
        }
    }
}