using AutTalk.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AutTalk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //variables
        private ObservableCollection<Sound> Sounds;
        private List<String> Suggestions;
        private List<MenuItem> MenuItems;

        public MainPage()
        {
            this.InitializeComponent();
            Sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(Sounds);

            //getting list of menu items and icons
            MenuItems = new List<MenuItem>();
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/need.png", Category = SoundCategory.Needs });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/wants.png", Category = SoundCategory.Wants });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/interaction.png", Category = SoundCategory.Interaction });

            //hide back button if not in menu and hide notes view
            BackButton.Visibility = Visibility.Collapsed;
            NotesView.Visibility = Visibility.Collapsed;
        }

        //opens are split view pane to view our menu
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            NotesView.Visibility = Visibility.Collapsed;
            MySplitView.Visibility = Visibility.Visible;
        }

        //Allows our user to navigate back(calls the go back method
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        //Search box allows user to find a specific sound they want
        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (String.IsNullOrEmpty(sender.Text))
            {
                goBack();//resets the page so all sounds are visible
            }

            //searchs our list of sounds
            SoundManager.GetAllSounds(Sounds);
            Suggestions = Sounds.Where(p => p.Name.StartsWith(sender.Text)).Select(p => p.Name).ToList();
            SearchAutoSuggestBox.ItemsSource = Suggestions;
        }


        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            //shows the selected sound
            SoundManager.GetSoundsByName(Sounds, sender.Text);
            CategoryTextBlock.Text = sender.Text;
            MenuItemsListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Visible;
        }

        //if menu item is clicked will show that category of sounds
        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            //filter category
            CategoryTextBlock.Text = menuItem.Category.ToString();
            SoundManager.GetSoundsByCategory(Sounds, menuItem.Category);
            //show back button if in menu
            BackButton.Visibility = Visibility.Visible;
        }

        //go back method resets everything to the all sounds menu
        private void goBack()
        {
            SoundManager.GetAllSounds(Sounds);
            CategoryTextBlock.Text = "All Sounds";
            MenuItemsListView.SelectedItem = null;
            NotesView.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            MySplitView.Visibility = Visibility.Visible;
        }

        //when a sound item is clicked it will play that sound
        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Sound)e.ClickedItem;
            MyMediaElement.Source = new Uri(this.BaseUri, sound.AudioFile);
        }

        //when notes is clicked it will show a new splitview and read out notes.txt and show it on screen
        private async void NotesButton_Click(object sender, RoutedEventArgs e)
        {
            NotesView.Visibility = Visibility.Visible;
            MySplitView.Visibility = Visibility.Collapsed;
            Warning.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Visible;
            var file = await isFilePresent("NotesFolder");
            if(file == true)
            {
                readFile();
            }
        }

        //will input our new note to notes.txt in local storage
        private async void Submit_button_Click(object sender, RoutedEventArgs e)
        {
            //if data is not entered it shows warning 
            if (notesInput.Text == "" || nameInput.Text == "")
            {
                Warning.Visibility = Visibility.Visible;
            }
            else//it continues on and enters a note
            {
                var dateTime = DateTime.Now.ToString();
                var notes = "Name:" + nameInput.Text + ".\nNotes: " + notesInput.Text + ".\nDate of Entry: " + dateTime + "!\n                  \n";
                //Create dataFile.txt in LocalFolder and write “My text” to it 
                var folder = ApplicationData.Current.LocalFolder;
                var newFolder = await folder.CreateFolderAsync("NotesFolder", CreationCollisionOption.OpenIfExists);

                var textFile = await newFolder.CreateFileAsync("notes.txt", CreationCollisionOption.OpenIfExists);

                //check if file exists
                var exists = await isFilePresent("NotesFolder");
                //if false file is empty
                if (exists == false)
                {
                    await FileIO.WriteTextAsync(textFile, notes);
                }
                else//append new note to file
                {
                    await FileIO.AppendTextAsync(textFile, notes);
                }
                readFile();
                // reset textboxes to empty
                nameInput.Text = "";
                notesInput.Text = "";
                Warning.Visibility = Visibility.Collapsed;
            }
        }

        //readfile method keeps the notes upto date
        private async void readFile()
        {
            var folder = ApplicationData.Current.LocalFolder;
            var newFolder = await folder.GetFolderAsync("NotesFolder");
            var files = await newFolder.GetFilesAsync();
            var desiredFile = files.FirstOrDefault(x => x.Name == "notes.txt");
            Stream stream = await desiredFile.OpenStreamForReadAsync();
            var textContent = await FileIO.ReadTextAsync(desiredFile);
            output.Text = textContent;
        }

        //checks to see if it has been created
        private async Task<bool> isFilePresent(string fileName)
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            return item != null;
        }

        //deletes notes from file
        private async void Delete_button_Click(object sender, RoutedEventArgs e)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var newFolder = await folder.CreateFolderAsync("NotesFolder", CreationCollisionOption.OpenIfExists);

            var textFile = await newFolder.CreateFileAsync("notes.txt", CreationCollisionOption.OpenIfExists);

            await FileIO.WriteTextAsync(textFile, "");
            readFile();

        }
    }
    
}
