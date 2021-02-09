# SevOmatic

## Setup instructions

##### 1. Download repository
- Open Command Line (cmd/Powershell/Linux console)
- Create a folder and enter it
- Type "git clone https://github.com/bar10dr/SevOmatic.git"

##### 2. Creating the Google credentials
- Go to https://developers.google.com/sheets/api/quickstart/dotnet
- Press the blue button named "Enable the Google Sheets API"
- Enter a name for the application and press next
- Choose "Destop app" in the dropdown and press "Create"
- Press the "Download client configuration" and save the "credentials.json" file to the "SevOmatic/SevOmatic.Terminal" folder

##### 3. Giving the application rights to modify your Google Spreadsheets
- Open Command Line (cmd/Powershell/Linux console)
- Go to the repository directory/SevOmatic.Terminal
- Type "dotnet run", this will start the application
- It will open a browser window for Google (Copy the URL to the browser where your google account is logged in)
- Choose the account of the user who's Spreadsheets account you want to use
- Since the Application has not been verified by Google, press the "Advanced" link
- Press the "Go to <whatever name you gave your application>" link
- Press the "Allow" link
- Press the "Allow" button

##### 4. Setup finished
The application should now create a new spreadsheet, and continually update it.
