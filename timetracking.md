
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
| 2025-10-04 | 19:00 | 1h | Random breed height for horse, Model, service, interface and added to HorsesController - not placed in db because the free instances can spin down or inactivate etc |
| 2025-10-04 | 20:00 | 0,75h | Stable model, starting doing random horse creator |
| 2025-10-07 | 18:00-19:50 | 1,83h | Study Ef.BulkInsert, add package, fix db connection (since outside memory didn't and have to reset on pc), planning validators that are compatible with bulkinsert(attributes or will the library from fluentannotations work...), creating all horses (no pagination only development) endpoint, random color by brees endpoint, adding color data to horse breeds, horse creation and breed service updated |
| 2025-10-07 | 19:50-20:40 | 0,83h | Add age to horsemodel, add ownerId, create random horse creator |
| 2025-10-07 | 20:40-21:45 | 1,08 | Add more randomness to horsemodel (needs refactoring!), starting up with the bulkinsertions problems with the package - maybe wrong project type? Not working |
| 2025-10-07 | 21:45-22:45 | 1h | Reading documentation for the bulk update, trying a few things and reading documentation. Unclear if it will work with this hosted db type|
| 2025-10-07 | 22:45-23:00 | 0,25h | Using ExecuteUpdate for the batch update, since problems with the bulk library, controller and service function done|
| 2025-10-08 | 11:30 | 2 | Add owner, sire, damId, add aging update (for worker task), FoalService, FoalController (needs refactoring), Foal Interface|
| 2025-10-08 | 14:25 | 1,08 | FoalService to inherit some features from parents, breed, color, height, capacity inherited, age O, gender not gelding, corrected a mistake in the HorseBreedService GetRandomColorForBreed |
| 2025-10-08 | 16:15 | 1h | Add env variables to the moster api hosting, Level Model, some refactoringm exposing horse aging endpoint, researching where to host celery for free, render does not seem to offer it |
| 2025-10-08 | 17:15 | 0,33h | Reading and reasearching background task hosting options that are free, Hangfire? |

Total time spent: 18.7h
17,5h per credit