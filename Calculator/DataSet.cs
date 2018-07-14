using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Calculator
{
    public class DataSet:INotifyPropertyChanged
    {

        private string m_DisplayText = "";
        public string DisplayText
        {
            get { return m_DisplayText; }
            set
            {
                m_DisplayText = value;
                OnPropertyChanged("DisplayText");
            }
        }

        private string m_SubDisplayText = "";
        public string SubDisplayText
        {
            get { return m_SubDisplayText;}
            set
            {
                m_SubDisplayText = value;
                OnPropertyChanged("SubDisplayText");

            }
        }

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventArgs args = new PropertyChangedEventArgs(PropertyName);
            if(PropertyChanged != null)
            {
                PropertyChanged(this, args);
            }
        }

        public void dataSet()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
