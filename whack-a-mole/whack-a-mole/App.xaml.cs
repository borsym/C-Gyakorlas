using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using whack_a_mole.Model;
using whack_a_mole.ModelView;

namespace whack_a_mole
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WhackAMoleModel _model;
        private WhackAMoleViewModel _viewModel;
        private MainWindow _view;
        private DispatcherTimer _timer;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell létrehozása
            _model = new WhackAMoleModel();
            _model.GameOver += new EventHandler<WhackAMoleEventArgs>(Model_GameOver);
            _model.NewGame();

            // nézemodell létrehozása
            _viewModel = new WhackAMoleViewModel(_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);

            // nézet létrehozása
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            //_view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing); // eseménykezelés a bezáráshoz
            _view.Show();

            // időzítő létrehozása
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        private void View_Closing(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Model_GameOver(object sender, WhackAMoleEventArgs e)
        {
            _timer.Stop();

            MessageBox.Show("Game over" + _model.GamePointsCount,
                            "Whack a mole",
                            MessageBoxButton.OK,
                            MessageBoxImage.Asterisk);
            _model.NewGame();
            _timer.Start();
            
 
        }

        private void ViewModel_ExitGame(object sender, EventArgs e)
        {
            _view.Close();
        }

        private void ViewModel_NewGame(object sender, EventArgs e)
        {
            _model.NewGame();
            _timer.Start();
            Debug.Write("asdasd"+_timer);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.AdvanceTime();
            _viewModel.FieldChange();
        }
    }
}
