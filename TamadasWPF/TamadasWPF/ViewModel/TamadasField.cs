using System;
using System.Collections.Generic;
using System.Text;

namespace TamadasWPF.ViewModel
{
    class TamadasField : ViewModelBase
    {
        private Int32 _type;
        private String _text;
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


        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Int32 Number { get; set; }
        public DelegateCommand StepCommand { get; set; }
    }
}
