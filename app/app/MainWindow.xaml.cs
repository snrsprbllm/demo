using System;
using System.Windows;

namespace app
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowPartners_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFrame.Navigate(new PartnersPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке страницы партнеров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddPartner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFrame.Navigate(new AddPartnerPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке страницы добавления партнера: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowPartnerHistory_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Реализация просмотра истории партнера
            MessageBox.Show("Функция просмотра истории партнера в разработке.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Реализация настроек
            MessageBox.Show("Функция настроек в разработке.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при закрытии приложения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoToMain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFrame.Content = null; // Очищаем Frame, чтобы вернуться на главную
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при возврате на главную страницу: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}