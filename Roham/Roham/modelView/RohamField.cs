using System;
using System.Collections.Generic;
using System.Text;

namespace Roham.modelView
{
    //ez meg minek?
    class RohamField : ViewModelBase
    {
        private String _text;
        private Int32 _number;
        private Int32 _type;
        public Int32 Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }

        public Int32 Number
        {
            get { return _number; }
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged();
                }
            }
        }
        //asszem itt atext nem kell
        public String Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        //x,y beállítása
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public DelegateCommand StepCommand { get; set; }


    }
}
