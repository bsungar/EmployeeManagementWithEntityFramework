using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MVVM.Commands;
using MVVM.Commands.MVVM.Utility;
using MVVM.Models;
using MVVM.ViewModels;


namespace MVVM.ViewModels
{
    public class employeeviewmodel : INotifyPropertyChanged
    {
        private readonly EmployeeServices employeeService;

        public employeeviewmodel()
        {
            employeeService = new EmployeeServices();
            LoadData();
            TempEmployee = new Employee();

            CurrentEmployee = new Employee();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            deleteCommand = new RelayCommand(Delete);
            updateCommand = new RelayCommand(Update);
        }

        private ObservableCollection<Employee> employeeList;
        public ObservableCollection<Employee> EmployeeList
        {
            get { return employeeList; }
            set
            {
                employeeList = value;
                OnPropertyChanged(nameof(EmployeeList));
            }
        }

        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set
            {
                currentEmployee = value;

                if (value == null)
                {
                    return;
                }

                TempEmployee = new Employee
                {
                    Id = value.Id,
                    Name = value.Name,
                    Age = value.Age
                };
                OnPropertyChanged(nameof(CurrentEmployee));
            }
        }

        private Employee tempEmployee;

        public Employee TempEmployee
        {
            get { return tempEmployee; }
            set { tempEmployee = value; OnPropertyChanged(nameof(TempEmployee));
            }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private void LoadData()
        {
            EmployeeList = new ObservableCollection<Employee>(employeeService.GetAll());
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand { get { return saveCommand; }}
       
        private RelayCommand searchCommand;
        public RelayCommand SearchCommand { get { return searchCommand; }}
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand { get { return deleteCommand; } }
        
        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand { get {return updateCommand; } }

        private void Save()
        {
            try
            {
                var newEmp = new Employee { Age = TempEmployee.Age, Id = TempEmployee.Id, Name = TempEmployee.Name };

                var isSaved = employeeService.AddEmployee(newEmp);
                LoadData();

                if (isSaved)
                {
                    Message = "Employee Saved";
                }
                else
                {
                    Message = "Save operation failed.";
                }
            }
            catch (System.Exception ex)
            {
                Message = ex.Message;
            }
        }

        private void Search()
        {
            try
            {
                var objEmployee = employeeService.Search(TempEmployee.Id);
                if (objEmployee != null)
                {
                    Message = "Employee has been found.";
                }
                else
                {
                    Message = "Employee Not Found.";
                }
            }
            catch (System.Exception ex)
            {
                Message = ex.Message;
            }
        }

        private void Delete()
        {
            try
            {
                var isDeleted = employeeService.DeleteEmployee(TempEmployee);
                if (isDeleted)
                {
                    Message = "Employee Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete operation failed.";
                }
            }
            catch (System.Exception ex)
            {
                Message = ex.Message;
            }
        }



        public void Update()
        {
            using EmployeeContext context = new EmployeeContext();
            bool isUpdated = false;

            if (TempEmployee.Age > 21 && TempEmployee.Age <= 51)
            {
                var entity = context.Update(TempEmployee);
                context.SaveChanges();
                LoadData();
                isUpdated = true;
            }
            else
            {
                
                MessageBox.Show("Çalışan yaş aralığı uygun değil! (Yaş aralığı: 22-51)", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           // return isUpdated;

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}








