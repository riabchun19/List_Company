﻿using System;
using System.ComponentModel;
using System.Windows;

namespace ListOfCompanyEmployees.Views
{
    public class EditableDataChildWindow : Window, IDisposable
    {
        private readonly INotifyPropertyChanged _data;
        public bool IsDataChanged { get; protected set; }
        public bool WasSavedData { get; protected set; }

        public EditableDataChildWindow(INotifyPropertyChanged data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));

            _data.PropertyChanged += OnDataChanged;
        }

        private void OnDataChanged(object sender, PropertyChangedEventArgs e) => IsDataChanged = true;

        protected void OnClosing(object _, CancelEventArgs e)
        {
            // Если данные грязные, уведомить пользователя и попросить ответ
            if (IsDataChanged && !WasSavedData)
            {
                var msg = "Данные были изменены. Закрыть без сохранения?";
                var result =
                    MessageBox.Show(
                        msg,
                        "Data App",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // Если пользователь не хочет закрывать, отмените закрытие
                    e.Cancel = true;
                }
            }
        }

        public void Dispose() => _data.PropertyChanged -= OnDataChanged;
    }
}