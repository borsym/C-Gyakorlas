using BlockDocuWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace BlockDocuWPF.ViewModel
{
    class BlockDocuViewModel : ViewModelBase
    {
        private BlockDocuModel _model;
        public DelegateCommand NewGameCommand { get; private set; }
        public ObservableCollection<BlockDocuField> Fields { get; set; }
        public String GameTime { get { return TimeSpan.FromSeconds(_model.GameTime).ToString("g"); } }
        public event EventHandler NewGame;
        public BlockDocuViewModel(BlockDocuModel model)
        {
            // játék csatlakoztatása
            _model = model;
            _model.GameAdvanced += new EventHandler<BlockDocuEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<BlockDocuEventArgs>(Model_GameOver);
            _model.GameCreated += new EventHandler<BlockDocuEventArgs>(Model_GameCreated);

            // parancsok kezelése
            NewGameCommand = new DelegateCommand(param => OnNewGame());
            // játéktábla létrehozása
            Fields = new ObservableCollection<BlockDocuField>();
            for (Int32 i = 0; i < _model.Table.Size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new BlockDocuField
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
            
            foreach (BlockDocuField field in Fields) // inicializálni kell a mezőket is
            {
                field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
                field.Type = _model.Table[field.X, field.Y];
            }

            OnPropertyChanged("GameTime");
        }

        private void OnNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
        }

        private void StepGame(int index)
        {
            BlockDocuField field = Fields[index];
            int i = field.X;
            int j = field.Y;
            if ((i == 4 && j == 5) || (i == 4 && j == 4) || (i == 5 && j == 5) || (i == 5 && j == 4)) return;
            if (((i == 0 || i == 1 || i == 2 || i == 3) && (j == 4 || j == 5)) || ((i == 4 || i == 5) && (j == 0 || j == 1 || j == 2 || j == 3))) return;
            Debug.Write(i+ " " + j+"\n");

            _model.Step(field.X, field.Y);
            _model.Check();
            
            RefreshTable();
            //
            //field.Text = _model.Table[field.X, field.Y] > 0 ? _model.Table[field.X, field.Y].ToString() : String.Empty; // visszaírjuk a szöveget
            //
            //field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
        }

        private void Model_GameAdvanced(object sender, BlockDocuEventArgs e)
        {
            OnPropertyChanged("GameTime");
        }

        private void Model_GameOver(object sender, BlockDocuEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Model_GameCreated(object sender, BlockDocuEventArgs e)
        {
            RefreshTable();
        }
    }
}
