using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class JumpAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _jumpAudio;

        private JumpComponent _jumpComponent;

        private Sequence _sequence;

        [Inject]
        private void Construct(JumpComponent jumpComponent)
        {
            _jumpComponent = jumpComponent;
        }

        private void OnEnable() => _jumpComponent.Jumped += UpdateJumpView;

        private void OnDisable() => _jumpComponent.Jumped -= UpdateJumpView;

        private void UpdateJumpView() => _jumpAudio.Play();
    }
}