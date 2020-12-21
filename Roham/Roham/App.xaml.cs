using Roham.model;
using Roham.modelView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Roham
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private RohamModel _model;
        private RohamViewModel _viewModel;
        private MainWindow _view;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
            
        }
        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new RohamModel();
            _model.GameOver += _model_GameOver;
            _model.NewGame(8);

            _viewModel = new RohamViewModel(_model);
            _viewModel.NewGame += _viewModel_NewGame;


            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += _view_Closing;
            _view.Show();

        }

        private void _view_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Biztos, hogy ki akar lépni?", "Roham", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true; // töröljük a bezárást
            }
        }

        private void _viewModel_NewGame(object sender, int e)
        {
            _model.NewGame(e);
        }

        private void _model_GameOver(object sender, RohamEventArgs e)
        {
            
            MessageBox.Show(e.Player == 3 ? "Game over, tie" : "Game over, the winner is" + e.Player);
            _model.NewGame(_viewModel.GridSize);
        }
    }
}
