using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    internal class CustomGridLayoutGroup : LayoutGroup
    {
        private const DrivenTransformProperties DrivenProperties = DrivenTransformProperties.Anchors |
                                                             DrivenTransformProperties.AnchoredPosition |
                                                             DrivenTransformProperties.SizeDelta;

        [SerializeField] private CornerType _startCorner = CornerType.UpperLeft;
        [SerializeField] private AxisType _startAxis = AxisType.Horizontal;
        [SerializeField] private Vector2 _spacing = Vector2.zero;
        [SerializeField] private ConstraintType _constraint = ConstraintType.FixedColumnCount;
        [SerializeField] private int _constraintCount = 2;

        private Vector2 _cellSize;

        public enum CornerType
        {
            UpperLeft = 0,
            UpperRight = 1,
            LowerLeft = 2,
            LowerRight = 3,
        }

        public enum AxisType
        {
            Horizontal = 0,
            Vertical = 1,
        }

        public enum ConstraintType
        {
            FixedColumnCount = 1,
            FixedRowCount = 2,
        }

        public CornerType StartCorner
        {
            get => _startCorner;
            set => SetProperty(ref _startCorner, value);
        }

        public AxisType StartAxis
        {
            get => _startAxis;
            set => SetProperty(ref _startAxis, value);
        }

        public Vector2 CellSize
        {
            get => _cellSize;
            set => SetProperty(ref _cellSize, value);
        }

        public Vector2 Spacing
        {
            get => _spacing;
            set => SetProperty(ref _spacing, value);
        }

        public ConstraintType Constraint
        {
            get => _constraint;
            set => SetProperty(ref _constraint, value);
        }

        public int ConstraintCount
        {
            get => _constraintCount;
            set => SetProperty(ref _constraintCount, Mathf.Max(1, value));
        }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            SetLayoutInputForAxis(0, rectTransform.rect.size.x, -1, (int)AxisType.Horizontal);
        }

        public override void CalculateLayoutInputVertical() =>
            SetLayoutInputForAxis(0, rectTransform.rect.size.y, -1, (int)AxisType.Vertical);

        public override void SetLayoutHorizontal() =>
            SetCellsAlongAxis(AxisType.Horizontal);

        public override void SetLayoutVertical() =>
            SetCellsAlongAxis(AxisType.Vertical);

        private void SetCellsAlongAxis(AxisType axis)
        {
            int childrenCount = rectChildren.Count;
            float width = rectTransform.rect.size.x;
            float height = rectTransform.rect.size.y;

            Vector2Int cellCount = Constraint switch
            {
                ConstraintType.FixedColumnCount =>
                    new Vector2Int(
                        Mathf.Clamp(childrenCount, 1, ConstraintCount),
                        childrenCount / ConstraintCount + (childrenCount % ConstraintCount > 0 ? 1 : 0)),
                ConstraintType.FixedRowCount =>
                    new Vector2Int(
                        childrenCount / ConstraintCount + (childrenCount % ConstraintCount > 0 ? 1 : 0),
                        Mathf.Clamp(childrenCount, 1, ConstraintCount)),
                _ => throw new ArgumentOutOfRangeException(),
            };

            CellSize = new Vector2
            {
                x = (width - padding.horizontal - Spacing.x * (cellCount.x - 1)) / cellCount.x,
                y = (height - padding.vertical - Spacing.y * (cellCount.y - 1)) / cellCount.y,
            };

            if (axis == AxisType.Horizontal)
            {
                for (int i = 0; i < childrenCount; i++)
                {
                    RectTransform rect = rectChildren[i];

                    m_Tracker.Add(this, rect, DrivenProperties);

                    rect.anchorMin = Vector2.up;
                    rect.anchorMax = Vector2.up;
                    rect.sizeDelta = CellSize;
                }

                return;
            }

            Vector2 requiredSpace = new (
                cellCount.x * (CellSize.x + Spacing.x) - Spacing.x,
                cellCount.y * (CellSize.y + Spacing.y) - Spacing.y);
            Vector2 startOffset = new (
                GetStartOffset((int)AxisType.Horizontal, requiredSpace.x),
                GetStartOffset((int)AxisType.Vertical, requiredSpace.y));

            int cellsPerMainAxis = StartAxis == AxisType.Horizontal ? cellCount.x : cellCount.y;
            int childrenToMove = 0;

            if (childrenCount > ConstraintCount &&
                Mathf.CeilToInt((float)childrenCount / cellsPerMainAxis) < ConstraintCount)
            {
                childrenToMove = ConstraintCount - Mathf.CeilToInt((float)childrenCount / cellsPerMainAxis);
                childrenToMove += Mathf.FloorToInt((float)childrenToMove / (cellsPerMainAxis - 1));

                if (childrenCount % cellsPerMainAxis == 1)
                {
                    childrenToMove += 1;
                }
            }

            for (int i = 0; i < childrenCount; i++)
            {
                int positionX;
                int positionY;

                if (StartAxis == AxisType.Horizontal)
                {
                    if (_constraint == ConstraintType.FixedRowCount && childrenCount - i <= childrenToMove)
                    {
                        positionX = 0;
                        positionY = _constraintCount - (childrenCount - i);
                    }
                    else
                    {
                        positionX = i % cellsPerMainAxis;
                        positionY = i / cellsPerMainAxis;
                    }
                }
                else
                {
                    if (_constraint == ConstraintType.FixedColumnCount && childrenCount - i <= childrenToMove)
                    {
                        positionX = _constraintCount - (childrenCount - i);
                        positionY = 0;
                    }
                    else
                    {
                        positionX = i / cellsPerMainAxis;
                        positionY = i % cellsPerMainAxis;
                    }
                }

                if (StartCorner is CornerType.UpperRight or CornerType.LowerRight)
                {
                    positionX = cellCount.x - 1 - positionX;
                }

                if (StartCorner is CornerType.LowerLeft or CornerType.LowerRight)
                {
                    positionY = cellCount.y - 1 - positionY;
                }

                SetChildAlongAxis(
                    rectChildren[i],
                    (int)AxisType.Horizontal,
                    startOffset.x + (CellSize.x + Spacing.x) * positionX,
                    CellSize.x);
                SetChildAlongAxis(
                    rectChildren[i],
                    (int)AxisType.Vertical,
                    startOffset.y + (CellSize.y + Spacing.y) * positionY,
                    CellSize.y);
            }
        }
    }
}