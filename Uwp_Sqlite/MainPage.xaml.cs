using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Uwp_Sqlite.Data;
using Uwp_Sqlite.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Uwp_Sqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IEnumerable<Todo> todos { get; set; }
        
        public MainPage()
        {
            this.InitializeComponent();
            LoadTodosAsync().GetAwaiter();
        }

        private async Task LoadTodosAsync()
        {
            todos = await SqliteContext.GetTodos();
                   
        }
        
            private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(CreateTodoPage));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "CreateTodoPage":
                        ContentFrame.Navigate(typeof(CreateTodoPage));
                        break;

                    case "ActiveTodosPage":
                        ContentFrame.Navigate(typeof(ActiveTodosPage));
                        break;

                    case "ClosedTodosPage":
                        ContentFrame.Navigate(typeof(ClosedTodosPage));
                        break;

                }
                   
                }

            }
        }
    }

