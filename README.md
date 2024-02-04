Please clone the code as usual. Make sure to run Client only since the API are already pubslished to Azure.
This app consists of Client & Server. The API is hosted in Azure including the SQL Server for database.
The main requirements are fulfilled except on 'adding new phone number' and 'activating' function.
The main obstacles are when POST the parameters are passed as NULL to the controller. This includes when performing PUT will giving error 405 method not allowed.
I am still working on to troubleshoot this to make it work as requested.
