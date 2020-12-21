using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using whack_a_mole.Model;

namespace whack_a_mole.ModelView
{
    class WhackAMoleViewModel : ViewModelBase
    {
        private WhackAMoleModel _model;
        public DelegateCommand NewGameCommand { get; private set; }
        public ObservableCollection<WhackAMoleField> Fields { get; set; }
        public String GameTime { get { return TimeSpan.FromSeconds(_model.GameTime).ToString("g"); } }
        public Int32 GamePointsCount { get { return _model.GamePointsCount; } }

        public event EventHandler NewGame;


        public WhackAMoleViewModel(WhackAMoleModel model)
        {
            // játék csatlakoztatása
            _model = model;
            _model.GameAdvanced += new EventHandler<WhackAMoleEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<WhackAMoleEventArgs>(Model_GameOver);
            _model.GameCreated += new EventHandler<WhackAMoleEventArgs>(Model_GameCreated);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => OnNewGame());

            // játéktábla létrehozása
            Fields = new ObservableCollection<WhackAMoleField>();
            for (Int32 i = 0; i < _model.Table.Size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new WhackAMoleField
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

        public void FieldChange()
        {
            foreach (WhackAMoleField field in Fields) // inicializálni kell a mezőket is
            {
                field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
                field.Type = _model.Table[field.X, field.Y];
               // Debug.Write(_model.Table[field.X, field.Y] + " ");
            }
        }
        private void RefreshTable()
        {
            foreach (WhackAMoleField field in Fields) // inicializálni kell a mezőket is
            {
                field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
                field.Type = _model.Table[field.X, field.Y];
                //Debug.Write(_model.Table[field.X, field.Y] +" ");
            }

            OnPropertyChanged("GameTime");
        }

        private void StepGame(Int32 index)
        {
            WhackAMoleField field = Fields[index];
            Debug.Write(field.X + " " + field.Y + " " + field.Type + "\n");
            if (field.Type == 1) return;
            if (field.Type != 2) { _model.GamePointsCount--; OnPropertyChanged("GamePointsCount"); return; }
            _model.GamePointsCount++;
            _model.Step(field.X, field.Y);
            OnPropertyChanged("GamePointsCount");
            FieldChange();
            //
            //field.Text = _model.Table[field.X, field.Y] > 0 ? _model.Table[field.X, field.Y].ToString() : String.Empty; // visszaírjuk a szöveget
            // OnPropertyChanged("GameStepCount"); // jelezzük a lépésszám változást es egy onProperty a pálya adott részére?
            //
            //field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
        }

        private void Model_GameAdvanced(object sender, WhackAMoleEventArgs e)
        {
            OnPropertyChanged("GameTime");
        }

        private void Model_GameOver(object sender, WhackAMoleEventArgs e)
        {
            OnPropertyChanged("GameTime");
            OnPropertyChanged("GamePointsCount");
        }

        private void Model_GameCreated(object sender, WhackAMoleEventArgs e)
        {
            RefreshTable();
        }

        private void OnNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
            OnPropertyChanged("GamePointsCount");
        }
    }
}
