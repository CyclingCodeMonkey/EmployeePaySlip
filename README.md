# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Emoloyee Payslip Coding Excercise 
* To caclculate the followig 
  * income tax for an individual based upon the gross annual salary
  * Monthly Net income
  * Monthly Gross income
  * Monthly Super contributions based upon input value 


### How do I get set up? ###

* This solution requires .Net Core 2.0
* 


### How to run tests ###
* To run tests, best performed from the command line.  Change directory to where the root of the source code and run "runTests.cmd" (note this will only for for Windows machines only)
* For Mac users open a terminal session, change location to where the root of the source code folder and run the following commands : 
    dotnet test EmployeePayslip.Domain.UnitTests
    dotnet test EmployeePayslip.DataAccess.UnitTests


### Assumptions ###

* Australian Income Tax Only
* For Individuals only and this is their primary source of income
* For current financial year income tax only
* No salary sacrifice
* No Levies are included, e.g. Medicare, Flood, etc

