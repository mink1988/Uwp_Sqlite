using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Uwp_Sqlite.Models;
using Uwp_Sqlite.Data;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp_Sqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ActiveTodosPage : Page
    {

        public ActiveTodosPage()
        {
            this.InitializeComponent();         
            LoadActiveTodos().GetAwaiter();
            LoadStatus();       
        }

        private async Task LoadActiveTodos()
        {
            var todos = await SqliteContext.GetTodos();
            lvActiveTodos.ItemsSource = todos
                .Where(i => i.Status != "closed")
                .OrderByDescending(i => i.Created)
                .Take(SettingsContext.GetMaxItemsCount())
                .ToList();
        }

        private void LoadStatus()
        {
            cmbStatus.ItemsSource = SettingsContext.GetStatus();
        }

        private void StatusChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTodo = (Todo)lvActiveTodos.SelectedItem;
            var status = cmbStatus.SelectedValue.ToString();
            SqliteContext.UpdateTodoStatusAsync(selectedTodo.Id, status).GetAwaiter();
        }
    }
}
