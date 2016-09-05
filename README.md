# Asp.net_MVC5_DDD_EF6_IoC
Asp.net C# MVC5, EF6, DDD, IoC

This a DDD project made in Asp.Net in C# using MVC 5, EF6 and Ninject as IoC.
In this project is possible to verify implementations of: 

- Generic repository pattern with Unit of Work.
- Programming to interfaces.
- SOLID Principles:
	  -Single responsibility principle => In C# and JavaScript (using patterns) each classes has one only reason to change.
	  -Open close principle => the use of the factory design pattern in RecipeMs.Infra.ImportData is a way of implementing it.
	  -Liskov principle =>  Good encapsulation examples.
	  -Interface segregation principle => the interfaces are short, there is no "no implemented" exception is better seen in -RecipeMs.Application.
	  -Dependency inversion => Use of Ninject to inject dependencies for the classes relies on abstractions and not in concrete classes.
- JavaScript pattern of module and revealing prototype to produce encapsulation use it more like object oriented programming .
- Bootstrap 3, CSS 3 and HTML5.
- Custom Json serialization.
- Generic entities.
- Separate of concerns in specific layers.
	  -Domain => the system core, do not depend of any one. Has entities and services. (DDD).
	  -Data => concrete implementations of the data access.
	  -Application=> connection between presentation devices with the domain layer.
	  -Common => general utilities.
	  -Ioc => Dependency injection implementation .
	  -ImportData => external connection to domain services.
- Entity framework 6 
	  -Migrations and code first.
	  -Fluent api instead annotations in the entities to separate the concerns even further. Data model is responsibility of the data layer.
	  -Deal with the Entity Framework convention and customize it.	
- MVC 5.
