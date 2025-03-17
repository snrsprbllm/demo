using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;

namespace app
{
    public partial class PartnersPage : UserControl
    {
        private Demo1Entities _context = new Demo1Entities();
        public ObservableCollection<Партнеры> Partners { get; set; }

        public PartnersPage()
        {
            InitializeComponent();
            Partners = new ObservableCollection<Партнеры>();
            this.DataContext = this;

            try
            {
                LoadPartners();
                LoadPartnerTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных партнеров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadPartners()
        {
            try
            {
                Partners.Clear();
                foreach (var partner in _context.Партнеры.ToList())
                {
                    Partners.Add(partner);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка партнеров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadPartnerTypes()
        {
            try
            {
                var partnerTypes = _context.ТипПартнера.ToList();
                partnerTypes.Insert(0, new ТипПартнера
                {
                    id_типа_партнера = 0,
                    Тип_партнера = "Все типы"
                });

                PartnerTypeFilter.ItemsSource = partnerTypes;
                PartnerTypeFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов партнеров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PartnerTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void PartnerSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            try
            {
                var selectedTypeId = (int)PartnerTypeFilter.SelectedValue;
                var searchText = PartnerSearchBox.Text?.ToLower() ?? string.Empty;

                var filteredData = _context.Партнеры
                    .Where(p => (selectedTypeId == 0 || p.id_типа_партнера == selectedTypeId) &&
                                (string.IsNullOrEmpty(searchText) || p.Наименование_партнера.ToLower().Contains(searchText)))
                    .ToList();

                PartnersItemsControl.ItemsSource = filteredData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик кнопки "Редактировать"
        private void EditPartner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button != null)
                {
                    // Получаем выбранного партнера
                    var partner = button.DataContext as Партнеры;
                    if (partner != null)
                    {
                        // Переходим на страницу редактирования
                        var mainWindow = Application.Current.MainWindow as MainWindow;
                        if (mainWindow != null)
                        {
                            mainWindow.MainFrame.Navigate(new EditPartnerPage(partner));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при переходе на страницу редактирования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}