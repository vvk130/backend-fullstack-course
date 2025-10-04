
| Date       | Time started | Time used  | Task               |
|------------|--------|--------|----------------------------------|
| 2025-10-02 | 13:00  | 2h | Started project, configuration, trying deployment to monsteraspnet with github direct and github actions, no 403 error anymore - but the swagger is not visible  |
| 2025-10-02 | 15:20  | 0,2h | Final restart of project in root, getting deployment to work: http://fullstackbackend.runasp.net/swagger/index.html |
| 2025-10-02 | 15:55 | 0,5 | Add postgres database - start by creating db in Aiven, install packages, add env variabels, update dbcontext and program.cs file, add port to successfully connect to Aiven (not usual port for portgres) |
| 2025-10-03 | 19:20 | 0,7h | Horse class |
| 2025-10-03 | 20:20 | 0,25h | Horse class modifications, primary keys |
| 2025-10-03 | 20:35 | 0,2h | Studying difference between owned and complex types in Ef core, no collection support for Complex type so using Owned type for this class |
| 2025-10-03 | 21:25 | 0,33h | Adjusting horse model, getting complex type to work, adding System.ComponentModel.DataAnnotations.Schema |
| 2025-10-03 | 21:45 | 0,4h | Adding things to horse model, competition model, competition dto |
| 2025-10-03 | 22:09 | 0,25h | Changing to controllers |
| 2025-10-04 | 10:00 | 1h | Changing to owned types in personality and fears in horse model. Problem with owned types, they were mapped wrong studying how to map them, needed to create a new database to run the migration (chosen database did not give access to pgconsole etc), finding a new database host koyeb and setting up database |
| 2025-10-04 | 11:04 | 0,25h | Starting horse creation service and interface for it |
| 2025-10-04 | 12:14 | 0,5h | Studying default api result types in net and validators and differenced in the frontend when consuming the api based on apporach |
| 2025-10-04 | 13:32 | 1h | Reading about bogus library, adding capitalization to name and removing colors that where in # format, creating random name generator with the library and creating the endpoint (maybe todo add gender overload?)|
| 2025-10-04 | 19:00 | 1h |  |

Total time spent: 7,63h
