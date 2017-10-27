# Coding Test for AGL with Azure Functions #

This provides a sample code repository for coding test in AGL before being engaged.


## Requirements ##

Based on the instruction on [http://agl-developer-test.azurewebsites.net/](http://agl-developer-test.azurewebsites.net/), a candidate is required to submit an application to fulfill the requirements.

* Data source can be aquired from a JSON object by sending an HTTP request to [http://agl-developer-test.azurewebsites.net/people.json](http://agl-developer-test.azurewebsites.net/people.json).
* The data source needs to be processed to display result with the rules of:
  * Display all cats in alphabetical order,
  * All cats are listed under their owners' gender, and
  * Can owners' genders are considered as headings.
* Use LINQ for grouping and sorting the data source.


## Implementation ##

In order to satisfy the requirements, I used the **serverless** architecture &ndash; Azure Functions, because:

* It doesn't need to setup an application environment,
* It only focuses on the code itself, which is basically business logic,
* It's easy to build


## Result ##

The application can be run on either local machine or Azure Functions instance on Azure. In order to run this application, use Visual Studio or Azure Function Tooling npm package. If Visual Studio is chosen, follow the steps below:

* Clone the repository to your local machine.
* Open `AglCodingTest.sln`.
* Build the solution.
* Press F5 key to run the Azure Function instance locally.
* Open a web browser and type `http://localhost:7071/pets` to run the Azure Functions application.
* The required result will be displayed.
* If a querystring parameter, `type` can be used to specify the pet type. Currently, the `type` parameter only considers three values &ndash; `Dog`, `Cat` and `Fish`

Valid requests are:

* [http://localhost:7071/pets](http://localhost:7071/pets)
* [http://localhost:7071/pets?type=cat](http://localhost:7071/pets?type=cat)
* [http://localhost:7071/pets?type=dog](http://localhost:7071/pets?type=dog)
* [http://localhost:7071/pets?type=fish](http://localhost:7071/pets?type=fish)

