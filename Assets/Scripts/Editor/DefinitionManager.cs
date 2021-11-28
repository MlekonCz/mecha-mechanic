using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using MechParts;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;


    public enum ManagerState
    {
        // 1. Add new field that will be used as a Tab
        MechParts,
        Sounds,
    }

    public class DefinitionManager : OdinMenuEditorWindow
    {
        [OnValueChanged("StateChange")]
        [LabelText("Manager View")]
        [LabelWidth(100f)]
        [EnumToggleButtons]
        [ShowInInspector]
        private ManagerState _managerState;

        private bool _treeRebuild = false;
        private int _enumIndex = 0;

        // 2. Declare definition that you want to display in Tab
        // If you will want more folders in one Tab like in Weapons have Melee and Ranged 
        // You have to declare it here for each new folder that will be in Tab to be able to select path for it
        private readonly DrawSelected<MechPartDefinition> _drawMechParts = new DrawSelected<MechPartDefinition>();
        private readonly DrawSelected<SFXClipDefinition> _drawExplosionSounds = new DrawSelected<SFXClipDefinition>();
        private readonly DrawSelected<SFXClipDefinition> _drawGunShotSounds = new DrawSelected<SFXClipDefinition>();
        private readonly DrawSelected<SFXClipDefinition> _drawReloadSounds = new DrawSelected<SFXClipDefinition>();


        // 3. Declare path for each folder you will want to be displayed in which are SO 
        
        private const string _mechPartDefinition = "Assets/Data/MechParts";
        
        private const string _explosionsSFXDefinitionPath = "Assets/Data/Audio/PlaceHolder 1";
        private const string _gunShotsSFXDefinitionPath = "Assets/Data/Audio/PlaceHolder 2";
        private const string _reloadsSFXDefinitionPath = "Assets/Data/Audio/PlaceHolder 3";
       
        

        [MenuItem("Tools/Definition Manager &#D")]
        public static void OpenWindow()
        {
            GetWindow<DefinitionManager>().Show();
        }

        private void StateChange()
        {
            _treeRebuild = true;
        }

        // 4. Setting path to definition
        protected override void Initialize()
        {
            _drawMechParts.SetPath(_mechPartDefinition);
            
            _drawExplosionSounds.SetPath(_explosionsSFXDefinitionPath);
            _drawGunShotSounds.SetPath(_gunShotsSFXDefinitionPath);
            _drawReloadSounds.SetPath(_reloadsSFXDefinitionPath);
        }

        protected override void OnGUI()
        {
            if (_treeRebuild && Event.current.type == EventType.Layout)
            {
                ForceMenuTreeRebuild();
                _treeRebuild = false;
            }

            // SirenixEditorGUI.IconButton((Texture) AssetDatabase.LoadAssetAtPath(headerImagePath, typeof(Texture)), 450,
            // 100);
            SirenixEditorGUI.Title("Definition Manager", "Tool made by MlekonCz", TextAlignment.Left,
                true);
            EditorGUILayout.Space();
            switch (_managerState)
            {
                // 5. Here you add enum so it will create new Tab in Definition Manager
                case ManagerState.MechParts:
                case ManagerState.Sounds:   
                    DrawEditor(_enumIndex);
                    break;
                default:
                    break;
            }

            EditorGUILayout.Space();
            base.OnGUI();
        }

        protected override void DrawEditors()
        {
            switch (_managerState)
            {
                // 6. For each Tab you will have you need to add case so system will be able to update when selected new Tab
                case ManagerState.MechParts:
                    _drawMechParts.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Sounds:
                    _drawExplosionSounds.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                default:
                    break;
            }

            DrawEditor((int) _managerState);
        }

        protected override IEnumerable<object> GetTargets()
        {
            // 7. Adding definitions to the enums in the list
            // THEY HAS TO BE IN SAME ORDERS AS ENUMS THAT ARE ON TOP!
            // in case you are not using some enum Then you just write: targets.Add(null);
            List<object> targets = new List<object>();
            targets.Add(_drawMechParts);
            targets.Add(_drawExplosionSounds);
            targets.Add(base.GetTarget());

            _enumIndex = targets.Count - 1;
            return targets;
        }

        protected override void DrawMenu()
        {
            switch (_managerState)
            {
                // 8. Add here State that you want to add
                case ManagerState.MechParts:
                case ManagerState.Sounds:
                    base.DrawMenu();
                    break;
                default:
                    break;
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            switch (_managerState)
            {
                // 9. Here you add folders to the Tabs that you want to be displayed
                // If you want to add multiple folders to one Tab you just need to 
                // write tree.AddAllAssetsAtPath(...) 
                //      typeof(...)
                // for each folder under case which you want to add it to
                case ManagerState.MechParts:
                    tree.AddAllAssetsAtPath("Weapon Definition", _mechPartDefinition, 
                        typeof(MechPartDefinition));
                    break;
                case ManagerState.Sounds:
                    tree.AddAllAssetsAtPath("Explosion Sounds", _explosionsSFXDefinitionPath,
                        typeof(SFXClipDefinition));
                    tree.AddAllAssetsAtPath("GunShot Sounds", _gunShotsSFXDefinitionPath,
                        typeof(SFXClipDefinition));
                    tree.AddAllAssetsAtPath("Reload Sounds", _reloadsSFXDefinitionPath,
                        typeof(SFXClipDefinition));
                    break;
                default:
                    break;
            }

            return tree;
        }
    }

    public class DrawSelected<T> where T : ScriptableObject
    {
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public T selected;

        [LabelWidth(100)] [PropertyOrder(-1)] [HorizontalGroup("Horizontal")]
        public string nameForNew;

        private string path;
        
        private bool deleteCheck = false;
        
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.7f, 1f, 0.5f)]
        [Button(ButtonSizes.Medium)]
        public void CreateNew()
        {
            if (string.IsNullOrWhiteSpace(nameForNew))
            {
                return;
            }

            List<T> instancesInFolder = new List<T>();
            
            instancesInFolder = AssetDatabase.FindAssets("", new string[] {path})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>).ToList();

            foreach (var scriptableObject in instancesInFolder)
            {
                if (scriptableObject.name == nameForNew)
                {
                    UnityEngine.Debug.Log("Name already used!");
                    return;
                }
            }
            
            T newItem = ScriptableObject.CreateInstance<T>();
            // newItem.name = "New " + typeof(T).ToString();

            if (string.IsNullOrWhiteSpace(path))
            {
                path = "Assets/";
            }

            AssetDatabase.CreateAsset(newItem, path + "\\" + nameForNew + ".asset");
            AssetDatabase.SaveAssets();
            nameForNew = "";
        }
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.4f, 0.8f, 1f)]
        [Button(ButtonSizes.Medium)]
        public void CopySelected()
        {
            if (string.IsNullOrWhiteSpace(nameForNew))
            {
                return;
            }
            
            List<T> instancesInFolder = new List<T>();
            
            instancesInFolder = AssetDatabase.FindAssets("", new string[] {path})
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>).ToList();

            foreach (var scriptableObject in instancesInFolder)
            {
                if (scriptableObject.name == nameForNew)
                {
                    UnityEngine.Debug.Log("Name already used!");
                    return;
                }
            }
            
            T clonedScriptableObject = ScriptableObject.Instantiate(original:selected) as T;
            
            if (string.IsNullOrWhiteSpace(path))
            {
                path = "Assets/";
            }

            AssetDatabase.CreateAsset(clonedScriptableObject, path + "\\" + nameForNew + ".asset");
            AssetDatabase.SaveAssets();
            nameForNew = "";
        }

        [HideIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(1f, 0f, 0f)]
        [Button]
        public void DeleteSelected()
        {
            deleteCheck = true;
        }
        [ShowIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(0.7f, 1f, 0.5f)]
        [Button(ButtonSizes.Small)]
        public void No()
        {
            deleteCheck = false;
        }
        
        [ShowIf("deleteCheck",true)]
        [HorizontalGroup("Horizontal")]
        [GUIColor(1f, 0f, 0f)]
        [Button(ButtonSizes.Small)]
        public void Yes()
        {
            deleteCheck = false;
            if (selected != null)
            {
                string _path = AssetDatabase.GetAssetPath(selected);
                AssetDatabase.DeleteAsset(_path);
                AssetDatabase.SaveAssets();
            }
        }
       

        public void SetSelected(object item)
        {
            if (selected != item)
            {
                deleteCheck = false;
            }
            var attempt = item as T;
            if (attempt != null)
            {
                this.selected = attempt;
            }
        }

        public void SetPath(string path)
        {
            this.path = path;
        }
    }