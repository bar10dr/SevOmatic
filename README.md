# SevOmatic

## Setup instructions
Tools needed
- .net core 5 SDK https://dotnet.microsoft.com/download/dotnet/5.0
- A git client (Windows: https://gitforwindows.org/)

##### 1. Download repository
- Open a terminal (Cmd/Powershell/Bash)
- Create a folder and enter it
- Type "git clone https://github.com/bar10dr/SevOmatic.git" or use another git client to download the source from github

##### 2. Creating the Google credentials
This step will register an application at Google and give you a json file containing the credentials for your registered application.
- Go to https://developers.google.com/sheets/api/quickstart/dotnet
- Press the blue button named "Enable the Google Sheets API"
- Enter a name for the application and press next
- Choose "Destop app" in the dropdown and press "Create"
- Press the "Download client configuration" and save the "credentials.json" file to the "SevOmatic/SevOmatic.Terminal/" folder

##### 3. Giving the application rights to modify your Google Spreadsheets
This step will grant the application you just created the rights to modify your Google user's spreadsheet data.
- Open Command Line (cmd/Powershell/Linux console)
- Go to the "SevOmatic/SevOmatic.Terminal/" folder
- Type "dotnet run", this will start the application
- It will open a browser window to Google (Copy URL to another browser manufacturer if needed)
- Choose the account of the user who's Spreadsheets account you want to use
- Since the Application has not been verified by Google, press the "Advanced" link
- Press the "Go to <whatever name you gave your application>" link
- Press the "Allow" link
- Press the "Allow" button

##### 4. Setup finished
The application should now create a new spreadsheet, and continually update it.

## Usage instructions
The application will only modify the first worksheet of a spreadsheet, so additional spreadsheets may be modified at will.

The application refers to the spreadsheet ID, so you are free to change the spreadsheet name or location at will.

The application will run fine without any command line arguments, but some are implemented for convenience.

To use arguments with the "dotnet run" command, you have to add two -- after the command before initiating arguments.

Ex: dotnet run -- -e false -r single -f 30 -x 3

---

#### _Show detailed error information_

-e [false/true]

Shows the full error in the console should the application crash. Disabled by default as it might show personal Google information.

_`Ex: dotnet run -- -e true`_

#### Show log in console window

-l [false/true]
 
Shows log in the console window, turning this off will stop any text being output to the console. Enabled by default.
 
_`Ex: dotnet run -- -l false`_

#### Run mode

-r [single/continous]

Dictates if the application should run just once or keep running. This way you can elect to use Windows Task Scheduler or Linux crontab instead of having the application running constantly.

Single = will run once and quit.

Continous = Will keep running until manually stopped. Continous is on by default.
 
_`Ex: dotnet run -- -r single`_

#### Update frequency

-f [Update frequency]

If in continous mode, will set the number of seconds between each attempt to update the spreadsheet. Default is 60.

_`Ex: dotnet run -- -f 30`_

#### Specify what row in the spreadsheet to start inserting information

-x [Row number]

The first row of the worksheet to start inserting information on. This way you can customize the top of a spreadsheet manually. Default is 1.
 
_`Ex: dotnet run -- -x 3`_
