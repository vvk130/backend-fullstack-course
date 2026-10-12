## NET / react fullstack project 

## Repos 
This is the backend repo
And frontend repo: [https://github.com/vvk130/frontend-fullstack-course/](https://github.com/vvk130/frontend-fullstack-course/)

## Project live: [https://frontend-fullstck.netlify.app/](https://frontend-fullstck.netlify.app/)
## Backend live: [https://vvk130-001-site1.htempurl.com/swagger/index.html](https://vvk130-001-site1.htempurl.com/swagger/index.html)

### Architecture & Key Features

- **Generic Pagination via EF.Property:** Implemented a reusable, database-level pagination system that workd even for aggragated paginated lists.
- **Generic Repository & Controllers:** Base CRUD operations are abstracted into a single generic layer to eliminate boilerplate code and keep the codebase DRY.
- **Polymorphic Data Models (Animal -> Horse / Alpaca):** Uses class inheritance on the backend (`Animal` base entity). This allows the React frontend to consume a predictable structure and use identical UI components for different subtypes.
- **Data Encapsulation (DTOs) & Mapping:** Implemented AutoMapper to map entities to Data Transfer Objects (DTOs), keeping database schemas isolated from the client layer.
- **ACID Transactions:** Uses explicit Entity Framework Core transactions for multi-table updates to ensure data integrity and full rollbacks on failure.


