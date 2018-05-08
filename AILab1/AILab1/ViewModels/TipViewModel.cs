using AILab1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AILab1.ViewModels
{
    class TipViewModel : INotifyPropertyChanged
    {
        float staff, food;
        float output;

        public event PropertyChangedEventHandler PropertyChanged;

        MamdaniSystem m;

        public float Staff
        {
            set
            {
                if (staff != value)
                {
                    staff = value;
                    OnPropertyChanged("Staff");
                    Calculate();
                }
            }
            get
            {
                return staff;
            }
        }

        public float Food
        {
            set
            {
                if (food != value)
                {
                    food = value;
                    OnPropertyChanged("Food");
                    Calculate();
                }
            }
            get
            {
                return food;
            }
        }

        public float Output
        {
            set
            {
                if (output != value)
                {
                    output = value;
                    OnPropertyChanged("Output");
                }
            }
            get
            {
                return output;
            }
        }

        public TipViewModel()
        {
            m = new MamdaniSystem();
        }

        void Calculate()
        {
            Output = m.ProcessRules(Staff, Food);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
