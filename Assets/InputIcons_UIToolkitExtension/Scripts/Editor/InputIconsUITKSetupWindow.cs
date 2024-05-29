using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine.TextCore.Text;
using System;
using TextAsset = UnityEngine.TextCore.Text.TextAsset;
using UnityEngine.UIElements;
using System.IO;
using UnityEngine.TextCore;

namespace InputIcons
{
    public class InputIconsUITKSetupWindow : EditorWindow
    {
        private bool showAdvanced = false;

        Vector2 scrollPos;

        GUIStyle textStyleHeader;
        GUIStyle textStyle;
        GUIStyle textStyleYellow;
        GUIStyle textStyleBold;
        GUIStyle buttonStyle;

        private InputIconsUITKManagerSO manager;

        [MenuItem("Tools/Input Icons/Input Icons UI Toolkit Setup", priority = 2)]
        public static void ShowWindow()
        {
            const int width = 600;
            const int height = 500;

            var x = (Screen.currentResolution.width - width) / 2;
            var y = (Screen.currentResolution.height - height) / 2;

            //GetWindow<InputIconsUITKSetupWindow>("Input Icons Setup").iconSetSOs = InputIconSetConfiguratorSO.GetAllIconSetsOnConfigurator();
            EditorWindow window = GetWindow<InputIconsUITKSetupWindow>("Input Icons UI Toolkit Setup");
            window.position = new Rect(x, y, width, height);
        }

