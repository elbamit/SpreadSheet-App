# SpreadSheet-App

Implementation of the GUI part of the SpreadSheet for OS course in SISE department of BGU University.

![image](https://user-images.githubusercontent.com/66309521/129691455-d1cae55b-cf90-46e3-9461-baa76917de76.png)


## Table of Content
* [General Information](#General-Information)
* [Technologies](#Technologies)
* [Supported Operations](#Supported-Operations)
* [Synchronization & Thread Safety](#Synchronization-&-Thread-Safety)

## General Information
Second part of the Shareable SpreadSheet app project - implements a GUI for a SpreadSheet (which is thread safe!) that saves data as strings and can perform a number of operations from the [Supported Operations](#Supported-Operations). The SpreadSheet is first initialized to a custom size of rows and columns which can be changed later on.

## Technologies
.NET Core 3.1

## Supported Operations
- Load - Loads an existing SpreadSheet file.
- Save - Saves the SpreadSheet data onto a file.
- Set Cell - Modify the data on a specific cell in the Sheet.
- Search String - Searches the given string in the Sheet and returns it if found.
- Add row/column - Adds a new row/column to the sheet after a specified row/column.


## ScreenShots

- Opening message:


![image](https://user-images.githubusercontent.com/66309521/129691705-76af15a0-a464-49c0-9eaa-cae19e80612d.png)



- SpreadSheet initialization:




![image](https://user-images.githubusercontent.com/66309521/129691840-5643471f-b61a-4858-a93f-0b5824a920d2.png)
![image](https://user-images.githubusercontent.com/66309521/129691891-5ac1737b-e14d-4cce-947c-3d1557fa0b1b.png)
![image](https://user-images.githubusercontent.com/66309521/129691947-a4fc7781-2737-47d1-a2e8-f626d6ba3028.png)

![image](https://user-images.githubusercontent.com/66309521/129692002-60a43e2a-c343-4985-9461-6813f6bf1385.png)


- Load/Save:
![image](https://user-images.githubusercontent.com/66309521/129692169-10f9cf5f-5084-414d-82f6-670418b412bb.png)

- Set Cell - Asks the user to enter row and column first:
![image](https://user-images.githubusercontent.com/66309521/129692459-32a753ae-ac4d-4b5b-a486-b01f1984d74b.png)

- Search String:
![image](https://user-images.githubusercontent.com/66309521/129692769-cf531b15-4b74-4ff6-839d-c536cb6ced20.png)
![image](https://user-images.githubusercontent.com/66309521/129692813-f77252a0-4400-4847-bfef-438ba318038b.png)

- Add Row:
![image](https://user-images.githubusercontent.com/66309521/129692924-0066877f-6a61-4a46-b6ce-5a07fdbfa2b7.png)
![image](https://user-images.githubusercontent.com/66309521/129692960-d9d3a5ab-005d-4a28-949e-bd3354474149.png)
