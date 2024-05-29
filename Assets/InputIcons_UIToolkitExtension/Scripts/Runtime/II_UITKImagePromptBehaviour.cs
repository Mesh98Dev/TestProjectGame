using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static InputIcons.II_SpritePrompt;

namespace InputIcons
{
    public class II_UITKImagePromptBehaviour : MonoBehaviour
    {
        private UIDocument rootDocument => GetComponent<UIDocument>();
        public List<UITKImagePromptData> spritePromptDatas = new List<UITKImagePromptData>();

        private void OnEnable()
        {
            InitializeUIDocumentData();
            UpdateDisplayedImages();
            InputIconsManagerSO.onBindingsChanged += UpdateDisplayedImages;
            InputIconsManagerSO.onControlsChanged += UpdateDisplayedSprites;
        }

        private void OnDisable()
        {
            InputIconsManagerSO.onBindingsChanged -= UpdateDisplayedImages;
            InputIconsManagerSO.onControlsChanged -= UpdateDisplayedSprites;
        }

        private void InitializeUIDocumentData()
        {
            for(int i=0;i<spritePromptDatas.Count; i++)
            {
                spritePromptDatas[i].image = rootDocument.rootVisualElement.Q(spritePromptDatas[i].imageID);
            }
        }

        public void UpdateDisplayedImages()
        {
            foreach (UITKImagePromptData s in spritePromptDatas)
            {
                s.UpdateDisplayedSprite();
            }
#if UNITY_EDITOR
            EditorApplication.QueuePlayerLoopUpdate();
#endif
        }
        private void UpdateDisplayedSprites(InputDevice inputDevice)
        {
            UpdateDisplayedImages();
        }


#if UNITY_EDITOR
        public void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;

        private void _OnValidate()
        {
            UnityEditor.EditorApplication.delayCall -= _OnValidate;
            if (this == null) return;
            InitializeUIDocumentData();
            UpdateDisplayedImages();
        }
#endif

        [System.Serializable]
        public class UITKImagePromptData : II_PromptData
        {
            
            public VisualElement image;
            public string imageID;

            public void UpdateDisplayedSprite()
            {
                if (actionReference == null)
                {
                    if (image != null)
                        image.style.backgroundImage = null;
                    return;
                }


                if (image != null)
                {
                    image.style.backgroundImage = new StyleBackground(GetKeySprite());
                }
                 
            }

        }
    }
}