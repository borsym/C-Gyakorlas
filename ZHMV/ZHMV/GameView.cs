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
using ZHMV.Model;

namespace ZHMV
{
    public partial class GameView : Form
    {
        private ModelMV _model; 
        private Button[,] _buttonGrid; 
        private Timer _timer;
        private bool isStoped;
        public GameView()
        {
            InitializeComponent();
        }
        private void GameView_Load(object sender, EventArgs e)
        {
            // modell létrehozása és az eseménykezelők társítása
            _model = new ModelMV();
            _model.GameAdvanced += new EventHandler<ModelEventArgs>(Game_GameAdvanced);
            _model.GameOver += new EventHandler<ModelEventArgs>(Game_GameOver);

            // időzítő létrehozása
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(Timer_Tick);

            // játéktábla és menük inicializálása
            _model.NewGame(12);
            GenerateTable();
            SetupTable();
            refreshTable();
            MessageBox.Show("Kérlek indíts egy játékot!");
           // _timer.Start();
        }

        private void Game_GameAdvanced(Object sender, ModelEventArgs e)
        {
                _LabelTime.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");
                _LabelStep.Text = e.GameStepCount.ToString();
            
        }

        private void Game_GameOver(Object sender, ModelEventArgs e)
        {
            _timer.Stop();

            foreach (Button button in _buttonGrid) // kikapcsoljuk a gombokat
                button.Enabled = false;


            MessageBox.Show("Vege a játéknak!" + Environment.NewLine +
                            "Összesen " + e.GameStepCount + " lépést tettél meg és " +
                            TimeSpan.FromSeconds(e.GameTime).ToString("g") + " ideig játszottál.",
                            "Game",
                            MessageBoxButtons.OK);
           
        }

     

        private void DeleteTable()
        {
            for (Int32 i = 0; i < _model.Table.Size; i++) 
            { 
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    Controls.Remove(_buttonGrid[i, j]);
                }
            }
        }

        private void Pause_Game(object sender, EventArgs e)
        {
            if(isStoped)
            {
                _timer.Start();
                isStoped = false;
            }
            else
            {
                isStoped = true;
                _timer.Stop();
            }
        
        }
        private void MenuFileNewGame_Click(Object sender, EventArgs e)
        {
            isStoped = false;
            DeleteTable();
            _model.NewGame(_model.CurrentSize);
            GenerateTable();
            SetupTable();

            _timer.Start();
        }


        private void MenuFileNewGame6_Click(Object sender, EventArgs e)
        {
            DeleteTable();
            _model.NewGame(6);
            GenerateTable();
            SetupTable();

            _timer.Start();
        }

        private void MenuFileNewGame8_Click(Object sender, EventArgs e)
        {
            DeleteTable();
            _model.NewGame(8);
            GenerateTable();
            SetupTable();

            _timer.Start();
        }

        private void MenuFileNewGame10_Click(Object sender, EventArgs e)
        {
            DeleteTable();
            _model.NewGame(10);
            GenerateTable();
            SetupTable();

            _timer.Start();
        }

        private void Timer_Tick(Object sender, EventArgs e)
        {
            _model.AdvanceTime(); // játék léptetése
            refreshTable();
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
                    _buttonGrid[i, j].Enabled = false; // kikapcsolt állapot
                    _buttonGrid[i, j].TabIndex = 100 + i * _model.Table.Size + j; // a gomb számát a TabIndex-ben tároljuk
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat; // lapított stípus
                   // _buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonGrid_MouseClick);
                    // közös eseménykezelő hozzárendelése minden gombhoz

                    Controls.Add(_buttonGrid[i, j]);
                    // felvesszük az ablakra a gombot
                }
        }

        private void refreshTable()
        {
            for (Int32 i = 0; i < _model.Table.Size; i++)
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    _buttonGrid[i, j].Text = _model.Table.IsEmpty(i, j)
                       ? String.Empty
                       : _model.Table[i, j].ToString();
                    switch (_model.Table.GetValue(i, j))
                    {
                        case 0:
                            _buttonGrid[i, j].BackColor = Color.White;
                            break;
                        case 1:
                            _buttonGrid[i, j].BackColor = Color.Yellow;
                            break;
                        case 2:
                            _buttonGrid[i, j].BackColor = Color.Red;
                            break;
                        case 3:
                            _buttonGrid[i, j].BackColor = Color.Black;
                            break;
                        case 4:
                            _buttonGrid[i, j].BackColor = Color.Blue;
                            break;
                    }
                }
            }

        }
        private void SetupTable()
        {
            for (Int32 i = 0; i < _buttonGrid.GetLength(0); i++)
            {
                for (Int32 j = 0; j < _buttonGrid.GetLength(1); j++)
                {
                    _buttonGrid[i, j].Text = _model.Table.IsEmpty(i, j)
                        ? String.Empty
                        : _model.Table[i, j].ToString();
                    _buttonGrid[i, j].Enabled = true;
                    _buttonGrid[i, j].BackColor = Color.White;

                }
            }

            _LabelTime.Text = TimeSpan.FromSeconds(_model.GameTime).ToString("g");
            _LabelStep.Text = _model.PointCount.ToString();
            refreshTable();
        }

        private void GameView_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.Write(e.KeyCode + "\n");

            if(e.KeyCode == Keys.A)
            {
                _model.movePackMan(0,-1);
            }
            else if(e.KeyCode == Keys.D)
            {
                _model.movePackMan(0, 1);
            }
            else if (e.KeyCode == Keys.S)
            {
                _model.movePackMan(1, 0);
            }
            else if (e.KeyCode == Keys.W)
            {
                _model.movePackMan(-1, 0);
            }

            refreshTable();
        }
    }
}
