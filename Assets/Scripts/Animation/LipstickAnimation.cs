using System.Collections.Generic;
using DG.Tweening;
using Makeup;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Animation
{
    internal class LipstickAnimation : MakeupAnimation
    {
        private const MakeupType Type = MakeupType.Lipstick;
        
        [SerializeField] private RectTransform _hand;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _leftTarget;
        [SerializeField] private Transform _rightTarget;
        [SerializeField] private Image _selectedMakeup;
        [SerializeField] private Image _currentMakeup;
        [SerializeField] private List<Transform> _colors = new ();

        private Makeup.Makeup _makeup;
        private Transform _toolParent;
        private RectTransform _tool;
        private Vector3 _handDefaultPosition;
        private Vector3 _toolDefaultPosition;

        [Inject]
        private void Initialize(IEnumerable<Makeup.Makeup> makeups)
        {
            foreach (Makeup.Makeup makeup in makeups)
            {
                if (makeup.Type == Type)
                {
                    _makeup = makeup;
                    break;
                }
            }
        }

        private void Awake() =>
            _handDefaultPosition = _hand.position;

        private void OnEnable()
        {
            _makeup.Selected += Take;
            Take();
        }

        private void OnDisable() =>
            _makeup.Selected -= Take;

        public override void ApplyMakeup()
        {
            if (_makeup.Selection == Makeup.Makeup.MinIndex)
            {
                return;
            }

            DOTween.Sequence()
                .AppendCallback(() => CanvasGroup.interactable = false)
                .Append(_hand.DOMove(_leftTarget.position, 0.4f))
                .Append(_hand.DOMove(_rightTarget.position, 0.4f))
                .Append(_hand.DOMove(_leftTarget.position, 0.4f))
                .Append(_hand.DOMove(_rightTarget.position, 0.4f))
                .AppendCallback(() => _makeup.Apply())
                .Append(_hand.DOMove(_toolParent.position, 0.6f))
                .AppendCallback(DropTool)
                .Append(_hand.DOMove(_handDefaultPosition, 0.4f))
                .AppendCallback(() => CanvasGroup.interactable = true)
                .Insert(0.4f, _selectedMakeup.DOFade(1f, 1.2f))
                .Insert(0.4f, _currentMakeup.DOFade(0f, 1.2f));
        }

        private void DropTool()
        {
            _tool.SetParent(_toolParent);
            _tool.anchoredPosition = _toolDefaultPosition;
        }

        private void Take()
        {
            if (_makeup.Selection == Makeup.Makeup.MinIndex)
            {
                return;
            }

            _toolParent = _colors[_makeup.Selection];
            _tool = _toolParent.GetChild(0) as RectTransform;

            DOTween.Sequence()
                .AppendCallback(() => CanvasGroup.interactable = false)
                .Append(_hand.DOMove(_tool!.position, 1f))
                .AppendCallback(TakeTool)
                .Append(_hand.DOMove((_target.position - _toolParent.position) / 2, 1f).SetRelative())
                .AppendCallback(() => CanvasGroup.interactable = true);
        }

        private void TakeTool()
        {
            _tool.SetParent(_hand);
            _tool.SetAsFirstSibling();
        }
    }
}