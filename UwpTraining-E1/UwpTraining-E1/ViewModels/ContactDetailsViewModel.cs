using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpTraining_E1.ViewModels.Base;

namespace UwpTraining_E1.ViewModels
{
    public class ContactDetailsViewModel : BindableBase
    {
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set 
            {
                this.SetProperty(ref firstName, value);
                this.OnPropertyChanged(nameof(Summary));
            }
        }


        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set 
            {
                this.SetProperty(ref lastName, value);
                this.OnPropertyChanged(string.Empty);
            }
        }

        public ObservableCollection<string> FavouriteColors { get; set; }

        public string Summary => $"{FirstName} {LastName}";
    }
}
