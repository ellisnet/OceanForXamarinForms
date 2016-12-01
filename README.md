# Ocean Validation for Xamarin Forms

## Introduction

This sample Xamarin Forms project shows using Prism, Unity, and Ocean Validation in several scenarios.

See my blog post here for the links to the videos: https://oceanware.wordpress.com/2016/12/01/ocean-validation-for-xamarin-forms

### You learn the following:

- Using the sample Xamarin Forms app and understanding the line of business scenarios it covers.

- Using Prism and Unity in a real-world Xamarin Forms App

- Dependency injection in a real-world Xamarin Forms App

- Abstracting Static services and Static Device information behind interfaces to promote testing and maintainable code

- Using Prism NavigationService and PageDialogService

- Async Await contrasted with the Promise pattern in a Xamarin Forms App

- Using the Ocean validation framework with Xamarin Forms

- Using IDataErrorInfo to surface validation errors for properties or the entire object.  (Yes, IDataErrorInfo!)  This also works for UWP.  These two platform's binding pipeline lack data validation.  No worries, it's very easy to do now.

- Using Ocean validation to validate an object in multiple states.  Multiple validation states imply that the object is going through a workflow, where an object can be valid for a certain state, but not valid for other states.  This is a common scenario in complex business applications like insurance claims or when an object is completed over several Xamarin Forms pages.

- At the end of November 2016, the Xamarin Forms ListView view began to have some problems that cost me over a day.  In the last video, I explain the issues and what I did to get around them.  I also wrote up bugs and communicated with Microsoft about these issues.