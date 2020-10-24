using System;
using DustInTheWind.Dot.ConsoleHelpers.ConsoleUtil;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class SettingsView
    {
        private readonly SelectableMenu<SettingsMenuItem> menu;

        private readonly LabelMenuItem<SettingsMenuItem> musicMenuItem;
        private readonly LabelMenuItem<SettingsMenuItem> sfxMenuItem;
        private readonly LabelMenuItem<SettingsMenuItem> voiceMenuItem;
        private readonly LabelMenuItem<SettingsMenuItem> closeMenuItem;

        public SettingsView()
        {
            musicMenuItem = new LabelMenuItem<SettingsMenuItem>
            {
                Text = "Music Volume",
                Value = SettingsMenuItem.VolumeMusic,
                HorizontalAlign = HorizontalAlign.Center
            };

            sfxMenuItem = new LabelMenuItem<SettingsMenuItem>
            {
                Text = "SFX Volume",
                Value = SettingsMenuItem.VolumeSfx,
                HorizontalAlign = HorizontalAlign.Center
            };

            voiceMenuItem = new LabelMenuItem<SettingsMenuItem>
            {
                Text = "Voice Volume",
                Value = SettingsMenuItem.VolumeVoice,
                HorizontalAlign = HorizontalAlign.Center
            };

            closeMenuItem = new LabelMenuItem<SettingsMenuItem>
            {
                Text = "Close",
                Value = SettingsMenuItem.Close,
                HorizontalAlign = HorizontalAlign.Center,
                Key = ConsoleKey.Escape
            };

            menu = new SelectableMenu<SettingsMenuItem>
            {
                musicMenuItem,
                sfxMenuItem,
                voiceMenuItem,
                new SpaceMenuItem<SettingsMenuItem>(),
                closeMenuItem
            };
        }

        public void DisplayMenu()
        {
            CustomConsole.WriteLine("Settings", ConsoleColor.Green, HorizontalAlign.Center);
            CustomConsole.WriteLine();

            menu.Display();
        }
    }
}