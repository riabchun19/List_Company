﻿using System.ComponentModel;
using ListOfCompanyEmployees.Models.Base;

namespace ListOfCompanyEmployees.Models
{
    public class Department : BaseNotifyPropertyChanged, IDataErrorInfo
    { 
        private string _name;  
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        public string this[string columnName]
        {
            get
            {  
                switch (columnName)
                {
                    case "Name":
                        if (int.TryParse(Name, out _))
                        {
                            return "Некорректное имя!!!";
                        }
                        break;
                    default:
                        return null;
                }
                return null;
            }
        }

        public string Error => null;

        public Department Clone() => new Department()
        { 
            Name = _name.Clone().ToString() 
        };

        public override string ToString() => Name;
    }
}