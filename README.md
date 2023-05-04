# Saler
Welcome to Garage Saler, the new way to find and post garage sale listings in your area! Saler aims to bring back the old days of newspaper-based garage sale hunting, where all the listings are conglomerated into one convenient location. [Test it out here!](https://salerapp20230325190845.azurewebsites.net)

## About Us
The team that created Saler consists of Dylan Bauders, Jake Besko, Ethan Hubbell, Samuel St John, and Karl Tobianski. This application was created for CPS 498: Senior Design II as a capstone project for Central Michigan University's Computer Science program in the spring semester of the 2022-2023 school year.

Special thanks to our advisors who helped us along the way: Dr. Patrick Kinnicutt, Dr. Patrick Seeling, and Dr. Alex Redei.

## How to Run Locally
To run this application locally, you should simply clone this GitHub repo into Visual Studio and run the application in any solution configuration you would like. Note that it is _highly likely_ that you will not be able to use any features that interact with the database (including the login, registration, search, and listing features) without first having your IP address added to the Azure database access permissions. If you are not a developer for this application, that means that the best way to test the functionality of the application is via [the published version of the site](https://salerapp20230325190845.azurewebsites.net). If you would like your IP address to be added to the Azure resource group access permissions, please email stjoh1sr@cmich.edu for that request.

This application requires .NET 6 support to run.

## Features
The major features implemented in this application are:
* Create an account and log in
* Create garage sale listings
* Hide or edit your listings
* Save interesting listings for later
* Search for listings
* Create a route from your location to a listing
* Dark mode support