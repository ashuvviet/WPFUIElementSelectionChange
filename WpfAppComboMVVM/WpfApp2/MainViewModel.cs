using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp2
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string message;

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        public ObservableCollection<string> Departments = new ObservableCollection<string> { "Admin", "Development", "Tranining", "Others" };

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EmployeeVM> MainItems { get; set; }
        
        public MainViewModel()
        {            
            MainItems = new ObservableCollection<EmployeeVM> { new EmployeeVM("Emp1", this), new EmployeeVM("Emp2", this), new EmployeeVM("Emp3", this) };
        }
    }

    public class EmployeeVM
    {
        public EmployeeVM(string name, MainViewModel parent)
        {
            SelectionChangeCommand = new MyCustomCommand<SelectionChangedEventArgs>(OnSelectionChange);
            Name = name;
            Parent = parent;
            Department = "Development";
        }

        private void OnSelectionChange(SelectionChangedEventArgs obj)
        {
            // write your logic on selection change.
            Parent.Message = $"Combo box selection Changed from {obj.RemovedItems[0]} to {obj.AddedItems[0]} using behavior";
        }

        public ObservableCollection<string> Items => Parent.Departments;

        public ICommand SelectionChangeCommand { get; set; }

        public string Name { get; set; }
        public MainViewModel Parent { get; }

        public string Department { get; set; }
    }

    public class MyCustomCommand<T> : ICommand
    {
        private Action<T> execute;
        private Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public MyCustomCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }
    }

}
