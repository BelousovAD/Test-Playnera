using System.Collections.Generic;
using DG.Tweening;
using Makeup;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Animation
{
    internal class CreamAnimation : MakeupAnimation
    {
        private const MakeupType Type = MakeupType.Cream;
        
        [SerializeField] private RectTransform _hand;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _leftTarget;
        [SerializeField] private Transform _rightTarget;
        [SerializeField] private Image _selectedMakeup;
        [SerializeField] private Image _currentMakeup;
        [SerializeField] private Transform _color;

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

        private void Awake()
        {
            _toolParent = _color;
            _handDefaultPosition = _hand.position;
            _tool = _toolParent.GetChild(0) as RectTransform;
        }

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
                .Append(_hand.DOMove(_target.position, 1f))
                .Append(_hand.DOMove(_leftTarget.position, 1f))
                .Append(_hand.DOMove(_rightTarget.position, 1f))
                .Append(_hand.DOMove(_leftTarget.position, 1f))
                .Append(_hand.DOMove(_rightTarget.position, 1f))
                .AppendCallback(() => _makeup.Apply())
                .Append(_hand.DOMove(_toolParent.position, 1f))
                .AppendCallback(DropTool)
                .Append(_hand.DOMove(_handDefaultPosition, 1f))
                .AppendCallback(() => CanvasGroup.interactable = true)
                .Insert(2f, _currentMakeup.DOFade(0f, 3f));
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

            DOTween.Sequence()
                .AppendCallback(() => CanvasGroup.interactable = false)
                .Append(_hand.DOMove(_tool.position, 1f))
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