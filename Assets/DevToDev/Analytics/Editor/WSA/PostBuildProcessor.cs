using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace DevToDev.Editor.WSA
{
    public static class PostBuildProcessor
    {
        private const string PROJECT_FILE_EXTENSION = ".vcxproj";

        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.WSAPlayer)
            {
                try
                {
                    var pathToProjectFile = Path.Combine(
                        pathToBuiltProject, 
                        PlayerSettings.productName,
                        PlayerSettings.productName + PROJECT_FILE_EXTENSION);
                    XDocument doc = XDocument.Load(pathToProjectFile);
                    XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
                    XElement projectElement = doc.Descendants(ns + "Project").First();
                    XElement itemGroup = new XElement(ns + "ItemGroup");
                    XElement reference = new XElement(ns + "Reference");
                    reference.SetAttributeValue("Include", "DevToDev.Background");
                    reference.Add(
                        new XElement(ns + "HintPath", @"Managed\DevToDev.Background.winmd"),
                        new XElement(ns + "IsWinMDFile", "true"));
                    itemGroup.Add(reference);
                    projectElement.Add(itemGroup);
                    doc.Save(pathToProjectFile);
                    Debug.Log($"[{nameof(PostBuildProcessor)}] Project file has been edited successfully!");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[{nameof(PostBuildProcessor)}] {ex.GetType()}\n{ex.Message}\n{ex.StackTrace}");
                }
            }
        }
    }
}
