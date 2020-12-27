using amongus.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace amongus.ViewModel
{
    class AmongUsViewModel : ViewModelBase
    {
        private AmongUsModel _model;
        public DelegateCommand NewGameCommand { get; private set; }

        public ObservableCollection<AmongUsField> Fields { get; set; }

        public String GameTime { get { return TimeSpan.FromSeconds(_model.GameTime).ToString("g"); } }
        public event EventHandler NewGame;

        public AmongUsViewModel(AmongUsModel model)
        {
            // játék csatlakoztatása
            _model = model;
            _model.GameAdvanced += new EventHandler<AmongUsModelEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<AmongUsModelEventArgs>(Model_GameOver);
            _model.GameCreated += new EventHandler<AmongUsModelEventArgs>(Model_GameCreated);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => OnNewGame());
            // játéktábla létrehozása
            Fields = new ObservableCollection<AmongUsField>();
            for (Int32 i = 0; i < _model.Table.Size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new AmongUsField
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

        private void Model_GameAdvanced(object sender, AmongUsModelEventArgs e)
        {
            OnPropertyChanged("GameTime");
            RefreshTable();
        }

        private void Model_GameOver(object sender, AmongUsModelEventArgs e)
        {
            Debug.Write("vege");
        }

        private void Model_GameCreated(object sender, AmongUsModelEventArgs e)
        {
            RefreshTable();
        }

        private void RefreshTable()
        {
            foreach (AmongUsField field in Fields) // inicializálni kell a mezőket is
            {
                //itt ki kell kérni a _table.positionból majd a végén és azokkal felülérni a maradék typet sztm
                //field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
                field.Type = _model.Table[field.X, field.Y];
                //_model.Table.positions[0].key; végigiterálok majd rajta
            }

            OnPropertyChanged("GameTime");
        }



        private void StepGame(int index)
        {
            AmongUsField field = Fields[index];

            //_model.Step(field.X, field.Y);

           // field.Text = _model.Table[field.X, field.Y] > 0 ? _model.Table[field.X, field.Y].ToString() : String.Empty; // visszaírjuk a szöveget
            OnPropertyChanged("GameStepCount"); // jelezzük a lépésszám változást
            
           // field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
        }

        private void OnNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
        }
    }
}
