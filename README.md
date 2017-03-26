# Campspot Coding Challenge

# How to run
This is a .NET MVC 5 web application. The solution file is provided in this repository and once cloned locally can be opened and ran in Visual Studio. I use the following plugin to ease the interaction between Visual Studio and github: https://visualstudio.github.com/ 

When running, click the Test Name to view details of the test case. When the "Execute Tests" button is clicked, the page will refresh and the test results will be visible under the details for each test case. Json test files are stored in the "Content" directory should you want to test my logic with additional test case files. 

# Problem Approach
I broke the problem down to its smallest base case; comparing a new input reservation date range to a single existing reservation for a single given gap rule. The comparison is made for this base case, then a boolean is returned indicating if the new input reservation could be made and not violate the given gap rule. This base case is then executed for each given gap rule for each previously existing campsite reservation provided in an input Json file. The base comparison boolean is retrieved and the logical AND is computed against the next iteration, such that if the new reservation violates any gap rule for any reservation within a campsite, that campsite is no longer eligible for the new reservation. All of this logic can be followed in the Services/GapRuleService.cs file. 

# Design Philosophy
All of the Json files are parsed into objects, and these objects are passed through the comparison process. Most of the comparison logic has been extracted into a services layer (GapRuleService). This layer allowed me to easily test my business logic, while keeping my controller level logic simple. GapRuleServiceTests.cs contains numerous private tests to follow not only "happy path" logic, but ensure the logic also fails when it is supposed to. I created four extra json test files for public facing executable tests that can be executed from the web app's "Execute Tests" button. 

# Assumptions
Exceptions will be thrown for improper formatting in Json files, as well as when invalid campsite IDs are provided for input reservations. It is assumed that each input Json file contains one new potential reservation. It is assumed that each Json input file will contain the following objects: search, gapRules, campsites, reservations. 
