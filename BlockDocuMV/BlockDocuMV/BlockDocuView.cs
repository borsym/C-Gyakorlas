using BlockDocuMV.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockDocuMV
{
    public partial class BlockDocuView : Form
    {
        private BlockDocuModel _model; // játékmodell
        private Button[,] _buttonGrid; // gombrács
        private Timer _timer; // időzítő
        public BlockDocuView()
        {
            InitializeComponent();
        }

        private void BlockDocuView_Load(object sender, EventArgs e)
        {
            _model = new BlockDocuModel();
            _model.GameAdvanced += new EventHandler<BlockDocuEventArgs>(Game_GameAdvanced);
            _model.GameOver += new EventHandler<BlockDocuEventArgs>(Game_GameOver);

            // időzítő létrehozása
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(Timer_Tick);

            // játéktábla és menük inicializálása
            GenerateTable();

            // új játék indítása
            _model.NewGame();
            SetupTable();

            _timer.Start();
        }


        private void GenerateTable()
        {
            _buttonGrid = new Button[_model.Table.Size, _model.Table.Size];
            for (Int32 i = 0; i < _model.Table.Size; i++)
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    _buttonGrid[i, j] = new Button();
                    _buttonGrid[i, j].Location = new Point(5 + 50 * j, 35 + 50 * i); // elhelyezkedés
                    _buttonGrid[i, j].Size = new Size(50, 50); // méret
                    _buttonGrid[i, j].Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold); // betűtípus
                    _buttonGrid[i, j].Enabled = false; // kikapcsolt állapot
                    _buttonGrid[i, j].TabIndex = 100 + i * _model.Table.Size + j; // a gomb számát a TabIndex-ben tároljuk
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat; // lapított stípus
                    _buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonGrid_MouseClick);
                    // közös eseménykezelő hozzárendelése minden gombhoz

                    Controls.Add(_buttonGrid[i, j]);
                    // felvesszük az ablakra a gombot
                }
        }
        private void SetupTable()
        {
            for (Int32 i = 0; i < _buttonGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < _buttonGrid.GetLength(1); j++)
                {
                    _buttonGrid[i, j].Text = _model.Table.IsEmpty(i, j) ? String.Empty : _model.Table[i, j].ToString();
                    if (_model.Table[i, j] == 2) 
                    { 
                        _buttonGrid[i, j].BackColor = Color.Black; _buttonGrid[i, j].Enabled = false; 
                    }
                    else if (_model.Table[i, j] == 1)
                    { 
                        _buttonGrid[i, j].BackColor = Color.Blue; _buttonGrid[i, j].Enabled = false; 
                    }
                    else 
                    {
                        _buttonGrid[i, j].BackColor = Color.White; _buttonGrid[i, j].Enabled = true;
                    }
                    
                }
            }

            _timeLabel.Text = TimeSpan.FromSeconds(_model.GameTime).ToString("g");
        }

        private void ButtonGrid_MouseClick(object sender, MouseEventArgs e)
        {
            Int32 x = ((sender as Button).TabIndex - 100) / _model.Table.Size;
            Int32 y = ((sender as Button).TabIndex - 100) % _model.Table.Size;
            int i = x;
            int j = y;
            if ((i == 4 && j == 5) || (i == 4 && j == 4) || (i == 5 && j == 5) || (i == 5 && j == 4)) return;
            if (((i == 0 || i == 1 || i == 2 || i == 3) && (j == 4 || j == 5)) || ((i == 4 || i == 5) && (j == 0 || j == 1 || j == 2 || j == 3))) return;
            Debug.Write(i + " " + j + "\n");

            _model.Step(i, j);
            _model.Check();

            SetupTable();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _model.AdvanceTime();
        }

        private void Game_GameOver(object sender, BlockDocuEventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("ugyi vagy");
        }

        private void Game_GameAdvanced(object sender, BlockDocuEventArgs e)
        {
            _timeLabel.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");
        }

        private void _newGame_Click(object sender, System.EventArgs e)
        {
            _model.NewGame();
            _timer.Start();
            SetupTable();
        }
    }
}
