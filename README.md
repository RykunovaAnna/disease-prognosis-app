# Disease Prognosis App

Desktop application for predicting reproductive health risks in women using machine learning methods.

## Description

Disease Prognosis App is a Windows desktop application developed in C# using Windows Forms. The program is designed to support preliminary assessment of reproductive health risks in women based on clinical and anamnestic data.

The application uses a pre-trained Support Vector Machine (SVM) model to classify patient data and calculate the probability of reproductive pathology. The system also provides interpretation of the prediction by showing the most significant risk factors.

## Features

* step-by-step input of patient data;
* validation of entered values;
* automatic BMI calculation;
* prediction of reproductive risk using an SVM model;
* display of risk level and recommendation;
* interpretation of the main factors affecting the result;
* export of results to Excel;
* generation of a Word report;
* local storage of patient examination results.

## Technologies

* C#
* .NET Framework
* Windows Forms
* Support Vector Machine
* ClosedXML
* Microsoft Visual Studio

## Usage

1. Open the solution file `disease_prognosis_Rykunova.sln` in Microsoft Visual Studio.
2. Restore NuGet packages if required.
3. Build the solution.
4. Run the application.
5. Enter patient data step by step and generate the prediction result.

## Note

The application was developed as part of a bachelor's thesis project. It is intended as a prototype of a clinical decision support system and does not replace medical diagnosis or professional clinical judgment.

No personal medical data is included in this repository.
