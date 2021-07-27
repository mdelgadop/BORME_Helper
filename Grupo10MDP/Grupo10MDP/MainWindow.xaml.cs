using Grupo10MDP.ViewModels;
using Grupo10MDP.ViewModels.Base;
using System;
using System.Windows;

namespace Grupo10MDP
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel myViewModel = new MainWindowViewModel();
            myViewModel.MessageBoxRequest += new EventHandler<MvvmMessageBoxEventArgs>(OpenMessageBoxRequest);

            this.DataContext = myViewModel;
        }

        private void OpenMessageBoxRequest(object sender, MvvmMessageBoxEventArgs e)
        {
            e.Show();
        }
    }
}
