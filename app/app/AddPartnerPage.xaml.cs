using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace demo1
{
    /// <summary>
    /// Логика взаимодействия для AddPartnerPage.xaml
    /// </summary>
    public partial class AddPartnerPage : Page
    {
        private Demo1Entities _context = new Demo1Entities();

        public AddPartnerPage()
        {
            InitializeComponent();
            LoadPartnersTypes();
        }

        private void LoadPartnersTypes()
        {
            try
            {
                var partnerTypes = _context.ТипПартнера.ToList();
                TypeComboBox.ItemsSource = partnerTypes;
                TypeComboBox.DisplayMemberPath = "Тип_партнера";
                TypeComboBox.SelectedValuePath = "id_типа_партнера";
                if (partnerTypes.Count > 0)
                    TypeComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов партнеров: {ex.Message}");
            }
        }

        private void AddPartner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var maxId = _context.Партнеры.Max(p => p.id_партнера) + 1;
                var newPartner = new Партнеры
                {
                    id_партнера = maxId,
                    Наименование_партнера = NameTextBox.Text,
                    Директор = DirectorTextBox.Text,
                    Телефон_партнера = PhoneTextBox.Text,
                    Электронная_почта_партнера = EmailTextBox.Text,
                    Юридический_адрес_партнера = AddressTextBox.Text,
                    ИНН = long.Parse(INNTextBox.Text),
                    Рейтинг = byte.Parse(RatingTextBox.Text),
                    Скидка_партнера = int.Parse(DiscountTextBox.Text),
                    id_типа_партнера = (int)TypeComboBox.SelectedValue
                };
                _context.Партнеры.Add(newPartner);
                _context.SaveChanges();
                MessageBox.Show("Партнер успешно добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении партнера: {ex.Message}");
            }
        }
    }
}
