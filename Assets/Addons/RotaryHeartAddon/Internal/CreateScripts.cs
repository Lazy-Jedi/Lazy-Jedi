using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

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

        private static string _dictionarySoTemplatePath = "template_dictionaryso";

        private static string _monoDictionaryTemplatePath = "template_monobehaviour_dictionary";

        private static string _sDictionaryTemplatePath = "template_serialized_dictionary";

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
            List<string> template = ToStringList(templates);

            bool hasNamespace = !string.IsNullOrEmpty(@namespace);

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
                    template[i] = template[i].Replace(_namespaceTag, @namespace);
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

            Resources.UnloadUnusedAssets();
            return string.Join("\n", template);
        }

        #endregion

        #region HELPERS

        private static List<string> ToStringList(Templates template)
        {
            switch (template)
            {
                case Templates.SDictionary:
                    return Resources.Load<TextAsset>(_sDictionaryTemplatePath).text.Split('\n').ToList();
                case Templates.MonoDictionary:
                    return Resources.Load<TextAsset>(_monoDictionaryTemplatePath).text.Split('\n').ToList();
                case Templates.SoDictionary:
                    return Resources.Load<TextAsset>(_dictionarySoTemplatePath).text.Split('\n').ToList();
                default:
                    throw new Exception($"Template - {template} is not defined!");
            }
        }

        #endregion
    }
}