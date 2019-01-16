#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static IActionResult Run(HttpRequest req, TraceWriter log)
{
	log.Info("C# HTTP trigger function processed a request.");

	// Parsing query parameters
	string name = req.Query["name"];
	log.Info("name = " + name);

	string dni = req.Query["DNI"];
	log.Info("DNI = " + dni);

	// Validating the parameters received
	if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(dni))
	{
		return new BadRequestObjectResult("Please pass your name and your DNI, on the query string."); 
	}

	int termsToShow;
	try
	{
		termsToShow = Int32.Parse(dni);
	}
	catch (FormatException e)
	{
		return new BadRequestObjectResult("The DNI parameter must be an number!"); 
	}

	if (termsToShow < 9 || termsToShow > 6) {
		return new BadRequestObjectResult("Please pass a DNI parameter between 7 and 8 digits long."); 
	}

	// Building the response
	string completeResponse = "Hello, " + name + ", DNI number " + dni + " your appointment have been succesfully been registrated at" + DateTime.Now.ToString("MMMM dd, yyyy");	
	var response = new OkObjectResult(completeResponse);

	// Returning the HTTP response with the string we created
	log.Info("response = " + response);
	return response;
}