using App.Scripts.Mics;
using Mics;
using Unity.BossRoom.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Gameplay.UI
{
    public class GameUI : MiddlewareBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _panel;
        [Inject] IPublisher<ShareData.PlayGame> _publisherInputdata;
        private ShareData.PlayGame _playGamePassage = new ShareData.PlayGame();

        DisposableGroup _subscriptions;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }
        
        private void OnPlayButtonClicked()
        {
            _panel.enabled = false;
            _playButton.gameObject.SetActive(false);
            _publisherInputdata.Publish(_playGamePassage);
        }
    }
    
}