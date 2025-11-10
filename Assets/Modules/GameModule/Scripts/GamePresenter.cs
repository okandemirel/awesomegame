using System.Collections;
using System.Collections.Generic;
using Modules.AudioModule.Scripts;
using Modules.AudioModule.Scripts.Core.Data.ValueObjects;
using Modules.CardModule.Scripts.Core.Data;
using Modules.CardModule.Scripts.Core.Data.UnityObjects;
using Modules.CardModule.Scripts.Presentation;
using Modules.GameModule.Scripts.Core.Data;
using Modules.GridModule.Scripts.Core.Data.UnityObjects;
using Modules.GridModule.Scripts.Core.Interfaces;
using Modules.HapticsModule.Scripts;
using Modules.InputModule.Scripts;
using Modules.SaveModule.Scripts.Core.Data.ValueObjects;
using Modules.SaveModule.Scripts.Core.Interfaces;
using Modules.ScoringModule.Scripts;
using UnityEngine;

namespace Modules.GameModule.Scripts
{
    public class GamePresenter
    {
        private readonly GameStateModel _state;
        private readonly IGameView _view;
        private readonly IGridLayoutGenerator _gridLayoutGenerator;
        private readonly IAudioService _audioService;
        private readonly IHapticsEngine _hapticsEngine;
        private readonly IScoreCalculator _scoreCalculator;
        private readonly IGameSaveManager _gameSaveManager;
        private readonly IInputHandler _inputHandler;
        private readonly MonoBehaviour _coroutineRunner;

        private List<CardPresenter> _cardPresenters = new List<CardPresenter>();
        private GridConfigurationData _gridConfig;

        public GamePresenter(
            GameStateModel state,
            IGameView view,
            IGridLayoutGenerator gridLayoutGenerator,
            IAudioService audioService,
            IHapticsEngine hapticsEngine,
            IScoreCalculator scoreCalculator,
            IGameSaveManager gameSaveManager,
            IInputHandler inputHandler,
            MonoBehaviour coroutineRunner)
        {
            _state = state;
            _view = view;
            _gridLayoutGenerator = gridLayoutGenerator;
            _audioService = audioService;
            _hapticsEngine = hapticsEngine;
            _scoreCalculator = scoreCalculator;
            _gameSaveManager = gameSaveManager;
            _inputHandler = inputHandler;
            _coroutineRunner = coroutineRunner;

            _inputHandler.OnTwoCardsSelected += OnTwoCardsSelected;
        }

        public void StartGame(GridConfigurationData gridConfig, CardConfig cardConfig)
        {
            _gridConfig = gridConfig;
            _state.CurrentState = GameState.Idle;
            _state.Score = 0;
            _state.Moves = 0;
            _state.MatchesFound = 0;
            _state.TotalPairs = gridConfig.UniquePairs;

            CleanupCards();
            CreateCardGrid(gridConfig, cardConfig);
            _state.CurrentState = GameState.Playing;
        }

        private void CleanupCards()
        {
            foreach (var presenter in _cardPresenters)
            {
                if (presenter?.View != null)
                {
                    presenter.View.OnCardClicked -= OnCardClick;
                }
            }
            _cardPresenters.Clear();

            var pool = _view.GetCardPool();
            if (pool != null)
            {
                pool.ReturnAll();
            }

            _inputHandler.Initialize();
        }

        private void CreateCardGrid(GridConfigurationData gridConfig, CardConfig cardConfig)
        {
            var layout = _gridLayoutGenerator.GenerateLayout(gridConfig, _view.GetContainerRect(), cardConfig);
            var pool = _view.GetCardPool();

            _view.ConfigureGridLayout(gridConfig.settings.columns, layout.CardSize, gridConfig.settings.spacing);

            pool.Initialize(layout.CardTypes.Count);

            for (int i = 0; i < layout.CardTypes.Count; i++)
            {
                var cardData = cardConfig.GetCardByType(layout.CardTypes[i]);
                var cardView = pool.GetCard();

                if (cardView == null) break;

                cardView.Initialize(i, cardData.frontSprite, cardConfig.GetDefaultBackSprite(), cardData.cardColor);

                var model = new CardModel(i, layout.CardTypes[i]);
                var presenter = new CardPresenter(model, cardView, _audioService, _hapticsEngine);

                cardView.OnCardClicked += OnCardClick;
                _cardPresenters.Add(presenter);
            }
        }

