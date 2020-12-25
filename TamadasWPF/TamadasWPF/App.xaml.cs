using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TamadasWPF.Model;
using TamadasWPF.ViewModel;

namespace TamadasWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TamadasModel _model;
        private TableViewModel _viewModel;
        private MainWindow _view;
        private DispatcherTimer _timer;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }
        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell létrehozása
            _model = new TamadasModel();
            _model.GameOver += new EventHandler<TamadasEventArgs>(Model_GameOver);
            _model.NewGame(6);

            // nézemodell létrehozása
            _viewModel = new SudokuViewModel(_model);
            _viewModel.NewGame += new EventHandler<int>(ViewModel_NewGame);
          
            // nézet létrehozása
            _view = new MainWindow();
            _view.DataContext = _viewModel;
          //  _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing); // eseménykezelés a bezáráshoz
            _view.Show();

            // időzítő létrehozása
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.AdvanceTime();
        }

        private void ViewModel_NewGame(object sender, int e)
        {
            _model.NewGame(e);
            _timer.Start();
        }

        private void Model_GameOver(object sender, TamadasEventArgs e)
        {
            _timer.Stop();
            MessageBox.Show(e.Player + " nyert");
            _model.NewGame(_viewModel.GridSize);
            _timer.Start();
        }
    }
}
