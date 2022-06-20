using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TextAsset = UnityEngine.TextCore.Text.TextAsset;

namespace RotaryHeartAddon.Internal
{
    public enum Templates
    {
        SDictionary,
        MonoDictionary,
        SoDictionary,
    }

    public static class CreateScripts
    {
        #region VARIABLES

        #region TEMPLATE PATHS

        private static string _dictionarySoTemplatePath =
            "Assets/Addons/RotaryHeartAddon/Editor/Resources/template_dictionaryso.txt";

        private static string _monoDictionaryTemplatePath =
            "Assets/Addons/RotaryHeartAddon/Editor/Resources/template_monobehaviour_dictionary.txt";

        private static string _sDictionaryTemplatePath =
            "Assets/Addons/RotaryHeartAddon/Editor/Resources/template_serialized_dictionary.txt";

        #endregion

        #region TAGS

        private static string _namespaceTag = "%NAMESPACE";
        private static string _classNameTag = "%CLASSNAME";
        private static string _sDictionaryTag = "%SDICTIONARY";
        private static string _keyTag = "%KEY";
        private static string _valueTag = "%VALUE";

        #endregion

        #endregion

        #region METHODS

        public static string CreateFromTemplate(Templates templates, string className, string sClassName, string key, string value, string @namespace = "")
        {
            List<string> template = File.ReadLines(ToFilePath(templates)).ToList();

            string rootNamespace = @namespace;
            if (string.IsNullOrEmpty(rootNamespace)) rootNamespace = EditorSettings.projectGenerationRootNamespace;

            bool hasNamespace = !string.IsNullOrEmpty(rootNamespace);

            int length = template.Count;
            int namespaceIndex = 0;
            for (int i = 0; i < length; i++)
            {
                if (!hasNamespace && template[i].Contains(_namespaceTag))
                {
                    namespaceIndex = i;
                    continue;
                }

                if (hasNamespace && template[i].Contains(_namespaceTag))
                {
                    template[i] = template[i].Replace(_namespaceTag, rootNamespace);
                    continue;
                }

                if (template[i].Contains(_classNameTag))
                {
                    template[i] = template[i].Replace(_classNameTag, className);
                }

                if (template[i].Contains(_sDictionaryTag))
                {
                    template[i] = template[i].Replace(_sDictionaryTag, sClassName);
                }

                if (template[i].Contains(_keyTag))
                {
                    template[i] = template[i].Replace(_keyTag, key);
                }

                if (template[i].Contains(_valueTag))
                {
                    template[i] = template[i].Replace(_valueTag, value);
                }

            }

            if (hasNamespace) return string.Join("\n", template);

            template.RemoveAt(length - 1); // Remove last } bracket
            template.RemoveAt(namespaceIndex + 1); // Remove { after namespace
            template.RemoveAt(namespaceIndex); // Remove namespace

            return string.Join("\n", template);
        }

        #endregion

        #region HELPERS

        private static string ToFilePath(Templates template)
        {
            switch (template)
            {
                case Templates.SDictionary:
                    return _sDictionaryTemplatePath;
                case Templates.MonoDictionary:
                    return _monoDictionaryTemplatePath;
                case Templates.SoDictionary:
                    return _dictionarySoTemplatePath;
                default:
                    throw new Exception($"Dictionary Template - {template} is not defined!");
            }
        }

        #endregion
    }
}