Repository Tests for RepairRequest and User Models
Overview
This repository contains unit tests for a set of repository methods used to interact with RepairRequest and User models in a database. The tests are designed to ensure that CRUD operations (Create, Read, Update, Delete) are functioning as expected.

Features
Create: Adds a new entry to the database.
Read (Get and GetAll): Retrieves one or more entries from the database.
Update: Modifies an existing entry in the database.
Delete: Removes an entry from the database.
Find: Filters and paginates results based on specific criteria.
Requirements
.NET Core 6.0 or later
Visual Studio or any other IDE supporting C# and .NET Core
An in-memory database for testing (no external database needed)
Setup
Clone the repository.

Open the solution in Visual Studio or your preferred IDE.

Ensure you have the necessary dependencies by restoring the NuGet packages.

Run the tests using the built-in test runner or through the command line:

dotnet test
Test Cases
The test cases cover the following operations:

Create_AddsRequestToDbSet: Verifies a new RepairRequest is added to the database.
Delete_RemovesRequestFromDbSet: Ensures a RepairRequest is removed from the database.
Find_ReturnsFilteredAndPagedRequests: Tests that filtering and pagination work correctly for RepairRequest.
Get_ReturnsRequestById: Retrieves a RepairRequest by its ID.
GetAll_ReturnsAllRequests: Retrieves all RepairRequest entries from the database.
Update_ChangesRequestEntityStateToModified: Ensures that updating a RepairRequest updates its properties in the database.
Usage
Each repository test case follows the Arrange-Act-Assert pattern:

Arrange: Set up the necessary data and context.
Act: Call the method being tested (Create, Update, Delete, Get, Find).
Assert: Verify that the results match the expected behavior.
