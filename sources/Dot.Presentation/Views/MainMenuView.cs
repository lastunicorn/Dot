using System;
using DustInTheWind.Dot.AudioSupport;
using DustInTheWind.Dot.ConsoleHelpers.UIControls;
using DustInTheWind.Dot.Presentation.Controls;

namespace DustInTheWind.Dot.Presentation.Views
{
    public class MainMenuView : ViewBase
    {
        private SelectableMenu<MainMenuItem> menu;
        private Separator separator;
        private LabelMenuItem<MainMenuItem> newGameMenuItem;
        private LabelMenuItem<MainMenuItem> continueLastGameMenuItem;
        private LabelMenuItem<MainMenuItem> resumeGameMenuItem;
        private LabelMenuItem<MainMenuItem> saveMenuItem;
        private LabelMenuItem<MainMenuItem> loadMenuItem;
        private LabelMenuItem<MainMenuItem> settingsMenuItem;
        private LabelMenuItem<MainMenuItem> helpMenuItem;
        private LabelMenuItem<MainMenuItem> creditsMenuItem;
        private LabelMenuItem<MainMenuItem> exitMenuItem;

        public ICommand NewGameCommand
        {
            get => newGameMenuItem.Command;
            set => newGameMenuItem.Command = value;
        }

        public ICommand ContinueLastGameCommand
        {
            get => continueLastGameMenuItem.Command;
            set => continueLastGameMenuItem.Command = value;
        }

        public ICommand ResumeGameCommand
        {
            get => resumeGameMenuItem.Command;
            set => resumeGameMenuItem.Command = value;
        }

        public ICommand SaveCommand
        {
            get => saveMenuItem.Command;
            set => saveMenuItem.Command = value;
        }

        public ICommand LoadCommand
        {
            get => loadMenuItem.Command;
            set => loadMenuItem.Command = value;
        }

        public ICommand SettingsCommand
        {
            get => settingsMenuItem.Command;
            set => settingsMenuItem.Command = value;
        }

        public ICommand HelpCommand
        {
            get => helpMenuItem.Command;
            set => helpMenuItem.Command = value;
        }

        public ICommand CreditsCommand
        {
            get => creditsMenuItem.Command;
            set => creditsMenuItem.Command = value;
        }

        public ICommand ExitCommand
        {
            get => exitMenuItem.Command;
            set => exitMenuItem.Command = value;
        }

        public MainMenuView(Audio audio)
            : base(audio)
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            newGameMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "New game",
                Value = MainMenuItem.NewGame,
                HorizontalAlign = HorizontalAlign.Center
            };

            continueLastGameMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Continue last game",
                Value = MainMenuItem.ContinueLastGame,
                //VisibilityProvider = () => !gameCradle.IsGameRunning && !string.IsNullOrEmpty(Settings.Default.LastSaveFileName),
                HorizontalAlign = HorizontalAlign.Center
            };

            resumeGameMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Resume game",
                Value = MainMenuItem.ResumeGame,
                HorizontalAlign = HorizontalAlign.Center,
                Key = ConsoleKey.Escape
            };

            saveMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Save game",
                Value = MainMenuItem.SaveGame,
                HorizontalAlign = HorizontalAlign.Center
            };

            loadMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Load game",
                Value = MainMenuItem.LoadGame,
                HorizontalAlign = HorizontalAlign.Center
            };

            settingsMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Settings",
                Value = MainMenuItem.Settings,
                HorizontalAlign = HorizontalAlign.Center
            };

            helpMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Help",
                Value = MainMenuItem.Help,
                HorizontalAlign = HorizontalAlign.Center
            };

            creditsMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Credits",
                Value = MainMenuItem.Credits,
                HorizontalAlign = HorizontalAlign.Center
            };

            exitMenuItem = new LabelMenuItem<MainMenuItem>
            {
                Text = "Exit",
                Value = MainMenuItem.Exit,
                HorizontalAlign = HorizontalAlign.Center
            };

            menu = new SelectableMenu<MainMenuItem>
            {
                newGameMenuItem,
                continueLastGameMenuItem,
                resumeGameMenuItem,
                saveMenuItem,
                loadMenuItem,

                new SpaceMenuItem<MainMenuItem>(),

                settingsMenuItem,
                helpMenuItem,
                creditsMenuItem,

                new SpaceMenuItem<MainMenuItem>(),

                exitMenuItem
            };

            menu.ItemsHorizontalAlign = HorizontalAlign.Center;

            menu.BeforeDisplay += HandleMenuBeforeDisplay;
            menu.AfterClose += HandleMenuAfterClose;

            separator = new Separator
            {
                ForegroundColor = DefaultTheme.Instance.DefaultColor
            };
        }

        private void HandleMenuBeforeDisplay(object sender, EventArgs e)
        {
            separator.Display();
        }

        private void HandleMenuAfterClose(object sender, EventArgs e)
        {
            separator.Display();
        }

        public void Display()
        {
            PlayBackgroundSound("menu-background.mp3", () =>
            {
                menu.Display();
            });
        }
    }
}