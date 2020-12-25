using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using TamadasWPF.Model;

namespace TamadasWPF.ViewModel
{
    class TableViewModel : ViewModelBase
    {
        private TamadasModel _model;

        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand NewGameCommand4 { get; private set; }
        public DelegateCommand NewGameCommand6 { get; private set; }
        public DelegateCommand NewGameCommand8 { get; private set; }

        public ObservableCollection<TamadasField> Fields { get; set; }

        public Int32 GridSize { get; private set; }
        public Int32 RoundGame { get { return _model.RoundGame; } }
        public Int32 Player { get { return _model.Player; } }
        public String GameTime { get { return TimeSpan.FromSeconds(_model.GameTime).ToString("g"); } }

        public event EventHandler<int> NewGame;

        public TableViewModel(TamadasModel model)
        {
            GridSize = 6;
            // játék csatlakoztatása
            _model = model;
            _model.GameAdvanced += new EventHandler<TamadasEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<TamadasEventArgs>(Model_GameOver);
            _model.GameCreated += new EventHandler<TamadasEventArgs>(Model_GameCreated);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => OnNewGame(GridSize));
            NewGameCommand4 = new DelegateCommand(param => OnNewGame(4));
            NewGameCommand6 = new DelegateCommand(param => OnNewGame(6));
            NewGameCommand8 = new DelegateCommand(param => OnNewGame(8));

            // játéktábla létrehozása
            Fields = new ObservableCollection<TamadasField>();
            for (Int32 i = 0; i < _model.Table.Size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new TamadasField
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
            foreach (TamadasField field in Fields) // inicializálni kell a mezőket is
            {
                //field.Text = !_model.Table.IsEmpty(field.X, field.Y)/* && _model.Table[field.X, field.Y] > 4)*/ ? _model.Table[field.X, field.Y].ToString() : String.Empty;
               
                    switch (_model.Table[field.X, field.Y])
                    { 
                        case 1:
                            field.Text = "1";
                            break;
                        case 2:
                            field.Text = "2";
                            break;
                        case 3:
                            field.Text = "3";
                            break;
                        case 4:
                            field.Text = "4";
                            break;
                        case 5:
                            field.Text = "1";
                            break;
                        case 6:
                            field.Text = "2";
                            break;
                        case 7:
                            field.Text = "3";
                            break;
                        case 8:
                            field.Text = "4";
                            break;
                        default:
                            field.Text = String.Empty;
                            break;
                    }
                
               
                field.Type = _model.Table[field.X, field.Y];
            }

            OnPropertyChanged("GameTime");
            OnPropertyChanged("RoundGame");
            OnPropertyChanged("Player");
        }

        private void StepGame(int index)
        {
            TamadasField field = Fields[index];
            Debug.Write("X:"+field.X + " Y:" + field.Y + " type:" + field.Type +" round:" + RoundGame + "\n");
            // teljes atírá lesz itt
            _model.Step(field.X, field.Y, RoundGame);

           
            RefreshTable();
            

            OnPropertyChanged("RoundGame");
            OnPropertyChanged("Player"); // jelezzük a lépésszám változást

            
        }

        private void OnNewGame(int size)
        {
            Fields.Clear();
            GridSize = size;
            OnPropertyChanged("GridSize");
            if (NewGame != null)
                NewGame(this, size);
            for (Int32 i = 0; i < size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < size; j++)
                {
                    Fields.Add(new TamadasField
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

        private void Model_GameAdvanced(object sender, TamadasEventArgs e)
        {
            OnPropertyChanged("GameTime");
        }

        private void Model_GameOver(object sender, TamadasEventArgs e)
        {
            
        }

        private void Model_GameCreated(object sender, TamadasEventArgs e)
        {
            RefreshTable();
        }
    }
}
