<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>

# Cart API

The Cart API is responsible for managing the shopping cart of the eCommerce application. While it focuses heavily on business rules, it features a simple yet robust architecture. It is built as a **Minimal API** that is well-organized by endpoints and divided into **Use Cases** to ensure maintainability and scalability.

## Architecture

- **Minimal API**: The API is designed with simplicity in mind, using minimal dependencies while ensuring that each endpoint is focused on a specific use case. This leads to a lightweight and efficient design.
- **Use Cases**: The application is structured around use cases, each representing a specific action or business process. This ensures that the business logic is well encapsulated and maintainable.
- **Business Rules**: The API focuses on enforcing the core business logic related to the shopping cart, such as adding, removing, and updating items, and calculating the total cost of the cart.
****
