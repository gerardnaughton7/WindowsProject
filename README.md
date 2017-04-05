# __WindowsProject__
# __AutTalk__
For our Project we were asked to create Universal Windows Project which will demonstrate the use of isolated storage and at least one other sensor or service available on the devices. For my App I chose Local Storage and Sound.

## __What is AutTalk?__
AutTalk is a SoundBoard App which displays icons which when tapped or clicked will output a sound message. The App was created to help non-verbal Autistic people to communicate easier with friends and family. The idea came from when talking to my sister about work. She is a social care worker and mainly deals with Autistic children. She loves working with them but she says it is very frustrating for the children and for the workers with no communication.
There has already been other SoundBoard Apps created but she said a lot of them have been overly complicated. So I decided to create a simplistic version with only the needs wants and interactions of the Autistic person. I also implemented a notes board for the carer/family/friend to write notes of what happened during the day as well as favorite sound board buttons and also ones which he/she may not recognize yet.

## __Design of App__
I went for a simple easy to use design.
Mainly focusing on easy navigation, big buttons for each sound and a correct color scheme for the intended audience.
### Navigation
In my main header using a relative panel I contained my main navigation buttons and auto suggest box.
In the main area in row 1 of the grid I used a splitview to display the categories and the sounds.
This will allow the user to view the sounds and when the category menu is not clicked to view the icons of each category at the same time.
Then also in row1 of the grid I had stackpanel for displaying the Daily notes view. which will be initially hidden but would appear when the notes button is clicked. This stackpanel will display a form for notes input and also a text area for viewing all the notes.
### Buttons
The buttons I used display icons representing each sound that the app provides. I made sure to use icons which are most recognisable to people. I kept them all black and white so each one would be clear to see.
All the buttons are separated into 3 categories. The categories I chose were Needs, Wants and Interaction.
### Color scheme
Doing some research online and from asking my sister, I chose 3 main colors Teal, White and Beige. These colors have all been recommended for Autistic People to insure a calming effect. As bright colors such as reds and yellows can be aggravating to them visually.

## __Functionality__
As I said above the sensor I chose was Sound. When the App is opened the app displays a header with buttons for menu, notes and auto suggest box for searching. Below this is a splitview displaying the icons for each category on the left and all the sounds on the right. If you click on any of the sound icons it will say what the person wants relating to that icon. If you click on the menu button the left splitview pane opens showing the category icons and the name of each category. If you click on any of the categories the right pane will only display the sounds for that category.
If you search for a specific sound using the auto suggest box in the top right corner and your search is successful it will show only the sound found. If it is unsuccessful it will show no sounds.
If you the user clicks the notes button it will change below to a stackpanel displaying a form for notes input and a text area with previous notes. All notes are saved to a NotesFolder in local storage and then to a notes.txt file. At the bottom of the text area there is a delete button allowing the user to clear all previous notes.
When a category, menu or notes is clicked a back button is made visible in the header allowing the user to go back to the original start page.  

## __Code__
The project was developed using Visual Studio 2015 community edition.
I contained all the xaml code within MainPage.xaml. In here contains the design layout of the app. For the functionality of all the buttons, auto suggest box and other features the code is contained in MainPage.xaml.cs.
Here I linked up my Model Classes MenuItems.cs, Sound.cs and SoundManager.cs. These allow me to organise my categories, sounds, images and menu icons.
All together bringing together the App AutTalk.

## References
* I found a great help was Channel9 which had tutorials for Windows 10 for Beginners
  * https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners.
* For setting up local storage I followed Microsoft Docs.
  * https://docs.microsoft.com/en-us/windows/uwp/app-settings/store-and-retrieve-app-data.
* For my Icons I used many various websites.
  * http://www.freepik.com
  * http://www.freeiconspng.com
  * https://clipartfox.com/download/63bd5f5d83b79ce4ef8175d552def94890f28cf3.html
  * https://www.iconfinder.com/icons/481055/emoticon_face_food_greedy_hungry_smiley_icon
