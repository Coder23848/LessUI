using Menu.Remix.MixedUI;
using UnityEngine;

namespace LessUI
{
    public class PluginOptions : OptionInterface
    {
        public static PluginOptions Instance = new();

        public static Configurable<bool> RemoveKillFeed = Instance.config.Bind("RemoveKillFeed", true, new ConfigurableInfo("Remove the kill feed from the shelter screen."));
        public static Configurable<bool> RemoveTokenTracker = Instance.config.Bind("RemoveTokenTracker", true, new ConfigurableInfo("Remove the token tracker from the shelter screen."));

        public override void Initialize()
        {
            base.Initialize();
            Tabs = new OpTab[1];

            Tabs[0] = new(Instance, "Options");
            CheckBoxOption(RemoveKillFeed, 0, "Remove Kill Feed");
            CheckBoxOption(RemoveTokenTracker, 1, "Remove Token Tracker");
        }

        private void CheckBoxOption(Configurable<bool> setting, float pos, string label)
        {
            Tabs[0].AddItems(new OpCheckBox(setting, new(50, 550 - pos * 30)) { description = setting.info.description }, new OpLabel(new Vector2(90, 550 - pos * 30), new Vector2(), label, FLabelAlignment.Left));
        }
        private void SliderOption(Configurable<float> setting, int size, float pos, string label)
        {
            Tabs[0].AddItems(new OpFloatSlider(setting, new(50, 545 - pos * 30), size) { description = setting.info.description }, new OpLabel(new Vector2(60 + size, 550 - pos * 30), new Vector2(), label, FLabelAlignment.Left));
        }
    }
}