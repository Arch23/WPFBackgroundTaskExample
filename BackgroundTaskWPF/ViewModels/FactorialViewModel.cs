using System.ComponentModel;

namespace BackgroundTaskWPF.ViewModels
{
    public class FactorialViewModel : INotifyPropertyChanged
    {
        private string inputTxt;
        public string InputTxt 
        {
            get => inputTxt; 
            set
            {
                inputTxt = value;
                RaisePropertyChanged(nameof(InputTxt));
            }
        }

        private bool enableStart = true;
        public bool EnableStart
        {
            get => enableStart;
            set
            {
                enableStart = value;
                RaisePropertyChanged(nameof(EnableStart));
            }
        } 

        private bool enableCancel = false;
        public bool EnableCancel
        {
            get => enableCancel;
            set
            {
                enableCancel = value;
                RaisePropertyChanged(nameof(EnableCancel));
            }
        }
        private int progressPercentage;
        public int ProgressPercentage
        {
            get => progressPercentage;
            set
            {
                progressPercentage = value;
                RaisePropertyChanged(nameof(ProgressPercentage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
