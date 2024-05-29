using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputIcons.II_ImagePrompt;

namespace InputIcons
{
    [CustomEditor(typeof(II_UITKImagePromptBehaviour))]

    public class II_UITKImagePromptBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            II_UITKImagePromptBehaviour imagePromptBehaviour = (II_UITKImagePromptBehaviour)target;

            EditorGUI.BeginChangeCheck();

            foreach (II_UITKImagePromptBehaviour.UITKImagePromptData sData in imagePromptBehaviour.spritePromptDatas.ToList())
            {
                sData.actionReference = (InputActionReference)EditorGUILayout.ObjectField("Action Reference", sData.actionReference, typeof(InputActionReference), false);

                if (sData.actionReference != null)
                {
                    sData.bindingSearchStrategy = (ImagePromptData.BindingSearchStrategy)EditorGUILayout.EnumPopup("Search Binding By", sData.bindingSearchStrategy);
                    if (sData.bindingSearchStrategy == ImagePromptData.BindingSearchStrategy.BindingType)
                    {
                        if (InputIconsUtility.ActionIsComposite(sData.actionReference.action))
                        {
                            sData.bindingType = (InputIconsUtility.BindingType)EditorGUILayout.EnumPopup("Binding Type", sData.bindingType);
                        }
                        sData.deviceType = (InputIconsUtility.DeviceType)EditorGUILayout.EnumPopup("Device Type", sData.deviceType);
                    }
                    else if (sData.bindingSearchStrategy == ImagePromptData.BindingSearchStrategy.BindingIndex)
                    {
                        sData.bindingIndex = EditorGUILayout.IntSlider("Binding Index", sData.bindingIndex, 0, sData.actionReference.action.bindings.Count - 1);
                    }
                }

                sData.imageID = EditorGUILayout.TextField("Visual Element Name", sData.imageID);

                EditorGUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                {
                    imagePromptBehaviour.spritePromptDatas.Remove(sData);
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(20);
            }


            if (GUILayout.Button("Add"))
            {
                imagePromptBehaviour.spritePromptDatas.Add(new II_UITKImagePromptBehaviour.UITKImagePromptData());
            }

            if (EditorGUI.EndChangeCheck())
            {
                imagePromptBehaviour.OnValidate();
            }

        }
    }

}
