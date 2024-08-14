using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Zavkr
    {
        public int DepartmentId { get; set; }

        public long PositionalNumber {  get; set; }
        public string LoanReferenceName { get; set; }

        public string Title { get; set; }
        public string Inn { get; set; }
        public string ZV_DATE { get; set; }
        public string ZV_SUM { get; set; }
        private bool isSecretHidden;
        public bool IsSecretHidden
        {
            get => isSecretHidden;
            set
            {
                if (isSecretHidden != value)
                {
                    isSecretHidden = value;
                    OnPropertyChanged(nameof(IsSecretHidden));
                    OnPropertyChanged(nameof(DisplaySecret));
                }
            }
        }

        public string DisplaySecret => IsSecretHidden ? new string('*', ZV_SUM.Length) : ZV_SUM;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
