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
The application will run without any arguments, but some are implemented for convenience.

The application will only modify the first worksheet of a spreadsheet, so additional spreadsheets may be modified at will.

* -e [false/true] : Shows full error in console. Disabled by default as it might show Google specific information.
* -l [false/true] : Shows log in the console window. Enabled by default.
* -r [single/continous] : Single = will run once and quit. Continous = Will keep running until manually stopped. Continous is on by default.
* -f [Update frequency] : If in continous mode, will set the number of seconds between each attempt to read the market json file. Default is 60.
* -n [Spreadsheet name] : When the application is started the first time, it will create a spreadsheet with the given name. The name can be altered later manually in Google Spreadsheets. Default is SevOmatic.
* -x [Row number] : The first row of the worksheet to start inserting information on. This way you can customize the top of a spreadsheet manually. Default is 1.