        private void OnGUI()
        {
            

            textStyleHeader = new GUIStyle(EditorStyles.boldLabel);
            textStyleHeader.wordWrap = true;
            textStyleHeader.fontSize = 14;

            textStyle = new GUIStyle(EditorStyles.label);
            textStyle.wordWrap = true;

            textStyleYellow = new GUIStyle(EditorStyles.label);
            textStyleYellow.wordWrap = true;
            textStyleYellow.normal.textColor = Color.yellow;

            textStyleBold = new GUIStyle(EditorStyles.boldLabel);
            textStyleBold.wordWrap = true;

            buttonStyle = EditorStyles.miniButtonMid;

            scrollPos =
               EditorGUILayout.BeginScrollView(scrollPos, GUILayout.ExpandWidth(true));

            InputIconsUITKManagerSO.Instance = (InputIconsUITKManagerSO)EditorGUILayout.ObjectField("Manager", InputIconsUITKManagerSO.Instance, typeof(InputIconsUITKManagerSO), false);
            manager = InputIconsUITKManagerSO.Instance;

            manager.uiDocumentUpdateOptions = (InputIconsManagerSO.TextUpdateOptions)EditorGUILayout.EnumPopup(new GUIContent("UI Document Update Setting",
    "Search and Update is reliable and updates all UI documents.\n" +
    "Via Input Icons Text Components requires you to add the II_UIDocument component to UI Documents which display Input Icons. " +
    "This method is more performant."), manager.uiDocumentUpdateOptions);

            EditorGUILayout.Space(5);
            DrawUILine(Color.grey);
            DrawUILine(Color.grey);

            GUILayout.Label("This window will help you to setup Input Icons for UI Toolbuilder more quickly.\n" +
                "We need to have all the needed assets for UI Toolbuilder\n" +
                "and we also need to have Text Sprite Assets containing all our sprites we want to display.\n" +
                "\n" +
                "There is not very much to do.\n" +
                "Just hit all the buttons below from top to bottom and you should be ready.", EditorStyles.label);


            if (manager == null)
            {
                GUILayout.Label("... must assign a manager first", textStyleYellow);
                EditorGUILayout.EndScrollView();
                return;
            }

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("UI Builder settings", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Make sure these are placed in a Resources (sub)folder", EditorStyles.label);
            manager.uitkPanelSettings = (PanelSettings)EditorGUILayout.ObjectField("Panel Settings", manager.uitkPanelSettings, typeof(PanelSettings), false);
            manager.uitkTextSettings = (TextSettings)EditorGUILayout.ObjectField("Text Settings", manager.uitkTextSettings, typeof(TextSettings), false);
            manager.uitkTextStyleSheet = (TextStyleSheet)EditorGUILayout.ObjectField("Text Style Sheet", manager.uitkTextStyleSheet, typeof(TextStyleSheet), false);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Default Sprite Asset Path", manager.defaultSpriteAssetPath);
            EditorGUI.EndDisabledGroup();

            if (manager.uitkPanelSettings == null || manager.uitkTextSettings == null || manager.uitkTextStyleSheet == null)
            {
                GUILayout.Label("To be able to setup, the above fields need to be filled. Create the necessary objects an reference them above.", textStyleYellow);
                EditorGUI.BeginDisabledGroup(true);

            }
            if (GUILayout.Button("Setup Panel/Text/TextStyleSheet.\nThis will make sure these objects are correctly referenced within each other."))
            {
                SetupPanelTextAndTextStyleSheet();
            }
            if (manager.uitkPanelSettings == null || manager.uitkTextSettings == null || manager.uitkTextStyleSheet == null)
            {
                EditorGUI.EndDisabledGroup();
            }

            EditorGUILayout.Space(10);
            DrawUILine(Color.grey);

            EditorGUILayout.LabelField("Add styles per input action to Text Style Sheet", EditorStyles.boldLabel);
            manager.uitkTextStyleSheet = (TextStyleSheet)EditorGUILayout.ObjectField("Text Style Sheet", manager.uitkTextStyleSheet, typeof(TextStyleSheet), false);
            GUILayout.Label("If you already did the setup of Input Icons for TMPro these styles might already exist in the Text Style Sheet as well and you can skip this step.\n" +
                "But you can do this step anyway, nothing will break.\n" +
                "I recommend the Option 2 as it is much faster", textStyleYellow);

            EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.Width(50));

            EditorGUILayout.LabelField("Option 1: Setup with recompilation.", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Slower, but no manual steps needed");
            if (GUILayout.Button("Prepare Text Style Sheet With Empty Values\n- then recompile to save changes"))
            {
                InputIconsUITKManagerSO.HandleStylesPrepared();
                CompilationPipeline.RequestScriptCompilation();
            }

            if (!EditorApplication.isCompiling)
            {
                if (GUILayout.Button("Add Style Entries To Text Style Sheet\n- then recompile to save changes"))
                {
                    InputIconsUITKManagerSO.HandleStylesAdded();
                    CompilationPipeline.RequestScriptCompilation();
                }
            }
            else
            {
                GUILayout.Label("... waiting for compilation ...", textStyleYellow);
            }

            EditorGUILayout.EndVertical();
            DrawUILineVertical(Color.grey);

            EditorGUILayout.BeginVertical(GUILayout.Width(100));

            EditorGUILayout.LabelField("Option 2: Setup with manual saving.", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Faster, make a small change to the style sheet\n" +
                "between steps to save it", GUILayout.Height(25));
            if (GUILayout.Button("Prepare Text Style Sheet With Empty Values\n" +
                "then make a manual change"))
            {
                InputIconsUITKManagerSO.HandleStylesPrepared();
                EditorGUIUtility.PingObject(manager.uitkTextStyleSheet);
            }

            GUILayout.Label("Now make a small change and undo it in the Text Style Sheet to save the asset.", textStyleYellow);

            if (GUILayout.Button("Add Style Entries To Text Style Sheet"))
            {
                InputIconsUITKManagerSO.HandleStylesAdded();
            }


            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            DrawUILine(Color.grey);

            EditorGUILayout.LabelField("Created Text Sprite Assets - Must have the same names as IconSets (Icon Set Name) on Configurator", EditorStyles.boldLabel);
            GUILayout.Label("BEFORE you do this, do the setup of the standard Input Icons. During that setup, the sprite assets " +
               "will be created which are necessary for the following to work.\n" +
               "You need to do this at least once to create the references to the Sprite Atlases in the following Text Assets", textStyleYellow);
            EditorGUILayout.LabelField("Don't have those assets yet or you created new sprite atlases and need to update them?\n" +
                "Use the following buttons.", EditorStyles.label, GUILayout.Height(30));
            GUILayout.Label("Ignore any MissingReferenceExceptions you might get (material has been destroyed ...)", textStyleYellow);
            EditorGUILayout.Space(5);
            if (GUILayout.Button("Create Text Sprite Assets (in TMPro default Sprite Asset folder)"))
            {
                InputIconsUITKSpritePacker.CreateSpriteAssetsOfIconSets();
            }
            if (GUILayout.Button("Try to automatically assign Text Sprite Assets (from TMPro default Sprite Assets folder)"))
            {
                TryToAutomaticallyAssignSpriteAssets();

            }

            manager.keyboardTextAsset = (TextAsset)EditorGUILayout.ObjectField("Keyboard Text Asset", manager.keyboardTextAsset, typeof(TextAsset), false);
            manager.nintendoProTextAsset = (TextAsset)EditorGUILayout.ObjectField("Nintendo Pro Text Asset", manager.nintendoProTextAsset, typeof(TextAsset), false);
            manager.ps3TextAsset = (TextAsset)EditorGUILayout.ObjectField("PS3 Text Asset", manager.ps3TextAsset, typeof(TextAsset), false);
            manager.ps4TextAsset = (TextAsset)EditorGUILayout.ObjectField("PS4 Text Asset", manager.ps4TextAsset, typeof(TextAsset), false);
            manager.ps5TextAsset = (TextAsset)EditorGUILayout.ObjectField("PS5 Text Asset", manager.ps5TextAsset, typeof(TextAsset), false);
            manager.xBoxTextAsset = (TextAsset)EditorGUILayout.ObjectField("XBox Text Asset", manager.xBoxTextAsset, typeof(TextAsset), false);


            if (GUILayout.Button("Move Text Sprite Assets to correct folder\n" +
                "and rename them to to match names on InputIconsConfigurator"))
            {
                MoveAndRenameSpriteAssets();
            }

            if (GUILayout.Button("Try to improve glyph positions"))
            {
                CorrectPositionOfGlyphs();
            }

            EditorGUILayout.Space(10);
            DrawUILine(Color.grey);
            DrawUILine(Color.grey);
            EditorGUILayout.Space(10);
            DrawAdvanced();
            EditorGUILayout.Space(30);

            if(EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(manager);
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawAdvanced()
        {
            GUILayout.Label("Advanced", EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            showAdvanced = EditorGUILayout.Foldout(showAdvanced, "TMP Style Sheet manipulation");
            if (showAdvanced)
            {
                EditorGUILayout.HelpBox("You can use this button to remove Input Icons style" +
                    " entries from the TMPro style sheet.", MessageType.Warning);

                var style = new GUIStyle(GUI.skin.button);

                if (GUILayout.Button("Remove all Input Icon styles from the text style sheet.", style))
                {
                    InputIconsUITKManagerSO.RemoveAllStyleSheetEntries();
                }
            }
            EditorGUILayout.EndVertical();
        }

        public void CorrectPositionOfGlyphs()
        {
            CorrectPositionOfGlyphInTextSpriteAsset(manager.keyboardTextAsset);
            CorrectPositionOfGlyphInTextSpriteAsset(manager.nintendoProTextAsset);
            CorrectPositionOfGlyphInTextSpriteAsset(manager.ps3TextAsset);
            CorrectPositionOfGlyphInTextSpriteAsset(manager.ps4TextAsset);
            CorrectPositionOfGlyphInTextSpriteAsset(manager.ps5TextAsset);
            CorrectPositionOfGlyphInTextSpriteAsset(manager.xBoxTextAsset);
            Debug.Log("Glyph positions improved");
        }

        private void CorrectPositionOfGlyphInTextSpriteAsset(TextAsset textAsset)
        {
            if (textAsset == null)
                return;

            SpriteAsset spriteAsset = textAsset as SpriteAsset;
            foreach (SpriteGlyph glyph in spriteAsset.spriteGlyphTable)
            {
                float h = glyph.metrics.height;
                GlyphMetrics metrics = glyph.metrics;
                metrics.horizontalBearingX = 0;
                metrics.horizontalBearingY = h / 4 * 3;
                glyph.metrics = metrics;
            }

            EditorUtility.SetDirty(textAsset);

        }

        public void SetupPanelTextAndTextStyleSheet()
        {
            Debug.Log("Trying to setup Panel/Text/TextStyleSheet ...");
            manager.uitkPanelSettings.textSettings = (PanelTextSettings)manager.uitkTextSettings;
            manager.uitkTextSettings.defaultStyleSheet = manager.uitkTextStyleSheet;

            string stylesheetResourcesPath = AssetDatabase.GetAssetPath(manager.uitkTextStyleSheet);
            stylesheetResourcesPath = stylesheetResourcesPath.Substring(stylesheetResourcesPath.IndexOf("Resources/"));
            stylesheetResourcesPath = stylesheetResourcesPath.Replace("Resources/", "");
            stylesheetResourcesPath = stylesheetResourcesPath.Replace(manager.uitkTextStyleSheet.name + ".asset", "");
            //Debug.Log("Style Sheet Resources Path: " + stylesheetResourcesPath);
            manager.uitkTextSettings.styleSheetsResourcePath = stylesheetResourcesPath;

            manager.uitkTextSettings.defaultSpriteAssetPath = manager.defaultSpriteAssetPath;

            EditorUtility.SetDirty(manager.uitkPanelSettings);
            EditorUtility.SetDirty(manager.uitkTextSettings);
            EditorUtility.SetDirty(manager.uitkTextStyleSheet);
            Debug.Log("Setup Panel/Text/TextStyleSheet completed.");
        }

        private void TryToAutomaticallyAssignSpriteAssets()
        {
            List<InputIconSetBasicSO> iconSOs = InputIconSetConfiguratorSO.GetAllIconSetsOnConfigurator();


            Debug.Log("Searching Input Icons sprite atlases in the " + InputIconsManagerSO.Instance.TEXTMESHPRO_SPRITEASSET_FOLDERPATH + " folder");
            List<SpriteAsset> spriteAssets = new List<SpriteAsset>();
            string[] filePaths = Directory.GetFiles(InputIconsManagerSO.Instance.TEXTMESHPRO_SPRITEASSET_FOLDERPATH); // Get all file paths in the folder

            foreach (string filePath in filePaths)
            {
                if (Path.GetExtension(filePath) == ".asset") // Adjust the file extension according to your requirements
                {
                    SpriteAsset spriteAsset = AssetDatabase.LoadAssetAtPath<SpriteAsset>(filePath);
                    if (spriteAsset != null)
                    {
                        spriteAssets.Add(spriteAsset); // Add the TextAsset to the list
                        //Debug.Log("found text Asset: " + spriteAsset.name);
                    }
                }
            }

            manager.keyboardTextAsset = TryToAssignTextAsset(InputIconSetConfiguratorSO.Instance.keyboardIconSet, spriteAssets);
            manager.nintendoProTextAsset = TryToAssignTextAsset(InputIconSetConfiguratorSO.Instance.switchIconSet, spriteAssets);
            manager.ps3TextAsset = TryToAssignTextAsset(InputIconSetConfiguratorSO.Instance.ps3IconSet, spriteAssets);
            manager.ps4TextAsset = TryToAssignTextAsset(InputIconSetConfiguratorSO.Instance.ps4IconSet, spriteAssets);
            manager.ps5TextAsset = TryToAssignTextAsset(InputIconSetConfiguratorSO.Instance.ps5IconSet, spriteAssets);
            manager.xBoxTextAsset = TryToAssignTextAsset(InputIconSetConfiguratorSO.Instance.xBoxIconSet, spriteAssets);
        }

        private void MoveAndRenameSpriteAssets()
        {
            string destinationPath = AssetDatabase.GetAssetPath(manager);
            destinationPath = destinationPath.Replace("InputIcons/" + manager.name + ".asset", "");
            destinationPath += "/" + manager.uitkTextSettings.defaultSpriteAssetPath;

            if (!Directory.Exists(destinationPath))
            {
                Debug.Log("creating folder");
                Directory.CreateDirectory(destinationPath);
                AssetDatabase.Refresh();
            }

            MoveAsset(manager.keyboardTextAsset, destinationPath);
            MoveAsset(manager.nintendoProTextAsset, destinationPath);
            MoveAsset(manager.ps3TextAsset, destinationPath);
            MoveAsset(manager.ps4TextAsset, destinationPath);
            MoveAsset(manager.ps5TextAsset, destinationPath);
            MoveAsset(manager.xBoxTextAsset, destinationPath);
            AssetDatabase.Refresh();

            RenameAsset(manager.keyboardTextAsset, InputIconSetConfiguratorSO.Instance.keyboardIconSet.iconSetName);
            RenameAsset(manager.nintendoProTextAsset, InputIconSetConfiguratorSO.Instance.switchIconSet.iconSetName);
            RenameAsset(manager.ps3TextAsset, InputIconSetConfiguratorSO.Instance.ps3IconSet.iconSetName);
            RenameAsset(manager.ps4TextAsset, InputIconSetConfiguratorSO.Instance.ps4IconSet.iconSetName);
            RenameAsset(manager.ps5TextAsset, InputIconSetConfiguratorSO.Instance.ps5IconSet.iconSetName);
            RenameAsset(manager.xBoxTextAsset, InputIconSetConfiguratorSO.Instance.xBoxIconSet.iconSetName);
            AssetDatabase.Refresh();

            EditorGUIUtility.PingObject(manager.keyboardTextAsset);

            Debug.Log("Text Assets Moved and Renamed");
        }

        private SpriteAsset TryToAssignTextAsset(InputIconSetBasicSO iconSet, List<SpriteAsset> spriteAssets)
        {
            for (int i = 0; i < spriteAssets.Count; i++)
            {
                if (spriteAssets[i].name.Contains(iconSet.iconSetName))
                {
                    return spriteAssets[i];
                }
            }
            return null;
        }

        private void MoveAsset(UnityEngine.Object obj, string destination)
        {
            destination += obj.name + ".asset";
            string assetPath = AssetDatabase.GetAssetPath(obj);

            AssetDatabase.MoveAsset(assetPath, destination);
        }

        private void RenameAsset(UnityEngine.Object obj, string newName)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);

            string assetFolderPath = System.IO.Path.GetDirectoryName(assetPath);
            string[] assetPathsInFolder = AssetDatabase.FindAssets("", new[] { assetFolderPath });

            // Check if another asset with the same name already exists
            foreach (string path in assetPathsInFolder)
            {
                string existingAssetPath = AssetDatabase.GUIDToAssetPath(path);
                string existingAssetName = System.IO.Path.GetFileNameWithoutExtension(existingAssetPath);

                // Skip the current asset being renamed
                if (existingAssetPath == assetPath)
                    continue;

                // Check if an asset with the same name or the new name already exists
                if (existingAssetName == newName || existingAssetName == "_old_" + newName)
                {
                    // Append "_old" to the existing asset's name
                    string oldAssetPath = System.IO.Path.Combine(assetFolderPath, "_old_" + existingAssetName + GetRandomNumber());
                    AssetDatabase.RenameAsset(existingAssetPath, System.IO.Path.GetFileName(oldAssetPath));
                }
            }

            AssetDatabase.RenameAsset(assetPath, newName);
        }

        private int GetRandomNumber()
        {
            System.Random random = new System.Random();
            return random.Next(1000, 9999);
        }

        private string GetRelativePath(string absolutePath)
        {
            string projectFolder = Application.dataPath;
            Uri projectUri = new Uri(projectFolder);
            Uri fileUri = new Uri(absolutePath);
            return projectUri.MakeRelativeUri(fileUri).ToString();
        }

        protected void OnEnable()
        {
            // load values
            var data = EditorPrefs.GetString("InputIconsUITKSetupWindow", JsonUtility.ToJson(this, false));
            JsonUtility.FromJsonOverwrite(data, this);

            position.Set(position.x, position.y, 80, 500);

        }

        protected void OnDisable()
        {
            // save values
            var data = JsonUtility.ToJson(this, false);
            EditorPrefs.SetString("InputIconsUITKSetupWindow", data);
        }

        public static void DrawUILine(Color color, int thickness = 2, int padding = 5)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            //r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        public static void DrawUILineVertical(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(padding+thickness), GUILayout.ExpandHeight(true));
            r.width = thickness;
            r.x += padding / 2;
            r.y -= 2;
            r.height += 3;
            EditorGUI.DrawRect(r, color);
        }
    }

}
