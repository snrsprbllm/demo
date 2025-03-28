using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace app
{
    public partial class EditPartnerPage : UserControl
    {
        private Партнеры _partner;
        private Demo1Entities _context = new Demo1Entities();

        public EditPartnerPage(Партнеры partner)
        {
            InitializeComponent();
            _partner = partner;
            LoadPartnerData();
        }

        private void LoadPartnerData()
        {
            // Заполняем поля данными партнера
            NameTextBox.Text = _partner.Наименование_партнера;
            DirectorTextBox.Text = _partner.Директор;
            PhoneTextBox.Text = _partner.Телефон_партнера;
            EmailTextBox.Text = _partner.Электронная_почта_партнера;
            AddressTextBox.Text = _partner.Юридический_адрес_партнера;
            INNTextBox.Text = _partner.ИНН.ToString();
            RatingTextBox.Text = _partner.Рейтинг.ToString();
            DiscountTextBox.Text = _partner.Скидка_партнера.ToString();

            // Загрузка типов партнеров
            LoadPartnersTypes(_partner.id_типа_партнера);
        }

        private void LoadPartnersTypes(int selectedTypeId)
        {
            try
            {
                var partnerTypes = _context.ТипПартнера.ToList();
                TypeComboBox.ItemsSource = partnerTypes;
                TypeComboBox.DisplayMemberPath = "Тип_партнера";
                TypeComboBox.SelectedValuePath = "id_типа_партнера";

                // Устанавливаем выбранный тип партнера
                if (partnerTypes.Any())
                {
                    TypeComboBox.SelectedValue = selectedTypeId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов партнеров: {ex.Message}");
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Обновляем данные партнера
                _partner.Наименование_партнера = NameTextBox.Text;
                _partner.Директор = DirectorTextBox.Text;
                _partner.Телефон_партнера = PhoneTextBox.Text;
                _partner.Электронная_почта_партнера = EmailTextBox.Text;
                _partner.Юридический_адрес_партнера = AddressTextBox.Text;
                _partner.ИНН = long.Parse(INNTextBox.Text);
                _partner.Рейтинг = byte.Parse(RatingTextBox.Text);
                _partner.Скидка_партнера = int.Parse(DiscountTextBox.Text);

                // Обновляем тип партнера
                _partner.id_типа_партнера = (int)TypeComboBox.SelectedValue;

                // Сохраняем изменения в базе данных
                _context.SaveChanges();

                MessageBox.Show("Изменения успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
