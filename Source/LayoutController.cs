﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QFSW.QGUI
{
    public class LayoutController
    {
        public static float HorizontalPadding => 4;
        public static float RowPadding => EditorGUIUtility.standardVerticalSpacing;
        public static float RowHeight => EditorGUIUtility.singleLineHeight;

        public bool IsValid
        {
            get
            {
                if (_currentRect.width < 0) { return false; }
                if (_currentRect.height < 0) { return false; }
                if (_currentRect.x < TotalDrawRect.x) { return false; }
                if (_currentRect.y < TotalDrawRect.y) { return false; }
                if (_currentRect.x + _currentRect.width > TotalDrawRect.x + TotalDrawRect.width) { return false; }
                if (_currentRect.y + _currentRect.height > TotalDrawRect.y + TotalDrawRect.height) { return false; }

                return true;
            }
        }

        public Rect TotalDrawRect { get; }
        public Rect CurrentRect => _currentRect;

        private Rect _currentRect;
        private readonly Stack<Rect> _undoStack = new Stack<Rect>();

        public LayoutController(Rect drawRect)
        {
            TotalDrawRect = drawRect;
            _currentRect = drawRect;
            _currentRect.height = RowHeight;
        }

        private void PushToUndoStack()
        {
            _undoStack.Push(_currentRect);
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                _currentRect = _undoStack.Pop();
            }
        }

        public Rect BeginNewLine()
        {
            PushToUndoStack();
            _currentRect.y += RowPadding + RowHeight;
            _currentRect.x = TotalDrawRect.x;
            _currentRect.width = TotalDrawRect.width;
            return _currentRect;
        }

        public Rect ReserveHorizontal(float width)
        {
            Rect drawRect = _currentRect;
            drawRect.width = width;

            PushToUndoStack();
            width += HorizontalPadding;
            _currentRect.x += width;
            _currentRect.width -= width;

            return drawRect;
        }

        public Rect ReserveHorizontalPercentage(float widthPercentage)
        {
            float width = _currentRect.width * widthPercentage;
            return ReserveHorizontal(width);
        }

        public Rect ReserveHorizontalReversed(float width)
        {
            Rect drawRect = _currentRect;
            drawRect.x += drawRect.width;
            drawRect.x -= width;
            drawRect.width = width;

            PushToUndoStack();
            width += HorizontalPadding;
            _currentRect.width -= width;

            return drawRect;
        }

        public Rect ReserveHorizontalReversedPercentage(float widthPercentage)
        {
            float width = _currentRect.width * widthPercentage;
            return ReserveHorizontalReversed(width);
        }

        public Rect ResizeRectHeight(Rect rect, float height)
        {
            rect.y += (rect.height - height) / 2;
            rect.height = height;

            return rect;
        }

        public Rect ReserveHorizontal(float width, float height)
        {
            return ResizeRectHeight(ReserveHorizontal(width), height);
        }

        public Rect ReserveHorizontalReversed(float width, float height)
        {
            return ResizeRectHeight(ReserveHorizontalReversed(width), height);
        }

        public Rect ReserveSquare()
        {
            return ReserveHorizontal(RowHeight);
        }

        public Rect ReserveSquareReversed()
        {
            return ReserveHorizontalReversed(RowHeight);
        }

        public Rect ReserveAuto(GUIContent content, GUIStyle style)
        {
            Vector2 size = style.CalcSize(content);
            return ReserveHorizontal(size.x, size.y);
        }

        public Rect ReserveAutoReversed(GUIContent content, GUIStyle style)
        {
            Vector2 size = style.CalcSize(content);
            return ReserveHorizontalReversed(size.x, size.y);
        }

        public void SpliceRow(int colCount, ref Rect[] rects)
        {
            float width = _currentRect.width / colCount;
            for (int i = 0; i < rects.Length; i++)
            {
                rects[i] = ReserveHorizontal(width);
            }
        }

        public Rect[] SpliceRow(int colCount)
        {
            Rect[] rects = new Rect[colCount];
            SpliceRow(colCount, ref rects);
            return rects;
        }
    }
}
