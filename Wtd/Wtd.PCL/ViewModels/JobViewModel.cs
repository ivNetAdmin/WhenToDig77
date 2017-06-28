
using Realms;
using System.Collections.Generic;
using Xamarin.Forms;
using System;
using System.Windows.Input;
using Wtd.PCL.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Wtd.PCL.Srevices;

namespace Wtd.PCL.ViewModels
{
    internal class JobViewModel : BaseModel
    {
        private Realm _realm;
        private DateTimeOffset _currentDate;

        public JobViewModel()
        {
            //Realm.DeleteRealm(new RealmConfiguration());
            _realm = Realm.GetInstance();
            CalendarChangeCommand = new Command(CalendarChange);

            //SeedJobs();
            //JobList = _realm.All<Job>();

            _currentDate = DateTime.Today;
            _dateRangeDate = new ObservableCollection<string>();
            _dateRangeJobType = new ObservableCollection<string>();
            _dateRangeTextColour = new ObservableCollection<Color>();
            _dateRangeVisible = new ObservableCollection<Boolean>();

            SetDateRange();

        }      

        public Page CurrentPage { get; }

        //public IEnumerable<Job> JobList { get; private set; }

        private ObservableCollection<Job> _jobList;
        public ObservableCollection<Job> JobList
        {
            get { return _jobList; }
            set
            {
                _jobList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _dateRangeJobType;
        public ObservableCollection<String> DateRangeJobType
        {
            get { return _dateRangeJobType; }
            set
            {
                _dateRangeJobType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _dateRangeDate;
        public ObservableCollection<String> DateRangeDate
        {
            get { return _dateRangeDate; }
            set
            {
                _dateRangeDate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Color> _dateRangeTextColour;
        public ObservableCollection<Color> DateRangeTextColour
        {
            get { return _dateRangeTextColour; }
            set
            {
                _dateRangeTextColour = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Boolean> _dateRangeVisible;
        public ObservableCollection<Boolean> DateRangeVisible
        {
            get { return _dateRangeVisible; }
            set
            {
                _dateRangeVisible = value;
                OnPropertyChanged();
            }
        }


        private string _displayCalendarDate;
        public String DisplayCalendarDate
        {
            get { return _displayCalendarDate; }
            set
            {
                if (_displayCalendarDate != value)
                {
                    _displayCalendarDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private Job _selectedItem;
        public Job SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();

                    if (value == null) return;
                    var editJobViewModel = new EditJobViewModel(value);
                    NavigationService.Navigate(editJobViewModel);

                   // _navigation.PushModalAsync(new WTDTaskEditPage());
                   // MessagingCenter.Send(this, "EditTask", value);
                }
            }
        }

        public INavigation Navigation { get; set; }

        public ICommand CalendarChangeCommand { get; private set; }

        private void CalendarChange(object param)
        {
            switch((string)param)
            {
                case "NextMonth":
                    _currentDate = _currentDate.AddMonths(1);
                    break;
                case "NextYear":
                    _currentDate = _currentDate.AddYears(1);
                    break;
                case "LastMonth":
                    _currentDate = _currentDate.AddMonths(-1);
                    break;
                case "LastYear":
                    _currentDate = _currentDate.AddYears(-1);
                    break;
            }
            SetDateRange();
        }

        private void SetDateRange()
        {
            _dateRangeDate.Clear();
            _dateRangeTextColour.Clear();
            _dateRangeVisible.Clear();
            _dateRangeJobType.Clear();

            var fistOfTheMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            var firstDayofMonth = fistOfTheMonth.DayOfWeek;
            var startDate = (int)firstDayofMonth==0? fistOfTheMonth.AddDays(-6): fistOfTheMonth.AddDays(-1 * ((int)firstDayofMonth-1));
            var endDate = startDate.AddDays(42);
            var visible = true;

            DisplayCalendarDate = fistOfTheMonth.ToString("MMM yyyy");

            var queryArray = _realm.All<Job>().AsEnumerable().Where(j => j.Date >= startDate && j.Date <= endDate).OrderBy(j => j.Date).ThenBy(j => j.Type);
            JobList = new ObservableCollection<Job>(queryArray);

            for (int i = 0; i < 42; i++)
            {
                var date = startDate.Date.AddDays(i);
                var day = date.Day;
                var fontColour = Color.White;

                if(date.Day == DateTime.Now.Day 
                    && date.Month == DateTime.Now.Month 
                    && date.Year == DateTime.Now.Year) fontColour = Color.Aqua;
                if (i < 7 && day > 10) fontColour = Color.Gray;
                if (i > 20 && day < 10) fontColour = Color.Gray;

                _dateRangeTextColour.Add(fontColour);
                _dateRangeDate.Add(day.ToString("D2"));             

                if (i == 35 && day < 10) visible = false;
                _dateRangeVisible.Add(visible);

                SetCellBackgroundImage(date);
            }

            DateRangeJobType = _dateRangeJobType;
            DateRangeDate = _dateRangeDate;
            DateRangeTextColour = _dateRangeTextColour;
            DateRangeVisible = _dateRangeVisible;
            
        }

        private void SetCellBackgroundImage(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            var endDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

            var queryArray = _realm.All<Job>().AsEnumerable()
                .Where(j => j.Date >= startDate 
                && j.Date <= endDate).OrderBy(j => j.Date).ThenBy(j => j.Type);

            var jobs = new ObservableCollection<Job>(queryArray);

            var types = new string[3];

            foreach(var job in jobs)
            {
                switch(job.Type)
                {
                    case 1:
                        types[0] = "1";
                        break;
                    case 2:
                        types[1] = "2";
                        break;
                    case 3:
                        types[2] = "3";
                        break;
                }
            }
            var image = string.Format("tt{0}{1}{2}.png", types[0], types[1], types[2]);
            if (image.Length == 6)
            {
                _dateRangeJobType.Add("ttblank.png");
            }
            else
            {
                _dateRangeJobType.Add(image);
            }
           
        }

        private void SeedJobs()
        {
            _realm.Write(() => { _realm.RemoveAll(); });

            _realm.Write(() =>
            {
                for (var i = 1; i < 4; i++)
                {
                    var job = new Job { Date = DateTimeOffset.Now, Type = i, Description = "Losts of interesting text " + i };
                    _realm.Add(job);
                }
            });

            _realm.Write(() =>
            {
                for (var i = 1; i < 4; i++)
                {
                    var job = new Job { Date = DateTimeOffset.Now.AddDays(-1*i), Type = i,Description = "Single job "+i };
                    _realm.Add(job);
                }
            });
        }
    }
}
