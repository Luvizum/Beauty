﻿using Beauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Beauty.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientInfo.xaml
    /// </summary>
    public partial class ClientInfo : Window
    {

        BeautyEntities context;
        Client newClient;
        DataGrid clientsDataGrid;

        string pathPhotoClient = string.Empty;
        public ClientInfo(BeautyEntities context, Client client, DataGrid clients)
        {
            InitializeComponent();
            this.context = context;
            this.newClient = client;
            this.DataContext = newClient;
            this.clientsDataGrid = clients;
            genderComboBox.ItemsSource = context.Gender.ToList();
        }

        private void saveDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckData())
            {
                newClient.GenderCode = CheckGender();
                newClient.Birthday = (DateTime)birthdayDatePicker.SelectedDate;
                newClient.RegistrationDate = DateTime.Now;
                newClient.PhotoPath = pathPhotoClient;
                context.SaveChanges();
                clientsDataGrid.ItemsSource = context.Client.ToList();
                this.Close();
            }
        }
        private string CheckGender()
        {
            string nameGender;
            string gender = string.Empty;

            nameGender = genderComboBox.Text;
            switch (nameGender)
            {
                case "Женский":
                    gender = "1";
                    break;
                case "Мужской":
                    gender = "0";
                    break;
                default:
                    gender = string.Empty;
                    break;
            }
            return gender;
        }

        private bool CheckData()
        {
            try
            {
                var check = this.DataContext as Client;
                string error = string.Empty;
                if (!Regex.IsMatch(check.LastName, "^[a-zA-Zа-яА-Я ]*$") || check.LastName == string.Empty || check.LastName.Length > 50)
                    error += "Неверный ввод поля Фамилия \n";
                if (!Regex.IsMatch(check.FirstName, "^[a-zA-Zа-яА-Я ]*$") || check.LastName == string.Empty || check.FirstName.Length > 50)
                    error += "Неверный ввод поля Имя \n";
                if (!Regex.IsMatch(check.Patronymic, "^[a-zA-Zа-яА-Я ]*$") || check.LastName == string.Empty || check.Patronymic.Length > 50)
                    error += "Неверный ввод поля Отчество \n";
                if (!Regex.IsMatch(check.Email, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$") || check.Email == string.Empty)
                    error += "Неверный ввод e-mail \n";
                if (genderComboBox.SelectedItem == null)
                    error += "Не введен пол клиента";
                if (!Regex.IsMatch(check.Phone, @"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\-\d\d) | (\d{ 7})| (\d{ 3}\-\d\-\d{ 3})|)") || check.Phone == string.Empty)
                    error += "Неверный ввод поля Телефон";
                if (error == string.Empty)
                    return true;
                else
                {
                    MessageBox.Show(error, "Ошибка добавления клиента", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось сохранить клиента, проверьте введенные данные", "Ошибка сохранения данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return false;

        }
    }
}
