using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Uwp_Sqlite.Data;
using Uwp_Sqlite.Models;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Uwp_Sqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClosedTodosPage : Page
    {
      
        public ClosedTodosPage()
        {
            this.InitializeComponent();
            LoadClosedTodos().GetAwaiter();
            LoadStatus();    
        }

        public async Task LoadClosedTodos()
        {
            var todos = await SqliteContext.GetTodos();
            lvClosedTodos.ItemsSource = todos
                .Where(i => i.Status == "closed")
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
            var selectedTodo = (Todo)lvClosedTodos.SelectedItem;
            var status = cmbStatus.SelectedValue.ToString();
            SqliteContext.UpdateTodoStatusAsync(selectedTodo.Id, status).GetAwaiter();
        }
    }
}
