
using Realms;
using System;
using System.Windows.Input;
using Wtd.PCL.Models;
using Wtd.PCL.Srevices;
using Xamarin.Forms;

namespace Wtd.PCL.ViewModels
{
    
    public class EditJobViewModel
    {
        private readonly Realm _realm;

        public Job Job { get; }

        public EditJobViewModel (Job job)
        {             
            Job = job;
        }          
    }
}
