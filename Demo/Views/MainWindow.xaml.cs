using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using ConfigData = Demo.Properties;

namespace Demo.Views
{
    public partial class MainWindow : Window
    {
        #region 定数
        private const double ICON_SIZE = 25;
        private readonly PackIcon WINDOW_RESTORE_ICON = new PackIcon { Kind = PackIconKind.WindowRestore, Height = ICON_SIZE, Width = ICON_SIZE };
        private readonly PackIcon WINDOW_MAXYMIZE_ICON = new PackIcon { Kind = PackIconKind.WindowMaximize, Height = ICON_SIZE, Width = ICON_SIZE };
        #endregion

        #region 初期化処理/終了処理
        public MainWindow()
        {
            InitializeComponent();

            this.Height = ConfigData.Settings.Default.MainWindowHeight;
            this.Width = ConfigData.Settings.Default.MainWindowWidth;
            this.Top = ConfigData.Settings.Default.MainWindowTop;
            this.Left = ConfigData.Settings.Default.MainWindowLeft;
            this.WindowState = (System.Windows.WindowState)ConfigData.Settings.Default.MainWindowState;
        }

        private void onWindowLoaded(object sender, RoutedEventArgs e)
        {
            var isNormal = this.WindowState == WindowState.Maximized;
            this.MaximizeOrRestoreButton.Content = isNormal ? this.WINDOW_RESTORE_ICON : this.WINDOW_MAXYMIZE_ICON;
        }

        private void onWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ConfigData.Settings.Default.Save();
        }
        #endregion

        #region ボタンクリック時処理
        private void onMinimizeButtonClicked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void onMaximizeOrRestoreButtonClicked(object sender, RoutedEventArgs e)
        {
            var isNormal = this.WindowState == WindowState.Normal;
            this.WindowState = isNormal ? WindowState.Maximized : WindowState.Normal;
        }

        private void onWindowStateChanged(object sender, EventArgs e)
        {
            var isNormal = this.WindowState == WindowState.Maximized;
            this.MaximizeOrRestoreButton.Content = isNormal ? this.WINDOW_RESTORE_ICON : this.WINDOW_MAXYMIZE_ICON;
            ConfigData.Settings.Default.MainWindowState = (int)this.WindowState;
        }

        private void onPowerButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region プロパティ変更時処理
        private void onWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                ConfigData.Settings.Default.MainWindowHeight = this.Height;
                ConfigData.Settings.Default.MainWindowWidth = this.Width;
            }
        }

        private void onWindowLocationChanged(object sender, EventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                ConfigData.Settings.Default.MainWindowTop = this.Top;
                ConfigData.Settings.Default.MainWindowLeft = this.Left;
            }
        }
        #endregion
    }
}
