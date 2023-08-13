using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TextSizeFilter
{
    [ExecuteAlways]
    public class TextSizeFilter : MonoBehaviour
    {
        private const float FONT_SIZE_OFFSET = .2f;
        [SerializeField] private FitMode _horizontalFit;
        [SerializeField] private FitMode _verticalFit;

        
        private TMP_Text _tmproText = default;
        private Text _uiText = default;
        private RectTransform _currentRect = default;

        private event Action OnChanged;

        private int _lastKnownTextHash = default;

        private void OnValidate()
        {
            BakeData();
        }
        
        private void Start()
        {
            BakeData();
        }

        private void BakeData()
        {
            if (TryGetComponent(out TMP_Text tmProUIText))
            {
                _tmproText = tmProUIText;
            }
            else if (TryGetComponent(out Text uiText))
            {
                _uiText = uiText;
            }
            else
            {
                throw new Exception($"Object {name} doesn't have text component. Script required {nameof(TMP_Text)}, {nameof(TextMeshProUGUI)} or" +
                    $" {nameof(Text)}");
            }

            _currentRect = _tmproText != null ? _tmproText.rectTransform : _uiText != null ? _uiText.rectTransform : null;
        }

        private void Update()
        {
            if (GetCurrentTextHash() != _lastKnownTextHash)
            {
                Resize();
                _lastKnownTextHash = GetCurrentTextHash();
            }
        }

        private void Refresh()
        {
            Resize();
        }

        private void Resize()
        {
            UnityEngine.Profiling.Profiler.BeginSample("TextSizeFilter.Resize()");
            _currentRect.sizeDelta = new Vector2(GetWidthByFit(), GetHeigthByFit());
            UnityEngine.Profiling.Profiler.EndSample();
        }

        private float GetWidthByFit()
        {
            return _horizontalFit switch
            {
                FitMode.PreferredSize => GetPrefferredWidth(),
                FitMode.MinSize => GetMinWidth(),
                FitMode.Unconstrained => GetTextWidth(),
                _ => GetTextWidth(),
            };
        }

        private float GetHeigthByFit()
        {
            return _verticalFit switch
            {
                FitMode.PreferredSize => GetPrefferredHeight(),
                FitMode.MinSize => GetMinHeight(),
                FitMode.Unconstrained => GetTextHeigh(),
                _ => GetTextHeigh(),
            };
        }

        private float GetPrefferredWidth()
        {
            return BasePrefferedWidth() + BaseFontSize() * FONT_SIZE_OFFSET;
        }
        private float GetPrefferredHeight()
        {
            return BasePrefferredHeigth() + BaseFontSize() * FONT_SIZE_OFFSET;
        }

        private float BasePrefferedWidth()
        {
            return _tmproText != null ? _tmproText.preferredWidth : _uiText != null ? _uiText.preferredWidth : default;
        }

        private float BasePrefferredHeigth()
        {
            return _tmproText != null ? _tmproText.preferredHeight : _uiText != null ? _uiText.preferredHeight : default;
        }

        private float BaseFontSize()
        {
            return _tmproText != null ? _tmproText.fontSize : _uiText != null ? _uiText.fontSize : default;
        }

        private float GetMinWidth()
        {
            return _tmproText != null ? _tmproText.renderedWidth: _uiText != null ? _uiText.preferredWidth: default;
        }

        private float GetMinHeight()
        {
            return _tmproText != null ? _tmproText.renderedHeight : _uiText != null ? _uiText.preferredHeight : default;
        }

        private float GetTextWidth()
        {
            return _currentRect != null ? _currentRect.sizeDelta.x : default;
        }

        private float GetTextHeigh()
        {
            return _currentRect != null ? _currentRect.sizeDelta.y : default;
        }

        private int GetCurrentTextHash()
        {
            return _tmproText != null ? _tmproText.text == null ? 0 : _tmproText.text.GetHashCode() : 
                   _uiText != null ? _uiText.text == null ? 0 : _uiText.text.GetHashCode() : 0;
        }

    }
}
