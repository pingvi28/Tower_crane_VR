using UnityEditor;

namespace CombustionVR.ScenarioSystem.Editor
{
    public static class ScenarioEditorUtils
    {
        private const string SymbolName = "DEBUG_SCENARIO";

        [MenuItem("CombustionVR/Switch DebugScenario")]
        private static void SwitchDebugScenario()
        {
            SwitchSymbols(SymbolName);
        }

        private static void SwitchSymbols(string symbolName)
        {
            var definedSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);

            if (definedSymbols.Contains(symbolName))
            {
                definedSymbols = definedSymbols.Replace(symbolName, "");
                definedSymbols = definedSymbols.Replace("  ", " ");
            }
            else
            {
                if (definedSymbols.Length == 0)
                {
                    definedSymbols = symbolName;
                }
                else
                {
                    definedSymbols = definedSymbols + " " + symbolName;
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, definedSymbols);
        }
    }
}