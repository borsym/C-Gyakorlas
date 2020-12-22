using FoxAndChees.Model;
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

namespace FoxAndChees
{
    public partial class FoxView : Form
    {
        private FoxAndCheesModel _model; // játékmodell
        private Button[,] _buttonGrid; // gombrács
        private Timer _timer; // időzítő
        public FoxView()
        {
            InitializeComponent();
        }

        private void Game_GameAdvanced(Object sender, FoxAndCheesEventArgs e)
        {
            _foxLife.Text = e.FoxLife.ToString();
            _timeLabel.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");
            _eatenChees.Text = e.EatenChees.ToString();
        }

        
        private void Game_GameOver(Object sender, FoxAndCheesEventArgs e)
        {
            _timer.Stop();
            MessageBox.Show("Vege a játéknak, !" + Environment.NewLine +
                                "Összesen " + e.EatenChees + " sajtot ettél meg és " +
                                TimeSpan.FromSeconds(e.GameTime).ToString("g") + " ideig játszottál.",
                                "Fox and Chees játékkal",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);

        }

        private void ButtonGrid_MouseClick(Object sender, MouseEventArgs e)
        {
            // a TabIndex-ből megkapjuk a sort és oszlopot
            Debug.Write("asd");
            Int32 x = ((sender as Button).TabIndex - 100) / _model.Table.Size;
            Int32 y = ((sender as Button).TabIndex - 100) % _model.Table.Size;

            //_model.Step(x, y); // lépés a játékban

            // mező frissítése
            if (_model.Table.IsEmpty(x, y))
                _buttonGrid[x, y].Text = String.Empty;
            else
                _buttonGrid[x, y].Text = _model.Table[x, y].ToString();
        }

        private void deleteTable()
        {
            for (Int32 i = 0; i < _model.Table.Size; i++)
                for (Int32 j = 0; j < _model.Table.Size; j++) 
                    Controls.Remove(_buttonGrid[i, j]);
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
                    {
                        _buttonGrid[i, j].Enabled = false; // gomb kikapcsolása
                        _buttonGrid[i, j].BackColor = Color.White;
                    }
                }
            }
        }

        private void refreshTable()
        {
            for (Int32 i = 0; i < _model.Table.Size; i++)
            {
                for (Int32 j = 0; j < _model.Table.Size; j++)
                {
                    switch (_model.Table.GetValue(i, j))
                    {
                        case 0:
                            _buttonGrid[i, j].BackColor = Color.White;
                            break;
                        case 1:
                            _buttonGrid[i, j].BackColor = Color.Red;
                            break;
                        case 2:
                            _buttonGrid[i, j].BackColor = Color.Orange;
                            break;
                    }
                }
            }

        }
        private void Timer_Tick(Object sender, EventArgs e)
        {
            refreshTable();
            _model.AdvanceTime(); // játék 
        }

        private void FoxView_Load(object sender, EventArgs e)
        {
            // adatelérés példányosítása
            // modell létrehozása és az eseménykezelők társítása
            _model = new FoxAndCheesModel();
            _model.GameAdvanced += new EventHandler<FoxAndCheesEventArgs>(Game_GameAdvanced);
            _model.GameOver += new EventHandler<FoxAndCheesEventArgs>(Game_GameOver);
           

            // időzítő létrehozása
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += new EventHandler(Timer_Tick);    

            // játéktábla és menük inicializálása
            GenerateTable();
            //SetupMenus();

            // új játék indítása
            _model.NewGame();
            SetupTable();

            _timer.Start();
        }

        private void FoxView_KeyPress(object sender, KeyPressEventArgs e)
        {
             _model.Step(e.KeyChar);
            refreshTable();


        }

        private void FoxView_MouseClick(object sender, MouseEventArgs e)
        {
            Debug.Write(e.X + "\n");
        }

        private void NewGame_Click(object sender, EventArgs e) 
        {
            deleteTable();
            _model.NewGame(_model.Table.Size);
            GenerateTable();
            SetupTable();
            refreshTable();
            _timer.Start();
        }

        private void NewGame10_Click(object sender, EventArgs e)
        {
            deleteTable();
            _model.NewGame(10);
            GenerateTable();
            SetupTable();
            
            refreshTable();
            _timer.Start();
        }

        private void NewGame8_Click(object sender, EventArgs e)
        {

            deleteTable();
            _model.NewGame(8);
            GenerateTable();
            SetupTable();
            refreshTable();
            _timer.Start();
        }

        private void NewGame6_Click(object sender, EventArgs e)
        {
            deleteTable();
            _model.NewGame(6);
            GenerateTable();
            SetupTable();
            refreshTable();
            _timer.Start();
        }
    }
}
