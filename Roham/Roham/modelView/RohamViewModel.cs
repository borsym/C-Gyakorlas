using Roham.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Roham.modelView
{
    class RohamViewModel : ViewModelBase
    {
        private RohamModel _model;
        private RohamField tmp;
        private Int32 tmpType;
        private Int32 skipCount = 0;

        //uj jatek kezdes parancs
        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand Balra { get; private set; }
        public DelegateCommand Jobbra { get; private set; }
        public DelegateCommand Hatra { get; private set; }
        public DelegateCommand Elore { get; private set; }
        public DelegateCommand Skipp { get; private set; }


        public DelegateCommand NewGame4 { get; private set; }
        public DelegateCommand NewGame6 { get; private set; }
        public DelegateCommand NewGame8 { get; private set; }

        //jatekmezo gyujtemeny lekérdezése
        public ObservableCollection<RohamField> Fields { get; set; }
        
        //jelenlegi jatekos
        public Int32 Player { get { return _model.Player; } } // private set;
        public Int32 GridSize { get; private set; }



        public Int32 FirstPlayerPuppet { get { return _model.FirstPlayerPuppetCount; }  }
        public Int32 SecondPlayerPuppet { get { return _model.SecondPlayerPuppetCount; } }

        public event EventHandler<int> NewGame;

        public RohamViewModel(RohamModel model)
        {
            _model = model;
            _model.GameAdvanced += _model_GameAdvanced;
            _model.GameOver += _model_GameOver;
            _model.GameCreated += _model_GameCreated;
            GridSize = 8;
            skipCount = 0;
            NewGameCommand = new DelegateCommand(param => OnNewGame(GridSize));

            Elore = new DelegateCommand(param => OnElore());
            Hatra = new DelegateCommand(param => OnHatra());
            Jobbra = new DelegateCommand(param => OnJobbra());
            Balra = new DelegateCommand(param => OnBalra());
            Skipp = new DelegateCommand(param => OnSkipp());

            NewGame4 = new DelegateCommand(param => OnNewGame(4));
            NewGame6 = new DelegateCommand(param => OnNewGame(6));
            NewGame8 = new DelegateCommand(param => OnNewGame(8));
           
            Fields = new ObservableCollection<RohamField>();
            for(Int32 i = 0; i < _model.Table.Size  ; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Fields.Add(new RohamField
                    {
                        Text = String.Empty,
                        X = i,
                        Y = j,
                        Number = i * _model.Table.Size + j,
                        StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                    });
                }
            }
            RefreshTable();
        }
        private void RefreshTable()
        {
            foreach (RohamField field in Fields) // inicializálni kell a mezőket is
            {
                field.Text = !_model.Table.IsEmpty(field.X, field.Y) ? _model.Table[field.X, field.Y].ToString() : String.Empty;
                field.Type = _model.Table[field.X, field.Y];
                Debug.Write(_model.Table[field.X, field.Y]+"\n");
                
            }
            
            OnPropertyChanged("Player");
            OnPropertyChanged("SecondPlayerPuppet");
            OnPropertyChanged("FirstPlayerPuppet");
        }

        //jatek leptetse esemenykivalt

        //paassszzz ez van a delegate commandbe ezt majd meg kell nézni jobban
        //kijelölök egy mezőt és aztán megmondom hogy merre akarok lépni

        private void _model_GameCreated(object sender, RohamEventArgs e)
        {
            RefreshTable();
        }

        private void _model_GameOver(object sender, RohamEventArgs e)
        {
            Debug.Write("vege");
        }

        

        private void _model_GameAdvanced(object sender, RohamEventArgs e)
        {
            OnPropertyChanged("Player");
            OnPropertyChanged("SecondPlayerPuppet");
            OnPropertyChanged("FirstPlayerPuppet");
            //igazábol ez felesleges mert ha lép a delikvens akkor így is ugy is frissíthetem
        }




        //4 gomb elérhetőek hogyha kattintott egy bábura majd léphet
        //itt az ütést hogy gondolja??
        private void StepGame(Int32 index)
        {

            RohamField field = Fields[index];
            Debug.Write(field.X + " " + field.Y + " " + field.Type + "\n");
            
            if (field.Type == 0 || _model.Player != field.Type) return;
            foreach (RohamField field2 in Fields)//  fogat bábu lesz xd
            {
                if (field2.Type == 4) return;
            }
            tmpType = field.Type;

            field.Type = 4;
            tmp = field;
            skipCount = 0;

        }

        private void OnElore()
        {
            if (tmp == null || tmp.X - 1 < 0 ) return;
            
            RohamField field = Fields[tmp.Number - _model.Table.Size];
            
            if (field.Type == tmpType) return;
            field.Type = tmpType;
            field.Text = tmp.Text;
            
            _model.StepClear(tmp.X, tmp.Y);
            _model.Step(field.X, field.Y);

            tmp.Type = 0;
            tmp.Text = String.Empty;
            tmp = null;

            Debug.Write("asd\n");
        }
        private void OnHatra()
        {
            if (tmp == null || tmp.X + 1 >= _model.Table.Size) return;
            
            RohamField field = Fields[tmp.Number + _model.Table.Size];
            if (field.Type == tmpType) return;
            field.Type = tmpType;
            field.Text = tmp.Text;

            _model.StepClear(tmp.X, tmp.Y);
            _model.Step(field.X, field.Y);
            tmp.Type = 0;
            tmp.Text = String.Empty;
            tmp = null;

        }
        private void OnBalra()
        {
            if (tmp == null || tmp.Y - 1 < 0 ) return;
            
            RohamField field = Fields[tmp.Number - 1];
            if (field.Type == tmpType) return;
            field.Type = tmpType;
            field.Text = tmp.Text;

            _model.StepClear(tmp.X, tmp.Y);
            _model.Step(field.X, field.Y);

            tmp.Type = 0;
            tmp.Text = String.Empty;
            tmp = null;
        }
        private void OnJobbra()
        {
            if (tmp == null || tmp.Y + 1 >= _model.Table.Size) return;
            
            RohamField field = Fields[tmp.Number + 1];
            if (field.Type == tmpType) return;
            field.Type = tmpType;
            field.Text = tmp.Text;

            _model.StepClear(tmp.X, tmp.Y);  // atallitom 0 ara a mezot
            _model.Step(field.X, field.Y);  // itt pedig beallítom a helyesre
            tmp.Type = 0;
            tmp.Text = String.Empty;
            tmp = null;
        }

        private void OnSkipp()
        {
            skipCount++;
            _model.Player = _model.Player % 2 + 1;
            
            Debug.Write("asd");
            if (skipCount == 2) { skipCount = 0; _model.OnGameOver(true,3); } //ezt nem hinném hogy így szabadna
            OnPropertyChanged("Player");

        }

        private void OnNewGame(int size)
        {
            skipCount = 0;
            Fields.Clear();
            GridSize = size;
            OnPropertyChanged("GridSize");
            NewGame?.Invoke(this, size);
            
            for (Int32 i = 0; i < size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < size; j++)
                {
                    Fields.Add(new RohamField
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
    }
}