        private void OnCardClick(int cardId)
        {
            var presenter = _cardPresenters[cardId];
            if (!presenter.CanFlip())
            {
                UnityEngine.Debug.Log($"Card {cardId} cannot flip (matched)");
                return;
            }

            var result = _inputHandler.RegisterCardClick(cardId);
            UnityEngine.Debug.Log($"Card {cardId} click result: {result}");

            if (result == true)
            {
                UnityEngine.Debug.Log($"Flipping card {cardId} to FRONT");
                presenter.Flip(true);
            }
            else if (result == false)
            {
                UnityEngine.Debug.Log($"Flipping card {cardId} to BACK");
                presenter.Flip(false);
            }
        }

        private void OnTwoCardsSelected(int card1Id, int card2Id)
        {
            _state.Moves++;
            _view.ShowMoves(_state.Moves);
            _coroutineRunner.StartCoroutine(CheckMatch(card1Id, card2Id));
        }

        private IEnumerator CheckMatch(int card1Id, int card2Id)
        {
            _state.CurrentState = GameState.Comparing;

            yield return new WaitForSeconds(0.5f);

            var card1 = _cardPresenters[card1Id];
            var card2 = _cardPresenters[card2Id];

            bool isMatch = card1.Model.CardType == card2.Model.CardType && card1.Model.Id != card2.Model.Id;

            if (isMatch)
            {
                card1.SetMatched();
                card2.SetMatched();

                _scoreCalculator.AddCombo();
                _state.MatchesFound++;

                int score = _scoreCalculator.CalculateScore(new ScoringContext
                {
                    IsMatch = true,
                    TimeTaken = 1f,
                    CurrentCombo = _scoreCalculator.CurrentCombo
                });

                _state.Score += score;
                _view.ShowScore(_state.Score, _scoreCalculator.CurrentCombo);

                if (_state.IsGameComplete)
                {
                    yield return new WaitForSeconds(0.5f);
                    EndGame();
                    yield break;
                }
            }
            else
            {
                card1.SetMismatch();
                card2.SetMismatch();
                _scoreCalculator.ResetCombo();

                yield return new WaitForSeconds(0.5f);

                card1.Flip(false);
                card2.Flip(false);
                _view.ShowScore(_state.Score, 0);
            }

            _inputHandler.Clear();
            _state.CurrentState = GameState.Playing;
        }

        private void EndGame()
        {
            _state.CurrentState = GameState.GameOver;
            _audioService.PlaySound(SoundType.GameOver);
            _hapticsEngine.TriggerHeavy();
            _view.ShowGameComplete(_state.Score);
        }

        public void SaveGame()
        {
            var data = new SaveData
            {
                score = _state.Score,
                moves = _state.Moves,
                matchesFound = _state.MatchesFound,
                elapsedTime = _state.ElapsedTime,
                gridConfigName = _gridConfig.name,
                cards = new List<CardSaveData>()
            };

            foreach (var p in _cardPresenters)
            {
                data.cards.Add(new CardSaveData
                {
                    id = p.Model.Id,
                    cardTypeId = (int)p.Model.CardType,
                    isFlipped = p.Model.IsFlipped,
                    isMatched = p.Model.IsMatched
                });
            }

            _gameSaveManager.Save(data);
        }

        public void Cleanup()
        {
            _inputHandler.OnTwoCardsSelected -= OnTwoCardsSelected;

            foreach (var p in _cardPresenters)
            {
                p.View.OnCardClicked -= OnCardClick;
            }
            _cardPresenters.Clear();
        }
    }
}