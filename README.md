# The task is to refactor existing code into best practices

# What I have done:

I have rebult the API in .Net Core and separated the data, business and API layers. The layout is now:
API -  The pulic interface
Core - The business logic
Contracts - The models and such, this prevents circular references
Data - The interacts with the data store

Two types of tests are provided - these are not comprehensive but give you a taste:
* Integration tests - these would execute on a temporary database and stubbed external systems
* Unit tests - these execute against mocks to test specific tests

Bonuses:
* Swagger
* Global execution handling
* Various bug fixes

What would I add in a real production system:
* The code is structured to be simple. In a real system, I would move all logic out of the API 
  so as to keep the API as thin as possible and the buess layer to be as testable as possible.
* Validation errors would be returned as a dedicated response if a UI required it. I usually return
  a 400 with the error in the body. This would allow a user interface to display a meaningful error.
* Logging of each call, not just exceptions
* Performance monitoring - either hand-made or something like New Relic.
* Health checks - needed to detect sick machines
* Deployment pipeline
* Log shipping
etc....


# refactor-this
The attached project is a poorly written products API in C#.

Please evaluate and refactor areas where you think can be improved. 

Consider all aspects of good software engineering and show us how you'll make it #beautiful and make it a production ready code.

## Getting started for applicants

There should be these endpoints:

1. `GET /products` - gets all products.
2. `GET /products?name={name}` - finds all products matching the specified name.
3. `GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
4. `POST /products` - creates a new product.
5. `PUT /products/{id}` - updates a product.
6. `DELETE /products/{id}` - deletes a product and its options.
7. `GET /products/{id}/options` - finds all options for a specified product.
8. `GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
9. `POST /products/{id}/options` - adds a new product option to the specified product.
10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.

All models are specified in the `/Models` folder, but should conform to:

**Product:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description",
  "Price": 123.45,
  "DeliveryPrice": 12.34
}
```

**Products:**
```
{
  "Items": [
    {
      // product
    },
    {
      // product
    }
  ]
}
```

**Product Option:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description"
}
```

**Product Options:**
```
{
  "Items": [
    {
      // product option
    },
    {
      // product option
    }
  ]
}
```
