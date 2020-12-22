using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using TiliToli.Model;

namespace TiliToli.ViewModel
{
    class TiliToliViewModel : ViewModelBase
    {
        private TiliToliModel _model;
        public Int32 GridSize { get; private set; }

        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand NewGameCommand3 { get; private set; }
        public DelegateCommand NewGameCommand4 { get; private set; }
        public DelegateCommand NewGameCommand5 { get; private set; }
        // itt lesznek a pályaméretek?

        public ObservableCollection<TiliToliField> Fields { get; set; }
        public String GameTime { get { return TimeSpan.FromSeconds(_model.GameTime).ToString("g"); } }


        public event EventHandler<int> NewGame;


        public TiliToliViewModel(TiliToliModel model)
        {
            // játék csatlakoztatása
            GridSize = 5;
            _model = model;
            _model.GameAdvanced += new EventHandler<TiliToliEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<TiliToliEventArgs>(Model_GameOver);
            _model.GameCreated += new EventHandler<TiliToliEventArgs>(Model_GameCreated);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => OnNewGame(GridSize));
            NewGameCommand3 = new DelegateCommand(param => OnNewGame(3));
            NewGameCommand4 = new DelegateCommand(param => OnNewGame(4));
            NewGameCommand5 = new DelegateCommand(param => OnNewGame(5));
            // játéktábla létrehozása
            Fields = new ObservableCollection<TiliToliField>();
            
            for (Int32 i = 0; i < _model.Table.Size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new TiliToliField
                    {
                        Text = String.Empty,
                        X = i,
                        Y = j,
                        Number = i * _model.Table.Size + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                        // ha egy mezőre léptek, akkor jelezzük a léptetést, változtatjuk a lépésszámot
                    });
                }
            }


            RefreshTable();
        }

        private void RefreshTable()
        {
            foreach (TiliToliField field in Fields) // inicializálni kell a mezőket is
            {
              //  Debug.Write(_model.Table[field.X, field.Y] + "\n");
                field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? (_model.Table[field.X, field.Y] == -1 ? String.Empty : _model.Table[field.X, field.Y].ToString()) : String.Empty;
            }

            OnPropertyChanged("GameTime");
        }

        private void StepGame(int index)  //ez így hibás
        {
            TiliToliField field = Fields[index];
            

            _model.Step(field.X, field.Y); //lehet hogy itt nézem meg hogy betudom e tolni jajaja ez lesz az

            
            //OnPropertyChanged("GameStepCount"); // jelezzük a lépésszám változást
            RefreshTable();


        }

        private void OnNewGame(int size)
        {
            
            Fields.Clear();
            GridSize = size;
            OnPropertyChanged("GridSize");
            NewGame?.Invoke(this, size);

            for (Int32 i = 0; i < size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < size; j++)
                {
                    Fields.Add(new TiliToliField
                    {
                        Text = String.Empty,
                        X = i,
                        Y = j,
                        Number = i * size + j,
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                    });
                }
            }
            RefreshTable();
        }

        private void Model_GameAdvanced(object sender, TiliToliEventArgs e)
        {
            OnPropertyChanged("GameTime");
        }

        private void Model_GameOver(object sender, TiliToliEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Model_GameCreated(object sender, TiliToliEventArgs e)
        {
            RefreshTable();
        }
    }
}
