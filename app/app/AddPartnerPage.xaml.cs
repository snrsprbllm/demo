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
            // Загружаем типы партнеров из базы данных
            var partnerTypes = _context.ТипПартнера.ToList();

            // Устанавливаем источник данных для ComboBox
            TypeComboBox.ItemsSource = partnerTypes;

            // Настройка отображения: название типа партнера будет показываться в комбобоксе
            TypeComboBox.DisplayMemberPath = "Название_типа"; // Название поля, которое будет отображаться
            TypeComboBox.SelectedValuePath = "id_типа";      // Название поля, которое будет использоваться как значение
        }

        private void AddPartner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем максимальный ID партнера и увеличиваем его на 1
                var maxId = _context.Партнеры.Max(p => p.id_партнера) + 1;

                // Создаем нового партнера
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

                    // Получаем выбранный ID типа партнера из ComboBox
                    id_типа_партнера = (int)TypeComboBox.SelectedValue
                };

                // Добавляем нового партнера в базу данных
                _context.Партнеры.Add(newPartner);
                _context.SaveChanges();

                MessageBox.Show("Партнер успешно добавлен!");
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при добавлении партнера: {ex.Message}");
            }
        }
    }
}
